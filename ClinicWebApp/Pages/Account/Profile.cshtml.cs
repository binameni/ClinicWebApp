using Clinic.Core.Domain.Entities;
using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Extensions.FileExtensions;
using ClinicWebApp.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ClinicWebApp.Pages.Account
{
    [Authorize]
    public class ProfileModel(UserManager<ApplicationUser> userManager,
        IDoctorRepository doctorRepository) : PageModelWrapper
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IDoctorRepository _doctorRepository = doctorRepository;

        [BindProperty]
        [Required(ErrorMessage = "مقدار نام نباید خالی باشد")]
        [Display(Name = "اسم")]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "نام کاربری")]
        public string? UserName { get; set; }

        [BindProperty]
        [Display(Name = "کد ملی")]
        public string? NationalCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "مقدار شماره موبایل نباید خالی باشد")]
        [Display(Name = "شماره موبایل")]
        [Length(minimumLength: 10, maximumLength: 10, ErrorMessage = "شماره موبایل باید ده رقم باشد")]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [BindProperty]
        [Display(Name = "شماره پزشکی")]
        public string? DoctorNumber { get; set; }

        [BindProperty]
        [Display(Name = "تخصص")]
        public string? Profession { get; set; }

        [BindProperty]
        [Display(Name = "پروفایل")]
        public IFormFile? Profile { get; set; }

        [BindProperty]
        public string? ProfileAddress { get; set; }

        [BindProperty]
        [Display(Name = "عکس ها")]
        public List<IFormFile>? Pictures { get; set; } = new List<IFormFile>();

        [BindProperty]
        public List<string>? PicturesAddresses { get; set; } = new List<string>();

        [BindProperty]
        public bool isDoctor { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ApplicationUser thisUser = await _userManager.FindByNameAsync(User.Identity.Name);
            UserName = thisUser.UserName;
            Name = thisUser.Name;
            NationalCode = thisUser.NationalCode;
            PhoneNumber = thisUser.PhoneNumber;
            Email = thisUser.Email;
            if (await _userManager.IsInRoleAsync(thisUser, ApplicationRoles.Doctor))
            {
                Doctor doctorInfo = await _doctorRepository.Get(User.Identity.Name);
                isDoctor = true;
                DoctorNumber = doctorInfo.DoctorNumber;
                Profession = doctorInfo.Profession;
                if (doctorInfo.ProfileSrc != null)
                {
                    ProfileAddress = doctorInfo.ProfileSrc;
                }
                if (!doctorInfo.PicturesSrc.IsNullOrEmpty())
                {
                    PicturesAddresses.AddRange(doctorInfo.PicturesSrc);
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!ulong.TryParse(PhoneNumber, out var temp2))
            {
                ModelState.AddModelError(nameof(PhoneNumber), "لطفا در قسمت شماره موبایل عدد وارد کنید");
                return Page();
            }

            // Receives user from database
            ApplicationUser thisUser = await _userManager.FindByNameAsync(User.Identity.Name);
            thisUser.Name = Name;
            thisUser.PhoneNumber = PhoneNumber;
            thisUser.Email = Email;
            // Updates users data
            IdentityResult result = await _userManager.UpdateAsync(thisUser);

            // Interrupts the process if updating fails
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                this.SetPrompt("فرایند ذخیره سازی اطلاعات با مشکل مواجه شد");
                return Page();
            }

            // Check if the user has Doctor role to do further updates
            if (await _userManager.IsInRoleAsync(thisUser, ApplicationRoles.Doctor))
            {
                // Receives doctors info
                Doctor doctorInfo = await _doctorRepository.Get(User.Identity.Name);
                doctorInfo.Profession = Profession;

                // Checks if user has chosen profile picture to upload
                if (Profile != null)
                {
                    // Checks if user has uploaded profile before to remove it
                    if (!doctorInfo.ProfilePhysical.IsNullOrEmpty())
                    {
                        FormFileExtensions.DeleteImage(doctorInfo.ProfilePhysical);
                    }

                    // Saves the new profile image in wwwroot/imgs
                    FormFileResult saveResult = await Profile.SaveImage(Environment.CurrentDirectory,
                        Guid.NewGuid().ToString());

                    // Checks the result of saving and if successful, fills doctor object
                    if (saveResult.Result)
                    {
                        doctorInfo.ProfileSrc = saveResult.SrcPath;
                        doctorInfo.ProfilePhysical = saveResult.PhysicalPath;
                        this.ProfileAddress = doctorInfo.ProfileSrc;
                    }
                    else
                    {
                        ModelState.AddModelError("", saveResult.Error);
                        this.SetPrompt("فرایند ذخیره سازی پروفایل با مشکل مواجه شد");
                        return Page();
                    }
                }

                // Checks if user has chosen any pictures
                if (!Pictures.IsNullOrEmpty())
                {
                    // PicturesSrc and PicturesPhysical may be null so we instantiate them here
                    if (doctorInfo.PicturesSrc.IsNullOrEmpty())
                    {
                        doctorInfo.PicturesSrc = new List<string>();
                        doctorInfo.PicturesPhysical = new List<string>();
                    }

                    int picCount = Pictures.Count();
                    // When user chooses more than 5 images, it removes the additional images
                    if (picCount >= 5)
                    {
                        int enteredDiff = picCount - 5;
                        Pictures.RemoveRange(0, enteredDiff);
                        picCount = picCount - enteredDiff;
                    }

                    // Calculates remained pics space and remove some pics if necessary to avoid exceeding 5 pics limit
                    int dbPicCount = doctorInfo.PicturesSrc.Count();
                    int remainedCount = 5 - dbPicCount;
                    if (picCount >= remainedCount)
                    {
                        int diff = picCount - remainedCount;
                        // Takes the first files to delete them
                        List<string> pathsToRemove = doctorInfo.PicturesPhysical.Take(diff).ToList();
                        foreach (var address in pathsToRemove)
                        {
                            FormFileExtensions.DeleteImage(address);
                        }

                        doctorInfo.PicturesSrc.RemoveRange(0, diff);
                        doctorInfo.PicturesPhysical.RemoveRange(0, diff);

                        this.PicturesAddresses.Clear();
                        this.PicturesAddresses.AddRange(doctorInfo.PicturesSrc);
                    }

                    // Loops through the chosen pictures
                    foreach (var picture in Pictures)
                    {
                        // Saves the chosen pictures
                        FormFileResult saveResult = await picture.SaveImage(Environment.CurrentDirectory, Guid.NewGuid().ToString());
                        if (saveResult.Result)
                        {
                            doctorInfo.PicturesSrc.Add(saveResult.SrcPath);
                            doctorInfo.PicturesPhysical.Add(saveResult.PhysicalPath);
                        }
                        else
                        {
                            ModelState.AddModelError("", saveResult.Error);

                            this.SetPrompt("فرایند ذخیره سازی عکس ها با مشکل مواجه شد");
                            return Page();
                        }
                    }
                }

                // Updates everything that happend in the database
                await _doctorRepository.UpdateAndSaveAsync(doctorInfo);
            }

            this.SetPrompt("ذخیره اطلاعات با موفقیت انجام شد");
            return Page();
        }
    }
}
