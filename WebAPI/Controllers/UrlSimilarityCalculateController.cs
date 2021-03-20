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
    public class UrlSimilarityCalculateController : ControllerBase
    {
        private IIndexerService _indexerService;

        public UrlSimilarityCalculateController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpGet]
        public IActionResult Guide()
        {
            return Ok(new guideDto
            {
                Website = new website
                {
                    Url = "http://www.example.com"
                },
                WebsitePool = new List<website>
                {
                    new website
                    {
                        Url="http://www.example.com"
                    },
                    new website
                    {
                        Url="http://www.example.com"
                    }
                }
            });
        }

        [HttpPost]
        public IActionResult UrlSimilarityCalculate(InputModelDto input)
        {
            return Ok(_indexerService.UrlSimilarityCalculate(input.webSite, input.webSitePool));
        }

    }
}