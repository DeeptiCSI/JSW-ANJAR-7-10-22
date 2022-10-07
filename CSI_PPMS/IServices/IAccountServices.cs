using CSI_PPMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSI_PPMS.IServices
{
    public interface IAccountServices
    {
        Task<LoginResponse> Login(Login model);

        Task<APIResponse> CreateUser(CreateUserModel model);

        Task<List<UserListModel>> UserListByModuleId(long moduleId);

        void DeleteUser(long UserId);

        bool ValidateUser(long userId, string password);

    }
}
