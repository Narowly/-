using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.UserDb;
using System.Security.Claims;

namespace ProjectService.Services
{
    public class UserService
    {
        protected readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser>? _userManager;
        public UserService(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser>? userManager=null)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        protected Guid? GetUserId() 
        {
            var id = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null) return null;
            return Guid.Parse(id);
        }

        public async Task<ApplicationUser?> GetUserById(string userId)
        {
            if (_userManager == null) return null;
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<List<ApplicationUser>?> GetUsersByIds(List<string> userIds)
        {
            if (_userManager == null) return null;
            return await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();
        }


    }
}
