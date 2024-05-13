using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PiSec.Api.Controllers;
using PiSec.Api.Entities;
using PiSec.Api.Extension;
using PiSec.Api.Model;
using PiSec.Api.Model.RequestModel;
using PiSec.Api.Repository;

namespace PiSec.Api.BusinessLogic.UserBSL
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger, AppDbContext context) 
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ResponseModel<User>> CreateUser(CreateUserRequestModel req)
        {
            if (req.Name.IsNullOrEmpty()) return new ResponseModel<User>(400,"Name is required");
            if (req.Email.IsNullOrEmpty()) return new ResponseModel<User>(400,"Email is required");
            if (!req.Email.IsValidEmail()) return new ResponseModel<User>(400,"Invalid Email format");

            var user = new User(req.Name, req.Email);

            _context.Add(user);

            await _context.SaveChangesAsync();

            return new ResponseModel<User>(200,"Create user successfully.", user);
        }

        public async Task<ResponseModel<User>> DeleteUsers(int id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user is null)
            {
                return new ResponseModel<User>(404, "User not found");
            }

            user.IsActive = false;

            await _context.SaveChangesAsync();

            return new ResponseModel<User>(200, "Delete user successfully.", user);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public ResponseModel<List<User>> GetUsers(string name)
        {
            List<User> users = _context.Users
                .Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{name}%".ToLower()))
                .Where(x => x.IsActive).ToList();
            
            return new ResponseModel<List<User>>(200,"User found successfully.", users);
        }

        public async Task<ResponseModel<User>> UpdateUsers(UpdateUserRequestModel req,int id)
        {
            if (req.Name.IsNullOrEmpty() && req.Email.IsNullOrEmpty()) return new ResponseModel<User>(400,"Name and email is null or empty");
            if (!req.Email.IsNullOrEmpty() && !req.Email.IsValidEmail()) return new ResponseModel<User>(400,"Invalid Email format");

            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();

            if (user is null)
            {
                return new ResponseModel<User>(404,"User not found");
            }

            if (!req.Name.IsNullOrEmpty())
            {
                user.Name = req.Name;
            }
            if (!req.Email.IsNullOrEmpty())
            {
                user.Email = req.Email;
            }

            await _context.SaveChangesAsync();

            return new ResponseModel<User>(200, "User found successfully.", user);
        }
    }
}
