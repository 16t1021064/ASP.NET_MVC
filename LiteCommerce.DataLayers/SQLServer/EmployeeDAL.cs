using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các tính năng xử lý dữ liệu nhân viên trong CSDL SQL Server
    /// </summary>
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }
        public int Add(Employee data)
        {
            int employeeID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into Employees(
	                                LastName, FirstName, BirthDate, Photo, Notes, Email, Password
                                    ) values(
	                                @LastName, @FirstName, @BirthDate, @Photo, @Notes, @Email, @Password
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                employeeID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return employeeID;
        }

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
                cmd.CommandText = @"Select count(*) from Employees
                                        where (@searchValue= '')
		                                    or(
			                                    LastName like @searchValue
			                                    or FirstName like @searchValue
			                                    or Email like @searchValue
			                                    )";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        public bool Delete(int employeeID)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from Employees
                                    where EmployeeID = @employeeID
	                                    AND not exists(
	                                    select * from Orders
		                                    where EmployeeID = Employees.EmployeeID
	                                    )";
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                isDeleted = cmd.ExecuteNonQuery() > 0;
            }
            return isDeleted;
        }

        public Employee Get(int employeeID)
        {
            Employee data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Employees where EmployeeID = @employeeID";
                cmd.Parameters.AddWithValue("@employeeID", employeeID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Password = Convert.ToString(dbReader["Password"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            BirthDate = Convert.ToDateTime(dbReader["BirthDate"])
                        };
                    }
                }
            }
            return data;
        }

        public List<Employee> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Employee> data = new List<Employee>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                    (
	                                    select *, ROW_NUMBER() OVER(Order by FirstName) As RowNumber
	                                    from Employees
	                                    where (@searchValue= '')
		                                    or(
			                                    EmployeeID Like @searchValue
			                                    or FirstName like @searchValue
			                                    or LastName like @searchValue
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
                        Employee employee = new Employee();
                        employee.EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]);
                        employee.LastName = Convert.ToString(dbReader["LastName"]);
                        employee.FirstName = Convert.ToString(dbReader["FirstName"]);
                        employee.BirthDate = Convert.ToDateTime(dbReader["BirthDate"]);
                        employee.Photo = Convert.ToString(dbReader["Photo"]);
                        employee.Notes = Convert.ToString(dbReader["Notes"]);
                        employee.Email = Convert.ToString(dbReader["Email"]);
                        employee.Password = Convert.ToString(dbReader["Password"]);
                        data.Add(employee);
                    }
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Employee data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Employees
                                    set LastName = @LastName,
	                                    FirstName =@FirstName,
                                        BirthDate =@BirthDate,
                                        Photo =@Photo,
                                        Notes =@Notes,
                                        Email =@Email,
                                        Password =@Password
	                                    Where EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);
                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);

                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
    }
}
