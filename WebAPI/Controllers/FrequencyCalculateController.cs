//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrequencyCalculateController : ControllerBase
    {
        private IIndexerService _indexerService;

        public FrequencyCalculateController(IIndexerService indexerService)
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