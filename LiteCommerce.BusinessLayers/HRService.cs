using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// lớp cung cấp các chức năng tác nghiệp liên quan đến quản lý nhân sự
    /// </summary>
    public static class HRService
    {
        private static IEmployeeDAL EmployeeDB;
        /// <summary>
        /// khởi tạp tầng nghiệp vụ
        /// hàm này phải được gọi trước khi sử dụng các chức năng khác trong lớp
        /// </summary>
        /// <param name="dbType">Loại CSDL</param>
        /// <param name="connectionString">chuỗi tham số kết nối</param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch(dbType)
            {
                case DatabaseTypes.SQLServer:
                    EmployeeDB = new LiteCommerce.DataLayers.SQLServer.EmployeeDAL(connectionString);
                    break;
                case DatabaseTypes.FakeDB:
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        
    }
}
