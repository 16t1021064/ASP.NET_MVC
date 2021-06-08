using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AcceptTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ShippedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FinishedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Status { get; set; }
    }
    public class OrderEX : Order
    {
        public List<Product> ListProducts { get; set; }
    }
}
