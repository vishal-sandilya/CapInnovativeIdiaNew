using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Domain.Constants
{
    public class ResponseMessageConstant
    {
        //Account
        public const string InvalidCurrentPassword = "You have enetered invalid current password.";
        public const string PasswordChangeSuccessfully = "Password has been changed successfully.";
        public const string UpdateProfileSuccessfully = "Profile has been updated successfully.";
        public const string InvalidEmailOrPassword = "You have entered invlid email or password.";
        public const string EmailNotRegister = "Email does not exists in system.";
        public const string ForgotPasswordLinkSent = "Forgot Password link has been sent successfully to your email.";

        //User
        public const string UserCreatedSuccessfully = "User has been created successfully.";
        public const string UserUpdatedSuccessfully = "User has been updated successfully.";
        public const string UserDeletedSuccessfully="User has been deleted successfully.";

        //Idia
        public const string IdiaCreatedSuccessfully = "Idia has been created successfully.";
        public const string IdiaDeletedSuccessfully = "Idia has been deleted successfully.";
        public const string IdiaEditedSuccessfully = "has been edited successfully.";

        //Reviewer
        public const string IdiaApprovedSuccessfully = "Idia has been approved successfully.";
        public const string IdiaRejectedSuccessfully = "Idia has been rejected successfully.";
        //Generic Messages

        public const string InvalidAction = "Oops, there is some error, please try after some time.";
        public const string UnAuthorizedAction = "It seems your are not auhorized this action";
        public const string RecordNotFound = "There is not record exists for given Id.";
    }
}
