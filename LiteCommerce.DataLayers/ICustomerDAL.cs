using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICustomerDAL
    {
        /// <summary>
        /// Bổ sung 1 anh khách hàng. hàm trả về mã anh khách hàng nếu bổ sung 
        /// thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của anh khách hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Customer data);
        /// <summary>
        /// lấy dánh sách các anh khách hàng(tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng hiện thị trên mỗi trang</param>
        /// <param name="searchValue">giá trị cần tìm theo CustomerID, CustomerName (chuỗi rỗng nếu không tìm kiếm)
        /// </param>
        /// <returns></returns>
        List<Customer> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng anh khách hàng thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue">giá trị cần tìm theo CustomerID, CustomerName
        /// (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một anh khách hàng theo mã.Trong trường hợp anh khách hàng không
        /// tồn tại, trả về giá trị null.
        /// </summary>
        /// <param name="CustomerID">Mã anh khách hàng cần lấy thông tin</param>
        /// <returns></returns>
        Customer Get(int CustomerID);
        /// <summary>
        /// Cập nhật thông tin cảu một anh khách hàng, hàm trả về giá trị boolean cho biết việc cập nhật 
        /// có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// xóa một anh khách hàng dựa vào mã. Hàm trả về giá trị bool cho biết có thực hiện được hay không
        /// (lưu ý: không được xóa anh khách hàng nếu đang có mặt hàng tham chiếu đến anh khách hàng)
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        bool Delete(int CustomerID);
    }
}
