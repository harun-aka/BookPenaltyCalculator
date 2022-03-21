using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (result != null)
            {
                List<SelectListItem> countries = (from country in result.Data
                                                  select new SelectListItem
                                                  {
                                                      Text = country.Name,
                                                      Value = country.Id.ToString()
                                                  }
                                                  ).ToList();
                ViewBag.Countries = countries;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Result(PenaltyCalculationDto penaltyCalculationDto)
        {
            IDataResult<CalculatedPenaltyDto> result = _penaltyCalculationService.CalculatePenalty(penaltyCalculationDto);
            if (!result.Success)
            {
                ViewBag.Error = "Failed" + result.Message;
                return View("Index");
            }

            result.Data.Penalty = Math.Round(result.Data.Penalty, 2);
            return PartialView("_Result", result.Data);
        }

    }
}
