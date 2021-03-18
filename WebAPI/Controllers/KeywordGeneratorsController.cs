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
    public class KeywordGeneratorsController : ControllerBase
    {
        private IIndexerService _indexerService;
        public KeywordGeneratorsController(IIndexerService indexerService)
        {
            _indexerService = indexerService;
        }

        [HttpPost]
        public IActionResult GetKeywords(WebSite webSite)
        {

            var result = _indexerService.KeywordGenerator(webSite);
            WebSiteKeywordDto webSiteKeywordDto = new WebSiteKeywordDto
            {
                Keywords = result.Data.Keywords,
                Title = result.Data.Title,
                Url = result.Data.Url
            };


            return Ok(new SuccessDataResult<WebSiteKeywordDto>(webSiteKeywordDto));
            
        }
    }
}
