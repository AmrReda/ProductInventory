using Newtonsoft.Json;

namespace ProductInventory.Api.ViewModels
{
    public class UserResponse
    {
        [JsonProperty]

        public string Token { get; private set; }

        [JsonProperty]

        public string Name { get; private set; }

        public static UserResponse CreateUser()
        {
            return new UserResponse()
            {
                Name = "Amr Kamel",
                Token = "108337a6-dbcd-4231-92d4-d4962fc43b71"
            };
        }
    }
}