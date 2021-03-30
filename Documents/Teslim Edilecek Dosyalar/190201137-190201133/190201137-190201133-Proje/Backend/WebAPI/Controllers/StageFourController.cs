﻿using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageFourController : ControllerBase
    {
        private IIndexerService _indexerService;

        public StageFourController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpGet]
        public IActionResult Guide()
        {
            return Ok(new GuideDto
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
        public IActionResult UrlSimilarityWithSubCalculate(InputDto input)
        {
            input.webSitePool.ForEach(p => p = _indexerService.WebSiteCalculate(p).Data);
            return Ok(_indexerService.UrlSimilarityWithSubCalculate(_indexerService.WebSiteCalculate(input.webSite).Data, input.webSitePool));
        }
    }
}