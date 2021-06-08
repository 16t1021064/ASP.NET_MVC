using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// định nghĩa các phép xử lý lq đến hàng hóa
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <param name="CategoryId">Mã loại hàng (0 nếu không lọc theo loại hàng)</param>
        /// <param name="SupplierId">Mã nhà cung cấp (0 nếu không lọc theo loại hàng)</param>
        /// <param name="searchValue">chuỗi rỗng nếu không tìm kiếm
        /// </param>
        /// <returns></returns>
        List<Product> List(int Page, int PageSize, int CategoryId, int SupplierId, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product Get(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductEx GetEx(int productId);
        /// <summary>
        /// Bổ sung 1 mặt hàng mới (hàm trả về nếu bổ sung thành công , trả về 0 nếu bổ sung k thành công)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);
        /// <summary>
        /// Cập nhật thông tin mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);
        /// <summary>
        /// Xóa mặt hàng(xóa thuộc tính và thư viện ảnh của mặt hàng)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int productId);
        List<Supplier> Suppliers();

        /// <summary>
        /// lấy danh sách cách thuộc tính của 1 product (sắp xếp theo displayorders)
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        ProductAttribute GetAttribute(long attributeId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long attributeId);
        List<ProductGallery> ListGalleries(int productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        ProductGallery GetGallery(long galleryId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddGallery(ProductGallery data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateGallery(ProductGallery data);
        List<Category> Categories(int categoryID);
        List<Category> Categories();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        bool DeleteGallery(long galleryId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="supplierId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(int categoryId, int supplierId, string searchValue);
    }
}
