using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordCalculateController : Controller
    {
        private IIndexerService _indexerService;

        public KeywordCalculateController(IIndexerService indexerService)
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
            KeywordWebSiteDto keywordWebSiteDto = new KeywordWebSiteDto
            {
                Url = result.Url,
                Title = result.Title,
                Keywords = result.Keywords
            };

            return Ok(new SuccessDataResult<KeywordWebSiteDto>(keywordWebSiteDto));
        }
    }
}
