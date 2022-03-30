namespace Common.Utilities
{
    public static class Constants
    {
        public const string roleNotFound = "Role not present. Please create Role";
        public const string permissionAssignErrror = "The Permission could not be assigned!";
        public const string changeUsernameError = "The Username could not be changed!";
        public const string changeRolenameError = "The Rolename could not be changed!";
        public const string userUpdateFailed = "User Update Failed! Please check users details and try again";
        public const string roleUpdateFailed = "Role Update Failed! Please check role details and try again";
        public const string invalidAccess = "Invalid Permission!";
        public const string roleExist = "Role already exists!";
        public const string userCreationFailed = "User creation failed! Please check user details and try again.";
        public const string userExist = "User already exist! Please login";
        public const string UserNotFound = "User doesn't exist with this Phone Number. Please try again!";

        public static string SetGetOTPSMS(string code)
        {
            return $"Your OTP for Power login is: {code}. This OTP is valid only for 3 minutes.";
        }
    }


}
