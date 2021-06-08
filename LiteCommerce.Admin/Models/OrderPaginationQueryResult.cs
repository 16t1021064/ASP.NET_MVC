using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Order> Data { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime FinishedTime { get; set; }
        public int Status { get; set; }
    }
}