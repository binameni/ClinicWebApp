using Clinic.Core.Domain.IdentityEntities;
using Clinic.Core.Domain.RepositoryContracts;
using ClinicWebApp.Pages.Shared;
using Dto.Other;
using Dto.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZarinPal.Class;

namespace ClinicWebApp.Pages.User.VisitReserver
{
    [Authorize(Roles = nameof(ApplicationRoles.Patient))]
    public class BuyVisitModel(IUserVisitRepository userVisitRepository) : PageModelWrapper
    {
        private readonly IUserVisitRepository _userVisitRepository = userVisitRepository;

        //private readonly Payment _payment;
        //private readonly Authority _authority;
        //private readonly Transactions _transactions;

        //public BuyVisitModel()
        //{
        //    var expose = new Expose();
        //    _payment = expose.CreatePayment();
        //    _authority = expose.CreateAuthority();
        //    _transactions = expose.CreateTransactions();
        //}

        [FromQuery]
        public int Id { get; set; }

        [FromQuery]
        public string SelectedDate { get; set; }

        [FromQuery]
        public string DoctorUserName { get; set; }

        public async Task<IActionResult> OnGetRequest()
        {
            //var result = await _payment.Request(new DtoRequest()
            //{
            //    Mobile = "09919685431",
            //    CallbackUrl = "https://localhost:44325/User/VisitReserver/BuyVisit?Handler=Validate",
            //    Description = "توضیحات",
            //    Email = "amir1382@gmail.com",
            //    Amount = 1000000,
            //    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
            //}, ZarinPal.Class.Payment.Mode.sandbox);
            //return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            return RedirectToPage("BuyVisit", pageHandler: "Validate", routeValues: new
            {
                Id = Id,
                SelectedDate = SelectedDate,
                DoctorUserName =DoctorUserName
            });
        }

        public async Task<IActionResult> OnGetValidate(/*string authority, string status*/)
        {
            //var verification = await _payment.Verification(new DtoVerification
            //{
            //    Amount = 1000000,
            //    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
            //    Authority = authority
            //}, Payment.Mode.sandbox);
            //return Page();
            this.SetPrompt("خرید با موفقیت انجام شد");
            return RedirectToPage("ReserveVisit", routeValues: new
            {
                Id = Id,
                SelectedDate = SelectedDate,
                DoctorUserName = DoctorUserName
            });
        }

        //public async Task<IActionResult> OnGetRefreshAuthority()
        //{
        //    var refresh = await _authority.Refresh(new DtoRefreshAuthority
        //    {
        //        Authority = "",
        //        ExpireIn = 1,
        //        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
        //    }, Payment.Mode.sandbox);
        //    return Page();
        //}

        //public async Task<IActionResult> OnGetUnverified()
        //{
        //    var refresh = await _transactions.GetUnverified(new DtoMerchant
        //    {
        //        MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
        //    }, Payment.Mode.sandbox);
        //    return Page();
        //}
    }
}
