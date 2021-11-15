using orchard.cms.web.Models.POC;
using Microsoft.AspNetCore.Mvc;
using OrchardCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrchardCore.Data;

namespace orchard.cms.web.Controllers
{
    public class POCController : Controller
    {
        private readonly IOrchardHelper _orchard;
        private readonly IDbConnectionAccessor _dbAccessor;

        public POCController (
            IOrchardHelper orchard,
            IDbConnectionAccessor dbAccessor
            )
        {
            _orchard = orchard;
            _dbAccessor = dbAccessor;
        }

        //[HttpGet]
        //[Route("POC/LiteratureView/{alias}")]
        //public async Task<IActionResult> LiteratureViewAsync(string alias)
        //{
        //    //var conn = _dbAccessor.CreateConnection().ConnectionString;

        //    var sd = await _getContent.GetSeriesDescriptionAsync(alias);

        //    var model = new LiteratureViewModel()
        //    {
        //       LitItem = await _orchard.GetContentItemByIdAsync(alias)
               
        //    };

        //    var htmlBody = model.LitItem.Content.HtmlBodyPart.Html;

        //    return View(model);
        //}

        //[HttpGet]
        //[Route("POC/SeriesView")]
        //public async Task<IActionResult> SeriesViewAsync()
        //{
        //    //var model = await _getContent.GetSeriesDescriptionAsync("4ssnyxtc9qkqy6kzcxcjqg7zwt");
        //    var model = await _getContent.GetSeriesDescriptionAsync("405qkc7a91gv4znb8rmvtmhtw8");
        //    return View(model);
        //}

    }
}
