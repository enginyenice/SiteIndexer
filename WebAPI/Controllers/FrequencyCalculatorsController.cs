//Created By Engin Yenice
//enginyenice2626@gmail.com

using Business.Abstract;
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
    public class FrequencyCalculatorsController : ControllerBase
    {
        private IIndexerService _indexerService;

        public FrequencyCalculatorsController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpPost]
        public IActionResult FrequanceCalculate(WebSite webSite)
        {
            var result = _indexerService.FrequanceCalculate(webSite).Data;
            FrequencyWebSiteDto frequencyWebSiteDto = new FrequencyWebSiteDto
            {
                Frequances = result.Frequances,
                Title = result.Title,
                Url = result.Url
            };
            return Ok(frequencyWebSiteDto);
        }
    }
}