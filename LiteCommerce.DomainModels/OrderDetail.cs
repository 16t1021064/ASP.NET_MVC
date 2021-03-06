using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Lớp hiển thị chi tiết đơn hàng
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrderDetailID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long SalePrice { get; set; }
    }
    
}
