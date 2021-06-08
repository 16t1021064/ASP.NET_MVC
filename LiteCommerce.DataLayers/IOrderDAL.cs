using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="CustomerID"></param>
        /// <param name="Status"></param>
        /// <param name="EmployeeID"></param>
        /// <param name="ShipperID"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Order> List(int Page, int PageSize, int Status, int ProductID, DateTime OrderTime, DateTime FinishedTime, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        Order Get(int OrderID);
        int Add(Order data);
        bool Update(Order data);
        bool Delete(Order data);
        int Count(int Status, DateTime OrderTime, int CustomerID, DateTime FinishedTime, string SearchValue);
        List<OrderStatus> OrderStatuss();
        OrderEX GetOrderEX(int OrderID);
        List<OrderDetail> ListOrderDetails(int OrderID);
    }
}
