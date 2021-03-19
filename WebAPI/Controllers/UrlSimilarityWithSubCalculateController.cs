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
    public class UrlSimilarityWithSubCalculateController : ControllerBase
    {
        private IIndexerService _indexerService;

        public UrlSimilarityWithSubCalculateController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpGet]
        public IActionResult Guide()
        {
            return Ok(new InputModelDto
            {
                webSite = new WebSite
                {
                    Url = "http://www.example.com"
                },
                webSitePool = new List<WebSite>
                {
                    new WebSite
                    {
                        Url="http://www.example.com"
                    },
                    new WebSite
                    {
                        Url="http://www.example.com"
                    }
                }
            });
        }

        [HttpPost]
        public IActionResult UrlSimilarityWithSubCalculate(InputModelDto input)
        {
            return Ok(_indexerService.UrlSimilarityWithSubCalculate(input.webSite, input.webSitePool).Data);
        }

    }
}