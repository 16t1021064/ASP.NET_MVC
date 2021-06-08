using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Bổ sung 1 anh nhân viên. hàm trả về mã anh nhân viên nếu bổ sung 
        /// thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của anh nhân viên cần bổ sung</param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// lấy dánh sách các anh nhân viên(tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng hiện thị trên mỗi trang</param>
        /// <param name="searchValue">giá trị cần tìm theo EmployeeID, EmployeeName (chuỗi rỗng nếu không tìm kiếm)
        /// </param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng anh nhân viên thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue">giá trị cần tìm theo EmployeeID, EmployeeName
        /// (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một anh nhân viên theo mã.Trong trường hợp anh nhân viên không
        /// tồn tại, trả về giá trị null.
        /// </summary>
        /// <param name="EmployeeID">Mã anh nhân viên cần lấy thông tin</param>
        /// <returns></returns>
        Employee Get(int EmployeeID);
        /// <summary>
        /// Cập nhật thông tin cảu một anh nhân viên, hàm trả về giá trị boolean cho biết việc cập nhật 
        /// có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// xóa một anh nhân viên dựa vào mã. Hàm trả về giá trị bool cho biết có thực hiện được hay không
        /// (lưu ý: không được xóa anh nhân viên nếu đang có mặt hàng tham chiếu đến anh nhân viên)
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        bool Delete(int EmployeeID);
    }
}
