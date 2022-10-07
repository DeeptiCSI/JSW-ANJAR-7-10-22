using Newtonsoft.Json;

namespace CSI_PPMS.Models
{
    public class Login
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; }

        [JsonProperty("moduleId")]
        public long ModuleId { get; set; }
    }

    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public long moduleId { get; set; }
    }

    public class UserListModel
    {
        public string UserName { get; set; }
        public long UserId { get; set; }
        public string CreatedDate { get; set; }
    }
}
