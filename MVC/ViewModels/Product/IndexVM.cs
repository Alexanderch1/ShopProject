using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels.Product
{
    public class IndexVM
    {
        public FilterVM Filter { get; set; }
        public List<ProductVM> Items { get; set; }
        public pagerVM Pager { get; set; }
    }
}