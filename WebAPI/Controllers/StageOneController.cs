using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageOneController : ControllerBase
    {
        private IIndexerService _indexerService;

        public StageOneController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpGet]
        public IActionResult Guide()
        {
            return Ok(new website
            {
                Url = "http://www.example.com"
            }
            );
        }

        [HttpPost]
        public IActionResult FrequencyCalculate(WebSite webSite)
        {
            var result = _indexerService.WebSiteCalculate(webSite).Data;
            FrequencyWebSiteDto frequencyWebSiteDto = new FrequencyWebSiteDto
            {
                Url = result.Url,
                Title = result.Title,
                Words = result.Words
            };

            return Ok(new SuccessDataResult<FrequencyWebSiteDto>(frequencyWebSiteDto));
        }
    }
}