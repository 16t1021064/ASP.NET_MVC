using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductPaginationQueryResult: BasePaginationQueryResult
    {
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public List<Product> Data { get; set; }
    }
}