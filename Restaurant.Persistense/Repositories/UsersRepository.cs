using Restaurant.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.CustomExceptions;
using Restaurant.Core.Enums;

namespace Restaurant.Persistense.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RestaurantDbContext _context;
        public UsersRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            if (_context.Users.Any(x => x.Email == user.Email || x.PhoneNum == user.PhoneNum))
            {
                throw new UserAlreadyExistsException("User with these credits already exists!");
            }

            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == (int)Core.Enums.Role.User)
                ?? throw new InvalidOperationException();       

            var model = new User(user.Id, user.Email, user.PasswordHash, user.PhoneNum)
            {
                Roles = [role]
            };

            await _context.Users.AddAsync(user);            
            await _context.UserRoles.AddAsync(new() { RoleId = role.Id, UserId = user.Id });
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();
        }

        public async Task<HashSet<Core.Enums.Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(u => u.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles.SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Core.Enums.Permission)p.Id)
                .ToHashSet();
        }
    }
}
