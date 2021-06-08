using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {
        /// <summary>
        /// Bổ sung 1 loại hàng. hàm trả về mã loại hàng nếu bổ sung 
        /// thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của loại hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Category data);
        /// <summary>
        /// lấy dánh sách các loại hàng(tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng hiện thị trên mỗi trang</param>
        /// <param name="searchValue">giá trị cần tìm theo CategoryName (chuỗi rỗng nếu không tìm kiếm)
        /// </param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng loại hàng thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue">giá trị cần tìm theo CategoryName
        /// (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một loại hàng theo mã.Trong trường hợp loại hàng không
        /// tồn tại, trả về giá trị null.
        /// </summary>
        /// <param name="CategoryID">Mã loại hàng cần lấy thông tin</param>
        /// <returns></returns>
        Category Get(int CategoryID);
        /// <summary>
        /// Cập nhật thông tin cảu một loại hàng, hàm trả về giá trị boolean cho biết việc cập nhật 
        /// có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);
        /// <summary>
        /// xóa một loại hàng dựa vào mã. Hàm trả về giá trị bool cho biết có thực hiện được hay không
        /// (lưu ý: không được xóa loại hàng nếu đang có mặt hàng tham chiếu đến loại hàng)
        /// </summary>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        bool Delete(int CategoryID);
    }
}
