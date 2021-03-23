﻿//Created By Engin Yenice
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
        [DisableRequestSizeLimit]
        
        public IActionResult UrlSimilarityWithSubCalculate(InputModelDto input)
        {
            input.webSitePool.ForEach(p => p = _indexerService.WebSiteCalculate(p).Data);
            return Ok(_indexerService.UrlSimilarityWithSubCalculate(_indexerService.WebSiteCalculate(input.webSite).Data, input.webSitePool));
        }

    }
}