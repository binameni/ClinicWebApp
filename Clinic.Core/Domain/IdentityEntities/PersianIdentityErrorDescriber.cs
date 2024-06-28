using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Core.Domain.IdentityEntities
{
    public class PersianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = string.Format(PersianIdentityErrorMessages.DuplicateEmail, email)
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = string.Format(PersianIdentityErrorMessages.DuplicateUserName, userName)
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = string.Format(PersianIdentityErrorMessages.InvalidEmail, email)
            };
        }

        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = string.Format(PersianIdentityErrorMessages.InvalidUserName, userName)
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = PersianIdentityErrorMessages.LoginAlreadyAssociated
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = PersianIdentityErrorMessages.PasswordMismatch
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = string.Format(PersianIdentityErrorMessages.PasswordTooShort, length)
            };
        }

        public override IdentityError UserAlreadyHasPassword()
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = PersianIdentityErrorMessages.UserAlreadyHasPassword
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = string.Format(PersianIdentityErrorMessages.UserAlreadyInRole, role)
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description = string.Format(PersianIdentityErrorMessages.UserNotInRole, role)
            };
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return new IdentityError
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = PersianIdentityErrorMessages.RecoveryCodeRedemptionFailed
            };
        }

        public override IdentityError DefaultError()
        {
            return new IdentityError
            {
                Code = nameof(DefaultError),
                Description = PersianIdentityErrorMessages.DefaultIdentityError
            };
        }
    }

    public struct PersianIdentityErrorMessages
    {
        public static readonly string DuplicateEmail = "این ایمیل قبلا استفاده شده است";
        public static readonly string DuplicateUserName = "نام کاربری تکراری است";
        public static readonly string InvalidEmail = "ایمیل وارد شده نامعتبر است";
        public static readonly string InvalidUserName = "نام کاربری نامعتبر است";
        public static readonly string LoginAlreadyAssociated = "عملیات ورود انجام شده است";
        public static readonly string PasswordMismatch = "رمز ورودی تطابق ندارد";
        public static readonly string PasswordTooShort = "رمز ورود باید حداقل شامل {0} کاراکتر باشد";
        public static readonly string UserAlreadyHasPassword = "این رمز برای کاربر تنظیم شده است";
        public static readonly string UserAlreadyInRole = "این حساب به دسترسی {0} مجاز است";
        public static readonly string UserNotInRole = "این حساب به دسترسی {0} مجاز نیست";
        public static readonly string RecoveryCodeRedemptionFailed = "کد ریکاوری اشتباه وارد شده";
        public static readonly string DefaultIdentityError = "خطای احراز هویت";
    }
}
