using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharepointLib;

namespace AspNetCoreWindowsSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SharePointController : ControllerBase
    {

        private readonly ILogger<SharePointController> _logger;
        private readonly ISharepointRepository _spRepo;

        public SharePointController(ILogger<SharePointController> logger, ISharepointRepository spRepo)
        {
            _logger = logger;
            _spRepo = spRepo;
        }

        [HttpGet]
        public IEnumerable<GeneralListItem> Get()
        {
            var items = _spRepo.GetGeneralListItemsAsync().Result;
            return items.ToArray();
        }
    }
}
