using TelefonRehberi.Entity;

namespace TelefonRehberi.Dto.Account
{
    public class ChangePasswordModel
    {
        public Admin admin { get; set; }
        public string password { get; set; }
        public string currentPassword { get; set; }

    }
}