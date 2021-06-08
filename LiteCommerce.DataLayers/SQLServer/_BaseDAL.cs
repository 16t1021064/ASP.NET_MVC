using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    ///  
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// chuỗi tham số kết nối đến cơ sở dữ liệu
        /// </summary>
        protected string connnectionString;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connnectionString"></param>
        public _BaseDAL(string connnectionString)
        {
            this.connnectionString = connnectionString;
        }
        protected SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this.connnectionString;
            connection.Open();
            return connection;
        }
    }
}
