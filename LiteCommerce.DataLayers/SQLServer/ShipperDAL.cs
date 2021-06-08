using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class ShipperDAL : _BaseDAL, IShipperDAL
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {

        }

        public int Add(Shipper data)
        {
            int shipperID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into Shippers(
	                                 ShipperName, Phone
                                    ) values(
	                                @ShipperName, @Phone
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                shipperID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return shipperID;
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
                cmd.CommandText = @"Select count(*) from Shippers
                                        where (@searchValue= '')
		                                    or(
			                                    ShipperID like @searchValue
			                                    or ShipperName like @searchValue
			                                    or Phone like @searchValue
			                                    )";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        public bool Delete(int shipperID)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from Shippers
                                    where ShipperID = @shipperID
	                                    AND not exists(
	                                    select * from Orders
		                                    where ShipperID = Shippers.ShipperID
	                                    )";
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                isDeleted = cmd.ExecuteNonQuery() > 0;
            }
            return isDeleted;
        }

        public Shipper Get(int shipperID)
        {
            Shipper data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Shippers where ShipperID = @shipperID";
                cmd.Parameters.AddWithValue("@shipperID", shipperID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                        };
                    }

                }
            }
            return data;
        }

        public List<Shipper> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Shipper> data = new List<Shipper>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select ShipperID, ShipperName, Phone
                                    from
                                    (
	                                    select *, ROW_NUMBER() OVER(Order by ShipperName) As RowNumber
	                                    from Shippers
	                                    where (@searchValue= '')
		                                    or(
			                                    ShipperID Like @searchValue
			                                    or ShipperName like @searchValue
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
                        Shipper shipper = new Shipper();
                        shipper.ShipperID = Convert.ToInt32(dbReader["ShipperID"]);
                        shipper.ShipperName = Convert.ToString(dbReader["ShipperName"]);
                        shipper.Phone = Convert.ToString(dbReader["Phone"]);
                        data.Add(shipper);
                    }
                }
                cn.Close();
            }

            return data;
        }

        public bool Update(Shipper data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Shippers
                                    set ShipperName = @ShipperName,
	                                    Phone	=@Phone
	                                    Where ShipperID = @ShipperID
	                                    ";
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
    }
}
