using PiSec.Api.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiSec.Test
{
    public partial class UserControllerTest
    {
        public static IEnumerable<object[]> CreateUserTestCase => new List<object[]>
        {
            new object[] { new CreateUserRequestModel { Name = "test", Email = "test@email.com" }, 200 },
            new object[] { new CreateUserRequestModel { Name = "", Email = "" }, 400 },
            new object[] { new CreateUserRequestModel { Name = "22", Email = "" }, 400 },
            new object[] { new CreateUserRequestModel { Name = "testtwo", Email = "" }, 400 },
            new object[] { new CreateUserRequestModel { Name = "testthree", Email = "test" }, 400 },
            new object[] { new CreateUserRequestModel { Name = "", Email = "test@gmail.com" }, 400 },
        };

        public static IEnumerable<object[]> UpdateUserTestCase => new List<object[]>
        {
            new object[] { new UpdateUserRequestModel { Name = "testtwo", Email = "" }, 1, 200 },            
            new object[] { new UpdateUserRequestModel { Name = "", Email = "" }, 2, 400 },
            new object[] { new UpdateUserRequestModel { Name = "22", Email = "" }, 1, 200 },            
            new object[] { new UpdateUserRequestModel { Name = "testthree", Email = "test" }, 5, 400 },
            new object[] { new UpdateUserRequestModel { Name = "test", Email = "testbb@gmail.com" },20, 404 },
            new object[] { new UpdateUserRequestModel { Name = "test", Email = "testaa@email.com" },1, 200 },
        };

        public async Task<CreateUserRequestModel> CreateUserTestData()
        {
            var user = new CreateUserRequestModel { Name = "testcreate", Email = "test@gmail.com" };
            await _userController.CreateUser(user);
            return user;
        }
    }
}
