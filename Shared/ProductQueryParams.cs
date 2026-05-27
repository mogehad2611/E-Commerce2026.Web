using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        private const int Default = 5;
        private const int Max = 10;


        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions sortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;



        private int pageSize = Default;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > Max ? Max : value; }
        }

    }
}
