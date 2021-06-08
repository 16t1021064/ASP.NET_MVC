using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public class OrderService
    {
        private static IOrderDAL OrderDB;
        /// <summary>
        /// Khởi tạo tính năng tác nghiệp(hàm này phải được gọi nếu muốn sử dụng các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connnectionString"></param>
        public static void Init(DatabaseTypes dbType, string connnectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    OrderDB = new DataLayers.SQLServer.OrderDAL(connnectionString);
                    break;
                default:
                    throw new Exception("database is not supported");
            }
        }
        public static List<Order> List(int page, int pageSize, int status, int customerId
                                    , DateTime orderTime, DateTime finishedTime, string searchValue, out int rowCount)
        {
            rowCount = OrderDB.Count(status, orderTime, customerId, finishedTime, searchValue);
            return OrderDB.List(page, pageSize, status, customerId, orderTime, finishedTime, searchValue);
        }
        public static int Add(Order data)
        {
            return OrderDB.Add(data);
        }
        public static bool Update(Order data)
        {
            return OrderDB.Update(data);
        }
        public static bool Delete(Order data)
        {
            return OrderDB.Delete(data);
        }
        public static List<OrderStatus> OrderStatuss()
        {
            return OrderDB.OrderStatuss();
        }
        public static OrderEX GetOrderEX(int orderId)
        {
            return OrderDB.GetOrderEX(orderId);
        }
    }
}
