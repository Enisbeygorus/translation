﻿using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.User
{
    public sealed class ChangePasswordModel : BaseModel
    {
        public Guid UserUid { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReEnterNewPassword { get; set; }

        public HiddenInputModel UserInput { get; }

        public PasswordInputModel OldPasswordInput { get; }
        public PasswordInputModel NewPasswordInput { get; }
        public PasswordInputModel ReEnterNewPasswordInput { get; }

        public ChangePasswordModel()
        {
            Title = "user_change_password_title";

            UserInput = new HiddenInputModel("UserUid");

            OldPasswordInput = new PasswordInputModel("OldPassword", "old_password", true);
            NewPasswordInput = new PasswordInputModel("NewPassword", "password", true);
            ReEnterNewPasswordInput = new PasswordInputModel("ReEnterNewPassword", "re_enter_new_password", true);
        }

        public override void SetInputModelValues()
        {
            UserInput.Value = UserUid.ToUidString();

            OldPasswordInput.Value = OldPassword;
            NewPasswordInput.Value = NewPassword;
            ReEnterNewPasswordInput.Value = ReEnterNewPassword;
        }

        public override void SetInputErrorMessages()
        {
            if (UserUid.IsEmptyGuid())
            {
                ErrorMessages.Add("user_uid_is_not_valid_error_message");
            }

            OldPassword = OldPassword.TrimOrDefault();
            if (OldPassword.IsEmpty())
            {
                OldPasswordInput.ErrorMessage.Add("old_password_required_error_message");
                InputErrorMessages.AddRange(OldPasswordInput.ErrorMessage);
            }

            NewPassword = NewPassword.TrimOrDefault();
            if (NewPassword.IsEmpty())
            {
                NewPasswordInput.ErrorMessage.Add("new_password_required_error_message");
                InputErrorMessages.AddRange(NewPasswordInput.ErrorMessage);
            }

            ReEnterNewPassword = ReEnterNewPassword.TrimOrDefault();
            if (ReEnterNewPassword.IsEmpty())
            {
                ReEnterNewPasswordInput.ErrorMessage.Add("re_entered_password_required_error_message");
                InputErrorMessages.AddRange(ReEnterNewPasswordInput.ErrorMessage);
            }

            OldPassword = OldPassword.TrimOrDefault();
            if (OldPassword.IsNotValidPassword())
            {
                OldPasswordInput.ErrorMessage.Add("old_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(OldPasswordInput.ErrorMessage);
            }

            NewPassword = NewPassword.TrimOrDefault();
            if (NewPassword.IsNotValidPassword())
            {
                NewPasswordInput.ErrorMessage.Add("new_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(NewPasswordInput.ErrorMessage);
            }

            ReEnterNewPassword = ReEnterNewPassword.TrimOrDefault();
            if (ReEnterNewPassword.IsNotValidPassword())
            {
                ReEnterNewPasswordInput.ErrorMessage.Add("re_entered_password_is_not_valid_error_message");
                InputErrorMessages.AddRange(ReEnterNewPasswordInput.ErrorMessage);
            }

            OldPassword = OldPassword.TrimOrDefault();
            if (OldPassword == NewPassword)
            {
                NewPasswordInput.ErrorMessage.Add("new_password_can_not_same_as_old_password_error_message");
                InputErrorMessages.AddRange(NewPasswordInput.ErrorMessage);
            }

            NewPassword = NewPassword.TrimOrDefault();
            if (NewPassword != ReEnterNewPassword)
            {
                ReEnterNewPasswordInput.ErrorMessage.Add("re_entered_password_does_not_match_error_message");
                InputErrorMessages.AddRange(ReEnterNewPasswordInput.ErrorMessage);
            }
        }
    }
}