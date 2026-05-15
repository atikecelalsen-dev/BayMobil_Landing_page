//using BayMobil_Landing_page.Helpers;
using BayMobil_Landing_page.Models;
using WhatsAppCloudApi;
using Microsoft.AspNetCore.Mvc;

namespace BayMobil_Landing_page.Controllers
{
    public class WhatsAppTestController : Controller
    {
        //private readonly WhatsAppHelper _whatsAppHelper;

        //public WhatsAppTestController(WhatsAppHelper whatsAppHelper)
        //{
        //    _whatsAppHelper = whatsAppHelper;
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return View(new WhatsAppTestModel());
        }
        //[HttpPost]
        //public async Task<IActionResult> Index(WhatsAppTestModel model)
        //{
        //    try
        //    {
        //        var sonuc = await _whatsAppHelper.Send(
        //            model.GsmNo,
        //            model.Mesaj,
        //            model.GonderilecekDosyaUrl
        //        );

        //        model.Success = sonuc.Success;
        //        model.StatusCode = sonuc.StatusCode;
        //        model.ApiResponse = sonuc.ResponseText;
        //    }
        //    catch (Exception ex)
        //    {
        //        model.Success = false;
        //        model.ApiResponse = ex.ToString();
        //    }

        //    return View(model);
        //}



        private readonly IConfiguration _configuration;

        public WhatsAppTestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Index(WhatsAppTestModel model)
        {
            WhatsAppHelper.AccessToken = _configuration["WhatsApp:AccessToken"];
            WhatsAppHelper.PhoneNumberId = _configuration["WhatsApp:PhoneNumberId"];

            WhatsAppResult sonuc = await WhatsAppHelper.Send(
                model.GsmNo,
                model.Mesaj,
                model.GonderilecekDosyaUrl
            );

            model.Success = sonuc.Success;
            model.StatusCode = sonuc.StatusCode;
            model.ApiResponse = sonuc.ResponseText ?? sonuc.ErrorMessage;

            return View(model);
        }
    }
}