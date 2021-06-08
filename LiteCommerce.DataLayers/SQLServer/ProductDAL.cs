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
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ProductDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into Products(
	                                ProductName, SupplierID, CategoryID, Unit, Price, Photo
                                    ) values(
	                                @ProductName, @SupplierID, @CategoryID, @Unit, @Price, @Photo
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                productID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return productID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            int attributeID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into ProductAttributes(
	                                AttributeName,ProductID, AttributeValue, DisplayOrder 
                                    ) values(
	                                @AttributeName,@ProductID, @AttributeValue, @DisplayOrder
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                attributeID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }

            return attributeID;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddGallery(ProductGallery data)
        {
            int galleryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"insert into ProductGallery(
	                                ProductID, Photo, Description, DisplayOrder, IsHidden
                                    ) values(
	                                @ProductID, @Photo, @Description, @DisplayOrder, @IsHidden
                                             );
                                Select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                galleryID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return galleryID;
        }

        public int Count(int categoryId, int supplierId, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT  count(*)
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                                        AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool Delete(int productId)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from Products
                                    where ProductID = @productId
	                                    AND not exists(
	                                    select * from ProductGallery
		                                    where ProductID = Products.ProductID
	                                    )AND not exists(
	                                    select * from ProductAttributes
		                                    where ProductID = Products.ProductID
	                                    )AND not exists(
	                                    select * from OrderDetails
		                                    where ProductID = Products.ProductID
	                                    )";
                cmd.Parameters.AddWithValue("@productId", productId);
                isDeleted = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return isDeleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeId)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from ProductAttributes
                                    where AttributeID = @attributeId";
                cmd.Parameters.AddWithValue("@attributeId", attributeId);
                isDeleted = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return isDeleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public bool DeleteGallery(long galleryId)
        {
            bool isDeleted = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"delete from ProductGallery
                                    where GalleryID = @galleryId";
                cmd.Parameters.AddWithValue("@galleryId", galleryId);
                isDeleted = cmd.ExecuteNonQuery() > 0;
                cn.Close();
            }
            return isDeleted;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product Get(int productId)
        {
            Product data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from Products where ProductID = @productId";
                cmd.Parameters.AddWithValue("@productId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"])
                        };
                    }
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeId)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from ProductAttributes where AttributeID = @attributeId";
                cmd.Parameters.AddWithValue("@attributeId", attributeId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        };
                    }
                }
                cn.Close();
            }
            return data;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductEx GetEx(int productId)
        {
            ProductEx data = new ProductEx();
            List<ProductAttribute> listOfAttribute = ListAttributes(productId);
            List<ProductGallery> listOfGallery = ListGalleries(productId);
            data.Attributes = listOfAttribute;
            data.Galleries = listOfGallery;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT *
		                            from Products Where ProductID = @productId";
                cmd.Parameters.AddWithValue("@productId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data.CategoryID = Convert.ToInt32(dbReader["CategoryID"]);
                        data.Photo = Convert.ToString(dbReader["Photo"]);
                        data.Price = Convert.ToDecimal(dbReader["Price"]);
                        data.SupplierID = Convert.ToInt32(dbReader["SupplierID"]);
                        data.Unit = Convert.ToString(dbReader["Unit"]);
                        data.ProductName = Convert.ToString(dbReader["ProductName"]);
                        data.ProductID = Convert.ToInt32(dbReader["ProductID"]);
                    }
                }
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public ProductGallery GetGallery(long galleryId)
        {
            ProductGallery data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Select * from ProductGallery where GalleryID = @galleryId";
                cmd.Parameters.AddWithValue("@galleryId", galleryId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToBoolean(dbReader["IsHidden"])
                        };
                    }
                }
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="CategoryId"></param>
        /// <param name="SupplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Product> List(int Page, int PageSize, int CategoryId, int SupplierId, string searchValue)
        {
            if (searchValue != "")
            {
                searchValue = "%" + searchValue + "%";
            }
            List<Product> data = new List<Product>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT  *
                                    FROM
                                    (
                                        SELECT  *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM    Products 
                                        WHERE   (@categoryId = 0 OR CategoryId = @categoryId)
                                        AND  (@supplierId = 0 OR SupplierId = @supplierId)
                                            AND (@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Page", Page);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);
                cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            Unit = Convert.ToString(dbReader["Unit"]),
                        });
                    }
                }

                cn.Close();
            }

            return data;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductAttribute> ListAttributes(int productId)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT  *
                                    FROM ProductAttributes 
                                    where ProductID = @productId 
                                    order by DisplayOrder";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductGallery> ListGalleries(int productId)
        {
            List<ProductGallery> data = new List<ProductGallery>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = @"SELECT  *
                                    FROM ProductGallery
                                    where ProductID = @productId
                                    order by DisplayOrder";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", productId);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToBoolean(dbReader["IsHidden"])
                        });
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
        public bool Update(Product data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update Products
                                    set ProductName = @ProductName,
	                                    SupplierID	=@SupplierID,
	                                    CategoryID =@CategoryID,
	                                    Unit = @Unit,
	                                    Price = @Price,
	                                    Photo = @Photo
	                                    Where ProductID = @ProductID
	                                    ";
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update ProductAttributes
                                    set ProductID = @ProductID,
	                                    AttributeName	=@AttributeName,
	                                    AttributeValue =@AttributeValue,
	                                    DisplayOrder = @DisplayOrder
	                                    Where AttributeID = @AttributeID
	                                    ";
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>     
        public bool UpdateGallery(ProductGallery data)
        {
            bool isUpdated = false;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"Update ProductGallery
                                    set ProductID = @ProductID,
	                                    Photo	=@Photo,
	                                    Description =@Description,
	                                    DisplayOrder = @DisplayOrder,
                                        IsHidden = @IsHidden
	                                    Where GalleryID = @GalleryID
	                                    ";
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                isUpdated = cmd.ExecuteNonQuery() > 0;
            }
            return isUpdated;
        }
        public List<Category> Categories()
        {
            return Categories(0);
        }
        public List<Category> Categories(int categoryID)
        {
            List<Category> data = new List<Category>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Categories 
                                    where @categoryID = '' Or CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Category()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"])
                        });
                    }
                }

                cn.Close();
            }

            return data;
        }
        public List<Supplier> Suppliers()
        {
            return Suppliers(0);
        }
        public List<Supplier> Suppliers(int supplierID)
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from Suppliers 
                                    where @supplierID = '' Or SupplierID = @supplierID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"])
                        });
                    }
                }
                cn.Close();
            }

            return data;
        }
    }
}
