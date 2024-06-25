using Microsoft.AspNetCore.Identity;

namespace ClinicWebApp.Validators.PasswordValidators
{
    public class SimplePasswordValidator : IPasswordValidator<string>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<string> manager, string user, string? password)
        {
            var errors = new List<IdentityError>();

            if (password.Length < 8)
            {
                errors.Add(new IdentityError()
                {
                    Description = "رمز باید شامل حداقل 8 کاراکتر باشد"
                });
            }

            //if (!password.Any(char.IsDigit))
            //{
            //    errors.Add("Password must contain at least one digit.");
            //}

            //if (!password.Any(char.IsLower))
            //{
            //    errors.Add("Password must contain at least one lowercase letter.");
            //}

            //if (!password.Any(char.IsUpper))
            //{
            //    errors.Add("Password must contain at least one uppercase letter.");
            //}

            //if (!password.Any(char.IsSymbol))
            //{
            //    errors.Add("Password must contain at least one special character.");
            //}

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}
