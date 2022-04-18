namespace API.ViewModels
{
    public class ChangePasswordVM
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public int MyProperty { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
