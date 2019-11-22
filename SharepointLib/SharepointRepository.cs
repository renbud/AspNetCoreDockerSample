using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharepointLib
{
    public class SharepointRepository : ISharepointRepository
    {
        ClientContext _ctx;
        private readonly ILogger _logger;
        public SharepointRepository(IConfiguration configuration,  ILogger<SharepointRepository> logger)
        {
            _logger = logger;
            _ctx = new ClientContextFactory(configuration).GetClientContext();
        }

        public SharepointRepository(IConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _ctx = new ClientContextFactory(configuration).GetClientContext();
        }

        public async Task<IEnumerable<GeneralListItem>> GetGeneralListItemsAsync()
        {
            const string ListTitle = "General Information";
            Web site = _ctx.Web;
            var targetList = site.Lists.GetByTitle(ListTitle);

            var query = new CamlQuery();
            var collListItem = targetList.GetItems(query);

            _ctx.Load(collListItem);
            await _ctx.ExecuteQueryAsync();
            _logger.LogInformation($"{collListItem.Count} items in {ListTitle}");

            var lst = new List<GeneralListItem>();
            foreach (ListItem targetListItem in collListItem)
            {
                var item = new GeneralListItem(
                        targetListItem["Title"].ToString(),
                        targetListItem["Details"].ToString());
                lst.Add(item);
            }

            return lst;
        }
    }
}
