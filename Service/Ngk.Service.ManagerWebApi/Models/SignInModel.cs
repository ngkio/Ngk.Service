namespace Ngk.Service.ManagerWebApi.Models
{
    public class SignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int AuthenticateNum { get; set; }
    }
}
