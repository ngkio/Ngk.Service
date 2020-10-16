namespace Ngk.DataAccess.DTO
{
    public class LoginInfoModel
    {
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public UserModel UserInfo { get; set; }
    }
}