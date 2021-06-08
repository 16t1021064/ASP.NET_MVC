using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public class AccountService
    {
        private static IAccountDAL AccountDB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <param name="accountType"></param>
        public static void Init(DatabaseTypes dbType, string connectionString, AccountTypes accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if(accountType == AccountTypes.Employee)
                        AccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
                    else
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        public static Account Authorize(string loginName, string password)
        {
            return AccountDB.Authorize(loginName, password);
        }
        public static string getEmployeePhotoByEmail(string loginName)
        {
            return AccountDB.getEmployeePhotoByEmail(loginName);
        }
        public static string getOldPaswword(string loginName)
        {
            return AccountDB.getOldPaswword(loginName);
        }
        public static bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            return AccountDB.ChangePassword(accountId, oldPassword, newPassword);
        }
        public static Account Get(string accountId)
        {
            return AccountDB.Get(accountId);
        }
    }
    /// <summary>
    /// Định nghĩa các loại tài khoản
    /// </summary>
    public enum AccountTypes
    {
        /// <summary>
        /// Tk nhân viên
        /// </summary>
        Employee,
        /// <summary>
        /// Tk Khách hàng
        /// </summary>
        Customer
    }
}
