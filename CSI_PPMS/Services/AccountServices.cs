using CSI_PPMS.Entity;
using CSI_PPMS.IServices;
using CSI_PPMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSI_PPMS.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly PPMSContext _context;

        public AccountServices(PPMSContext context)
        {
            _context = context;
        }

        public async Task<LoginResponse> Login(Login model)
        {
            var log = new AppLogs();
            var checkUser = await _context.User.Include(x => x.UserRole)
                                         .ThenInclude(x => x.Role)
                                         .Where(x => x.UserName == model.UserName &&
                                                     x.Password == model.Password)
                                         .FirstOrDefaultAsync();
            if (checkUser != null)
            {
                log.Log = $"new user login {checkUser.UserName} on {DateTime.Now}";
                log.LogType = "normal";
                log.ModuleId = checkUser.UserRole.Select(x => x.ModuleId).FirstOrDefault();
                log.UserId = checkUser.UserId;

                
                var res = new LoginResponse()
                {
                    UserName = checkUser.UserName,
                    UserId = checkUser.UserId,
                    RoleId = checkUser.UserRole.Select(x => x.RoleId).FirstOrDefault(),
                    ModuleId = checkUser.UserRole.Select(x => x.ModuleId).FirstOrDefault()
                };

                _context.Add(log);
                _context.SaveChanges();
                return res;
            }

            log.Log = $"login failed for {model.UserName} on password {model.Password}";
            log.LogType = "normal";
            log.ModuleId = 1;
            log.UserId = 1;
            _context.Add(log);
            _context.SaveChanges();

            return null;
        }


        public async Task<APIResponse> CreateUser(CreateUserModel model)
        {
            var checkuser = await _context.User.Include(x => x.UserRole).Where(x => x.UserName == model.UserName && x.UserRole.Select(x => x.ModuleId).FirstOrDefault() == model.moduleId).FirstOrDefaultAsync();
            if (checkuser is not null)
                return new APIResponse("user already exists", 400);
            var newUser = new User()
            {
                UserName = model.UserName,
                Password = model.Password
            };
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var role = new UserRole()
            {
                UserId = newUser.UserId,
                RoleId = 2,
                ModuleId = model.moduleId
            };

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
            return new APIResponse("user Created", 200);
        }


        public async Task<List<UserListModel>> UserListByModuleId(long moduleId)
        {
            var data = await _context.User.Include(x => x.UserRole).Where(x => x.UserRole.Select(x => x.ModuleId).FirstOrDefault() == moduleId && x.IsDeleted != true && x.UserRole.Select(x => x.RoleId).FirstOrDefault() != 1)
                .Select(x => new UserListModel
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    CreatedDate = x.CreatedDate != null ? x.CreatedDate.Value.ToString("dd-MM-yyyy HH:mm:ss") : DateTime.Now.AddDays(30).ToString("dd-MM-yyyy HH:mm:ss")
                })
                .ToListAsync();
            return data;
        }

        public void DeleteUser (long UserId)
        {
            var user = _context.User.Where(x => x.UserId == UserId).FirstOrDefault();
            if(user != null)
            {
                user.IsDeleted = true;
                _context.Update(user);
                _context.SaveChanges();
            }    
        }

        public bool ValidateUser(long userId,string password )
        {
            var user = _context.User.Where(x => x.UserId == userId && x.Password == password).FirstOrDefault();
            return user != null;
        }


    }
}
