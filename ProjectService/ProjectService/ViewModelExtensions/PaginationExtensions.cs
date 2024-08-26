using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

public static class PaginationExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, PaginationParams paginationParams, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);

        var items = await source
            .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<T>(items, count, paginationParams.Page, paginationParams.PageSize);
    }

    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, DbContext context, Expression<Func<T, object>> orderBy,bool isDescending, PaginationParams paginationParams, CancellationToken cancellationToken = default) where T : class
    {
        var page = paginationParams.Page;
        var pageSize = paginationParams.PageSize;

        // 获取表名（这里简化为假设的方法，实际中需要查询EF Core元数据）  
        string tableName = GetTableName(context, typeof(T));

        // 将Lambda表达式转换为SQL ORDER BY子句片段  
        string orderBySql = GetOrderBySql(orderBy, isDescending);

        // 构建带有ROW_NUMBER()的SQL查询字符串  
        string paginatedSql = $"WITH NumberedItems AS (" +
                              $"SELECT *, ROW_NUMBER() OVER(ORDER BY {orderBySql}) AS RowNum FROM {tableName} " +
                              $") " +
                              $"SELECT * FROM NumberedItems WHERE RowNum BETWEEN {((page - 1) * pageSize) + 1} AND {page * pageSize}";

        // 执行原始SQL查询并映射到实体类型T  
        var dbSet = context.Set<T>();
        var query = dbSet.FromSqlRaw(paginatedSql);
        var items = await query.ToListAsync(cancellationToken);

        // 注意：这里我们仍然需要另外一个查询来获取总记录数  
        // 假设我们有一个简单的方法来获取总记录数（这可能需要一个单独的查询或缓存策略）  
        int totalCount = await GetTotalCountAsync(context, source, cancellationToken);

        return new PaginatedList<T>(items, totalCount, paginationParams.Page, paginationParams.PageSize);
    }

    private static string GetTableName<T>(DbContext context) where T : class
    {
        var entityType = context.Model.FindEntityType(typeof(T));
        if (entityType == null)
        {
            throw new InvalidOperationException($"Entity type '{typeof(T).Name}' is not part of the model for this context.");
        }

        // 对于SqlServer提供程序，使用Relational()扩展方法和TableName属性  
        var tableName = entityType.GetTableName();
        if (string.IsNullOrEmpty(tableName))
        {
            // 如果TableName为空，可能是因为没有显式设置，那么可以回退到使用默认的表名（通常是类名）  
            tableName = entityType.DisplayName(); // DisplayName()通常返回的是DbSet的属性名，它可能与类名相同或不同，取决于配置  
        }

        return tableName;
    }

    private static string GetTableName(DbContext context, Type type)
    {
        var entityType = context.Model.FindEntityType(type);
        if (entityType == null)
        {
            throw new InvalidOperationException($"Entity type '{type.Name}' is not part of the model for this context.");
        }

        // 对于SQL Server，表名通常可以直接从IEntityType获取  
        // 但请注意，这取决于EF Core版本和提供程序的实现细节  
        var tableName = entityType.GetTableName(); // 这可能是提供程序特定的扩展方法  
        if (tableName == null)
        {
            // 如果GetTableName()不可用或返回null，则尝试其他方法  
            // 例如，从注解中获取表名或回退到默认命名约定  
            var tableNameAnnotation = entityType.FindAnnotation("Relational:TableName");
            if (tableNameAnnotation != null && tableNameAnnotation.Value is string name)
            {
                tableName = name;
            }
            else
            {
                // 作为最后手段，使用DisplayName，它通常是DbSet的属性名  
                tableName = entityType.DisplayName();
            }
        }

        return tableName;
    }

    private static async Task<int> GetTotalCountAsync<T>(DbContext context, IQueryable<T> source, CancellationToken cancellationToken = default) where T : class
    {
        // 直接对source调用CountAsync来获取总记录数  
        return await source.CountAsync(cancellationToken);
    }

    private static string GetOrderBySql<T>(Expression<Func<T, object>> orderBy, bool isDescending)
    {
        // 解析Lambda表达式并获取属性名（这里简化处理）  
        MemberExpression memberExpression = null;
        if (orderBy.Body is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Convert)
        {
            memberExpression = unaryExpression.Operand as MemberExpression;
        }
        else if (orderBy.Body is MemberExpression me)
        {
            memberExpression = me;
        }

        if (memberExpression == null)
        {
            throw new ArgumentException("The order by expression must be a member access expression.", nameof(orderBy));
        }

        string propertyName = memberExpression.Member.Name;
        // 如果需要降序，则添加DESC关键字  
        string orderDirection = isDescending ? "DESC" : "ASC";
        return $"{propertyName} {orderDirection}";
    }


    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, PaginationParams paginationParams)
    {
        var count = source.Count();

        var items = source
            .Skip((paginationParams.Page - 1) * paginationParams.PageSize)
            .Take(paginationParams.PageSize)
            .ToList();

        return new PaginatedList<T>(items, count, paginationParams.Page, paginationParams.PageSize);
    }
    
}