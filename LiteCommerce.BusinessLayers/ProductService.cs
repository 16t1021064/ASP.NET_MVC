using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng nghiệp vụ liên quan đến quản lý hàng hóa.
    /// </summary>
    public static class ProductService
    {
        private static IProductDAL ProductDB;
        /// <summary>
        /// Khởi tạo tính năng tác nghiệp(hàm này phải được gọi nếu muốn sử dụng các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connnectionString"></param>
        public static void Init(DatabaseTypes dbType, string connnectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    ProductDB = new DataLayers.SQLServer.ProductDAL(connnectionString);
                    break;
                default:
                    throw new Exception("database is not supported");
            }
        }
        public static List<Product> List(int page, int pageSize, int categoryId, int supplierId, 
                                            string searchValue, out int rowCount)
        {
            rowCount = ProductDB.Count(categoryId, supplierId, searchValue);
            return ProductDB.List(page, pageSize, categoryId, supplierId, searchValue);
        }
        public static ProductEx GetEx(int productId)
        {
            return ProductDB.GetEx(productId);
        }
        public static Product Get(int productId)
        {
            return ProductDB.Get(productId);
        }
        public static int Add(Product data)
        {
            return ProductDB.Add(data);
        }
        public static bool Update(Product data)
        {
            return ProductDB.Update(data);
        }

        public static List<Supplier> Suppliers()
        {
            return ProductDB.Suppliers();
        }

        public static bool Delete(int productId)
        {
            return ProductDB.Delete(productId);
        }
        public static List<ProductAttribute> ListAttributes(int productId)
        {
            return ProductDB.ListAttributes(productId);
        }
        public static ProductAttribute GetAttribute(long attributeId)
        {
            return ProductDB.GetAttribute(attributeId);
        }
        public static long AddAttribute(ProductAttribute data)
        {
            return ProductDB.AddAttribute(data);
        }
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return ProductDB.UpdateAttribute(data);
        }
        public static void DeleteAttributes(long[] attributeIds)
        {
            foreach (var id in attributeIds){
                ProductDB.DeleteAttribute(id);
            }
        }
        public static List<ProductGallery> ListGalleries(int productId)
        {
            return ProductDB.ListGalleries(productId);
        }
        public static ProductGallery GetGallery(long galleryId)
        {
            return ProductDB.GetGallery(galleryId);
        }
        public static long AddGallery(ProductGallery data)
        {
            return ProductDB.AddGallery(data);
        }
        public static bool UpdateGallery(ProductGallery data)
        {
            return ProductDB.UpdateGallery(data);
        }
        public static void DeleteGallery(long[] galleryIds)
        {
            foreach (var id in galleryIds)
            {
                ProductDB.DeleteGallery(id);
            }
        }
        public static List<Category> Categories(int categoryID)
        {
            return ProductDB.Categories(categoryID);
        }
        public static List<Category> Categories()
        {
            return ProductDB.Categories();
        }
    }
}
