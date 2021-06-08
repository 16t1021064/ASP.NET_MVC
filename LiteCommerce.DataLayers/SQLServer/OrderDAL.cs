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
    ///
    /// </summary>
    public class OrderDAL : _BaseDAL, IOrderDAL
    {
        public OrderDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Order data)
        {
            throw new NotImplementedException();
        }

        public int Count(int status, DateTime orderTime, int customerId, DateTime finishedTime, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  Count(*)
                                        FROM    Orders 
                                        WHERE   (@status = 0 OR Status = @status)
                                            AND  (@customerId = 0 OR CustomerID = @customerId)
                                            AND (@searchValue = '' OR CustomerID LIKE @searchValue)
                                            AND (OrderTime >= @orderTime OR @orderTime = '1980-1-1') 
                                            AND (FinishedTime >= @finishedTime OR @finishedTime = '1980-1-1')";
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@orderTime", orderTime.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@finishedTime", finishedTime.ToString("yyyy-MM-dd"));
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        public bool Delete(Order data)
        {
            throw new NotImplementedException();
        }

        public Order Get(int OrderID)
        {
            throw new NotImplementedException();
        }

        public List<Order> List(int page, int pageSize, int status, int customerId, DateTime orderTime, DateTime finishedTime, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Order> data = new List<Order>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY OrderID) AS RowNumber
                                        FROM    Orders 
                                        WHERE   (@status = 0 OR Status = @status)
                                            AND  (@customerId = 0 OR CustomerID = @customerId)
                                            AND (@searchValue = '' OR CustomerID LIKE @searchValue)
                                            AND (OrderTime >= @orderTime OR @orderTime = '1980-1-1') 
                                            AND (FinishedTime >= @finishedTime OR @finishedTime = '1980-1-1') 
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@orderTime", orderTime.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@finishedTime", finishedTime.ToString("yyyy-MM-dd"));

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Order()
                        {
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            AcceptTime = Convert.ToDateTime(dbReader["AcceptTime"]),
                            OrderTime = Convert.ToDateTime(dbReader["OrderTime"]),
                            ShippedTime = Convert.ToDateTime(dbReader["ShippedTime"]),
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FinishedTime = Convert.ToDateTime(dbReader["FinishedTime"]),
                            Status = Convert.ToInt32(dbReader["Status"]),
                        });
                    }
                }

                cn.Close();
            }

            return data;
        }

        public OrderEX GetOrderEX(int orderId)
        {
            throw new NotImplementedException();
        }
        public List<OrderDetail> ListOrderDetails(int orderId)
        {
            List<OrderDetail> data = new List<OrderDetail>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"Select * from OrderDetails where OrderID = @orderId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@orderId", orderId);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        var oderDetails = new OrderDetail()
                        {
                            OrderDetailID = Convert.ToInt32(dbReader["OrderDetailID"]),
                            OrderID = Convert.ToInt32(dbReader["OrderID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Quantity = Convert.ToInt32(dbReader["Quantity"]),
                            SalePrice = Convert.ToInt64(dbReader["SalePrice"])
                        };
                        data.Add(oderDetails);
                    }
                }

                cn.Close();
            }
            return data;
        }
        public List<OrderStatus> OrderStatuss()
        {
            return OrderStatuss(0);
        }
        public List<OrderStatus> OrderStatuss(int status)
        {
            List<OrderStatus> data = new List<OrderStatus>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from OrderStatus";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@status", status);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new OrderStatus()
                        {
                            Status = Convert.ToInt32(dbReader["Status"]),
                            Description = Convert.ToString(dbReader["Description"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }
        public bool Update(Order data)
        {
            throw new NotImplementedException();
        }
    }
}
