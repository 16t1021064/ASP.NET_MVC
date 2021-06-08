using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CustomerDAL : _BaseDAL, ICustomerDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int customerID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into Customers(
	                                 CustomerName, ContactName, Address, City, PostalCode, Country, Email, Password
                                    ) values(
	                                @CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @Password
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                customerID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return customerID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select count(*) from Customers
                                        where (@searchValue= '')
		                                    or(
			                                    CustomerName like @searchValue
			                                    or ContactName like @searchValue
			                                    or Address like @searchValue
			                                    or Email like @searchValue
			                                    )";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public bool Delete(int CustomerID)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from Customers
                                    where CustomerID = @CustomerID
	                                    AND not exists(
	                                    select * from Orders
		                                    where CustomerID = Customers.CustomerID
	                                    )";
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
                isDeleted = cmd.ExecuteNonQuery() > 0;
            }
            return isDeleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public Customer Get(int customerID)
        {
            Customer data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Customers where CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])
                        };
                    }
                }
            }

            return data;
        }

        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Customer> data = new List<Customer>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                    (
	                                    select *, ROW_NUMBER() OVER(Order by CustomerName) As RowNumber
	                                    from Customers
	                                    where (@searchValue= '')
		                                    or(
			                                    CustomerID Like @searchValue
			                                    or ContactName like @searchValue
			                                    or Address like @searchValue
			                                    or Email like @searchValue
			                                    )
                                    ) as s 
                                    where s.RowNumber between (@page -1)*@pageSize + 1 and @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = Convert.ToInt32(dbReader["CustomerID"]);
                        customer.CustomerName = Convert.ToString(dbReader["CustomerName"]);
                        customer.ContactName = Convert.ToString(dbReader["ContactName"]);
                        customer.Address = Convert.ToString(dbReader["Address"]);
                        customer.City = Convert.ToString(dbReader["City"]);
                        customer.PostalCode = Convert.ToString(dbReader["PostalCode"]);
                        customer.Country = Convert.ToString(dbReader["Country"]);
                        customer.Email = Convert.ToString(dbReader["Email"]);
                        customer.Password = Convert.ToString(dbReader["Password"]);
                        data.Add(customer);
                    }
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Customer data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Customers
                                    set CustomerName = @CustomerName,
	                                    ContactName	=@ContactName,
	                                    Address =@Address,
	                                    City = @City,
	                                    PostalCode = @PostalCode,
	                                    Country = @Country,
	                                    Email = @Email,
                                        Password = @Password
	                                    Where CustomerID = @CustomerID
	                                    ";
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);

                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
    }
}
