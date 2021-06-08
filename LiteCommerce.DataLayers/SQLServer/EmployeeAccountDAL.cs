using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;
using System.Data;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// cài đặt liên quan đến tk nhân viên
    /// </summary>
    public class EmployeeAccountDAL : _BaseDAL, IAccountDAL
    {
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string loginName, string password)
        {
            Account data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select EmployeeID, FirstName, LastName, Email, Photo
                                    from Employees
                                    where Email = @loginName and Password=@password";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                cmd.Parameters.AddWithValue("@password", password);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Account()
                        {
                            UserName = dbReader["EmployeeID"].ToString(),
                            FullName = $"{dbReader["FirstName"]}{dbReader["LastName"]}",
                            Email = dbReader["Email"].ToString(),
                            Photo = dbReader["Photo"].ToString()
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
        public string getEmployeePhotoByEmail(string loginName)
        {
            string employeePhoto = "";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select Photo
                                    from Employees
                                    where Email = @loginName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                employeePhoto = cmd.ExecuteScalar().ToString();
                connection.Close();
            }
            return employeePhoto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string loginName, string oldPassword, string newPassword)
        {
            bool isChanged = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET Password = @newPassword
                                    WHERE Email= @loginName and Password =@oldPassword";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                cmd.Parameters.AddWithValue("@loginName", loginName);
                cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
                isChanged = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return isChanged;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public Account Get(string accountId)
        {
            throw new NotImplementedException();
        }

        public string getOldPaswword(string loginName)
        {
            string oldPassword = "";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select Password
                                    from Employees
                                    where Email = @loginName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@loginName", loginName);
                oldPassword = cmd.ExecuteScalar().ToString();
                connection.Close();
            }
            return oldPassword;
        }
    }
}
