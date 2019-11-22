using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SharepointLib
{
    public interface ISharepointRepository
    {
        public Task<IEnumerable<GeneralListItem>> GetGeneralListItemsAsync();
    }
}
