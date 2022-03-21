using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class BookController : Controller
    {
        IPenaltyCalculationService _penaltyCalculationService;
        ICountryService _countryService;
        public BookController(IPenaltyCalculationService penaltyCalculationService, ICountryService countryService)
        {
            _penaltyCalculationService = penaltyCalculationService;
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            IDataResult<List<Country>> result = _countryService.GetAll();
            if (result == null)
            {
                ViewBag.Error = "Failed" + Messages.CountriesReturnedNull;
            }
            return View();
        }

        public IActionResult CalculatePenalty(PenaltyCalculationDto penaltyCalculationDto)
        {
            IDataResult<CalculatedPenaltyDto> result = _penaltyCalculationService.CalculatePenalty(penaltyCalculationDto);
            if(!result.Success)
            {
                ViewBag.Error = "Failed" + result.Message;
                return View("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        public JsonResult GetCountries()
        {
            IDataResult<List<Country>> result = _countryService.GetAll();
            if(result == null)
            {
                return new JsonResult(new { data = "null" });
            }
            return new JsonResult(new { data = result.Data });
        }
    }
}
