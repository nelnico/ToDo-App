namespace TodoApp.Api.Models.Auth
{
    public class LoginResultModel
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
