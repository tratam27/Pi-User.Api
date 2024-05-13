using PiSec.Api.Entities;
using PiSec.Api.Model;
using PiSec.Api.Model.RequestModel;

namespace PiSec.Api.BusinessLogic.UserBSL
{
    public interface IUserService : IDisposable
    {
        public Task<ResponseModel<User>> CreateUser(CreateUserRequestModel req);
        public ResponseModel<List<User>> GetUsers(string name);
        public Task<ResponseModel<User>> UpdateUsers(UpdateUserRequestModel req, int id);
        public Task<ResponseModel<User>> DeleteUsers(int id);

    }
}
