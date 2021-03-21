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
    public class SubUrlFinderController : ControllerBase
    {
        IIndexerService _indexerService;

        public SubUrlFinderController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpPost]
        public IActionResult SubUrlFinder(WebSite webSite)
        {

            return Ok(_indexerService.SubUrlFinder(_indexerService.WebSiteCalculate(webSite).Data));
        }
    }
}
