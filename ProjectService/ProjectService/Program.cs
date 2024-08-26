using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.Services;
using System;
using ProjectService.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjectService.UserDb;
using Microsoft.OpenApi.Models;
using System.Text.Json;

namespace ProjectService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                // ע��ȫ���쳣������
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<ApiResponseFilter>();
            }).AddJsonOptions(options =>
            {
                //���datetime�Զ���ת����ʽ
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss.fff"));
            });

            builder.Logging.AddLog4Net("Configs/log4net.config");

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            RegisterService(ref builder);
            // �������ݿ������ַ�������appsettings.json�ж��壩  
            var projectDbConnectionString = builder.Configuration.GetConnectionString("ProjectConnection");

            // ������ݿ�������  
            builder.Services.AddDbContext<ProjectDbContext>(options =>
                options.UseLazyLoadingProxies()
                       .UseSqlServer(projectDbConnectionString));

            var userDbConnectionString = builder.Configuration.GetConnectionString("UserConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(userDbConnectionString));

            //���Identity����
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.Configure<JwtModel>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // �ڿ��������п�������Ϊfalse������������Ӧ����Ϊtrue  
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // ����Ĭ�ϵĴ����߼�  
                        context.HandleResponse();

                        // ������֤ʧ�ܵ���Ӧ  
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var apiResponse = ApiResponse<object>.CreateFailedResponse("��֤ʧ�ܣ���Ч��JWT����", 401);
                        var responseString = JsonSerializer.Serialize(apiResponse); // ʹ��System.Text.Json  

                        return context.Response.WriteAsync(responseString);
                    }
                };
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // ��� JWT ��֤�����ã���ѡ��  
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseAuthentication(); // ȷ���� UseAuthorization ֮ǰ����  
            app.UseAuthorization(); // ��Ȩ�м�������ڴ�����Ȩ����

            app.MapControllers();

            app.Run();
        }

        public static void RegisterService(ref WebApplicationBuilder builder)
        {
            //builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<ProjectAllService>();
            builder.Services.AddScoped<DictService>();
            builder.Services.AddScoped<StaffService>();
            builder.Services.AddScoped<ProcessService>();
            builder.Services.AddScoped<DeviceService>();
            builder.Services.AddScoped<ProjectDailyProcessService>();
            builder.Services.AddScoped<ProjectBonusService>();
            builder.Services.AddScoped<EarlyWarningService>();
            builder.Services.AddScoped<ProjectAttachmentService>();
            builder.Services.AddScoped<ProjectPaymentTermService>();
            builder.Services.AddScoped<ProjectDailyWorkService>();
            builder.Services.AddScoped<ConsumableService>();
            builder.Services.AddScoped<ProjectUpdateScheduleService>();
            builder.Services.AddScoped<ApplicationService>();
            builder.Services.AddScoped<PatrolService>();
            builder.Services.AddScoped<ConsumableAskForService>();
            builder.Services.AddScoped<WorkAttendanceService>();
            builder.Services.AddScoped<PlaceOnFileService>();
        }
    }
}
