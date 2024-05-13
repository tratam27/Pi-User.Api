using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PiSec.Api.BusinessLogic.UserBSL;
using PiSec.Api.Controllers;
using PiSec.Api.Entities;
using PiSec.Api.Model.AppSettingModel;
using PiSec.Api.Model.RequestModel;
using PiSec.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiSec.Test.UserTest
{
    public class UserFixture : IDisposable
    {
        public UserController userController;
        public CreateUserRequestModel User { get; set; }
        protected readonly AppDbContext _context;
        private readonly IDbContextTransaction _transaction;
        public UserFixture()
        {
            var setting = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            AppSettingModel appsetting = setting.GetSection("AppSetting").Get<AppSettingModel>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserTestDatabase")
                .Options;

            _context = new AppDbContext(options);

            IUserService userService = new UserService(Mock.Of<ILogger<UserService>>(), _context);

            userController = new UserController(Options.Create(appsetting), Mock.Of<ILogger<UserController>>(), _context, userService);

            User = CreateUserTestData().Result;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<CreateUserRequestModel> CreateUserTestData()
        {
            var user = new CreateUserRequestModel { Name = "testcreate", Email = "test@gmail.com" };
            await userController.CreateUser(user);
            return user;
        }
    }
}
