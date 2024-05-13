using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PiSec.Api.Controllers;
using PiSec.Api.Entities;
using PiSec.Api.Model;
using PiSec.Api.Model.RequestModel;
using PiSec.Test.UserTest;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

namespace PiSec.Test
{
    public partial class UserControllerTest : IClassFixture<UserFixture>
    {
        private UserController _userController;
        private CreateUserRequestModel _user;

        public UserControllerTest(UserFixture fixture)
        {
            _userController = fixture.userController;
            _user = fixture.User;
        }

        [Theory]
        [MemberData(nameof(CreateUserTestCase))]
        public async Task Create_User_Should_Return_Expected_StatusCode(CreateUserRequestModel req, int expectedStatusCode)
        {
            //Act
            var response = await _userController.CreateUser(req) as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == 200)
            {
                var responseModel = Assert.IsType<ResponseModel<User>>(response.Value);
                Assert.NotNull(responseModel);
                Assert.NotNull(responseModel.Data);
                Assert.Equal(req.Name, responseModel.Data.Name);
                Assert.Equal(req.Email, responseModel.Data.Email);
            }
        }

        [Theory]
        [MemberData(nameof(UpdateUserTestCase))]
        public async Task Update_User_Should_Return_Expected_StatusCode(UpdateUserRequestModel req, int id, int expectedStatusCode)
        {
            //Arrange
            //var user = await CreateUserTestData();

            //Act
            var response = await _userController.UpdateUser(req, id) as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == 200)
            {
                var responseModel = Assert.IsType<ResponseModel<User>>(response.Value);
                Assert.NotNull(responseModel);
                Assert.NotNull(responseModel.Data);
                if (req.Name.IsNullOrEmpty())
                {
                    Assert.Equal(_user.Name, responseModel.Data.Name);
                }
                else
                {
                    Assert.Equal(req.Name, responseModel.Data.Name);
                }

                if (req.Email.IsNullOrEmpty())
                {
                    Assert.Equal(_user.Email, responseModel.Data.Email);
                }
                else
                {
                    Assert.Equal(req.Email, responseModel.Data.Email);
                }

            }
        }

        [Theory]
        [InlineData(null, 200, true)]
        [InlineData("te", 200, true)]
        [InlineData("aaa", 200, false)]
        public async Task Get_Users_Should_Return_Expected_StatusCode(string name, int expectedStatusCode, bool isFindData)
        {
            //Arrange
            //var user = await CreateUserTestData();

            //Act
            var response = _userController.GetUsers(name) as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == 200)
            {
                var responseModel = Assert.IsType<ResponseModel<List<User>>>(response.Value);
                Assert.NotNull(responseModel);
                Assert.NotNull(responseModel.Data);
                Assert.Equal(isFindData, responseModel.Data.Count > 0);
            }
        }

        [Theory]
        [InlineData(1,200)]
        [InlineData(20,404)]
        public async Task Delete_User_Should_Return_Expected_StatusCode(int id, int expectedStatusCode)
        {
            //Arrange
            //var user = await CreateUserTestData();

            //Act
            var response = await _userController.DeleteUser(id) as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatusCode, response.StatusCode);

            if (response.StatusCode == 200)
            {
                var responseModel = Assert.IsType<ResponseModel<User>>(response.Value);
                Assert.NotNull(responseModel);
                Assert.NotNull(responseModel.Data);
                Assert.False(responseModel.Data.IsActive);
            }
        }
    }
}