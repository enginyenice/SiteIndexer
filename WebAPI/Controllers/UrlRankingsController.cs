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
    public class UrlRankingsController : ControllerBase
    {
        private IIndexerService _indexerService;

        public UrlRankingsController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpPost("UrlRanking")]
        public IActionResult UrlRanking(UrlRankingControllerDtos urlRankingControllerDtos)
        {
            return Ok(_indexerService.UrlRanking(urlRankingControllerDtos.targetWebSite, urlRankingControllerDtos.pool));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new UrlRankingControllerDtos
            {
                targetWebSite = new WebSite
                {
                    Url = "http://www.google.com"
                },
                pool = new List<WebSite>
            {
                new WebSite
                {
                    Url="http://www.google.com"
                },
                new WebSite
                {
                    Url="http://www.google.com"
                }
            }
            });
        }
    }
}