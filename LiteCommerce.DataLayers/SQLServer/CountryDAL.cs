using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {

        }

        public List<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select * from Countries";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        //Country country = new Country();
                        //country.CountryName = Convert.ToString(dbReader["CountryName"]);
                        //data.Add(country);
                        //Country country = new Country()
                        //{
                        //    CountryName = Convert.ToString(dbReader["CountryName"])
                        //};
                        //data.Add(country);
                        data.Add(new Country()
                        {
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }

                cn.Close();
            }

            return data;
        }
    }
}
