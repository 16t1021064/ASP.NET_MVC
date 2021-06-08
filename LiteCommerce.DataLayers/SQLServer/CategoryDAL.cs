using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CategoryDAL : _BaseDAL, ICategoryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CategoryDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Category data)
        {
            int categoryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into Categories(
	                                 CategoryName, Description, ParentCategoryId
                                    ) values(
	                                @CategoryName, @Description, @ParentCategoryId
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
                categoryID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return categoryID;
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
                cmd.CommandText = @"Select count(*) from Categories
                                        where (@searchValue= '')
		                                    or(
			                                    CategoryID like @searchValue
			                                    or CategoryName like @searchValue
			                                    or Description like @searchValue
			                                    or ParentCategoryId like @searchValue
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
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        public bool Delete(int categoryID)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from Categories
                                    where CategoryID = @categoryID
	                                    AND not exists(
	                                    select * from Products
		                                    where CategoryID = Categories.CategoryID
	                                    )";
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                isDeleted = cmd.ExecuteNonQuery() > 0;
            }
            return isDeleted;
        }
        public Category Get(int categoryID)
        {
            Category data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select CategoryID, CategoryName,Description, ISNULL(ParentCategoryId, 0) as ParentCategoryId 
                                    from Categories where CategoryID = @categoryID";
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"])
                        };
                    }
                   
                }
            }
            return data;
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Category> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Category> data = new List<Category>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select CategoryID, CategoryName,Description, ISNULL(ParentCategoryId, 0) as ParentCategoryId
                                    from
                                    (
	                                    select *, ROW_NUMBER() OVER(Order by CategoryName) As RowNumber
	                                    from Categories
	                                    where (@searchValue= '')
		                                    or(
			                                    CategoryID Like @searchValue
			                                    or CategoryName like @searchValue
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
                        Category category = new Category();
                        category.CategoryID = Convert.ToInt32(dbReader["CategoryID"]);
                        category.CategoryName = Convert.ToString(dbReader["CategoryName"]);
                        category.Description = Convert.ToString(dbReader["Description"]);
                        category.ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"]);
                        data.Add(category);
                    }
                }
                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Category data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Categories
                                    set CategoryName = @CategoryName,
	                                    Description	=@Description,
	                                    ParentCategoryId =@ParentCategoryId
	                                    Where CategoryID = @CategoryID
	                                    ";
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);

                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
    }
}
