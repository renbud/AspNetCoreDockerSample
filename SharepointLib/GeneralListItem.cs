using System;
using System.Collections.Generic;
using System.Text;

namespace SharepointLib
{
    public class GeneralListItem
    {
        public GeneralListItem() { }
        public GeneralListItem(string title, string details)
        {
            Title = title;
            Details = details;
        }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
