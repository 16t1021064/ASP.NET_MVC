using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public interface IShipperDAL
    {
        /// <summary>
        /// Bổ sung 1 anh giao hàng. hàm trả về mã anh giao hàng nếu bổ sung 
        /// thành công.
        /// </summary>
        /// <param name="data">Đối tượng lưu thông tin của anh giao hàng cần bổ sung</param>
        /// <returns></returns>
        int Add(Shipper data);
        /// <summary>
        /// lấy dánh sách các anh giao hàng(tìm kiếm, phân trang)
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng hiện thị trên mỗi trang</param>
        /// <param name="searchValue">giá trị cần tìm theo ShipperID, ShipperName (chuỗi rỗng nếu không tìm kiếm)
        /// </param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số lượng anh giao hàng thỏa điều kiện tìm kiếm.
        /// </summary>
        /// <param name="searchValue">giá trị cần tìm theo ShipperID, ShipperName
        /// (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy thông tin của một anh giao hàng theo mã.Trong trường hợp anh giao hàng không
        /// tồn tại, trả về giá trị null.
        /// </summary>
        /// <param name="ShipperID">Mã anh giao hàng cần lấy thông tin</param>
        /// <returns></returns>
        Shipper Get(int ShipperID);
        /// <summary>
        /// Cập nhật thông tin cảu một anh giao hàng, hàm trả về giá trị boolean cho biết việc cập nhật 
        /// có thành công hay không?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);
        /// <summary>
        /// xóa một anh giao hàng dựa vào mã. Hàm trả về giá trị bool cho biết có thực hiện được hay không
        /// (lưu ý: không được xóa anh giao hàng nếu đang có mặt hàng tham chiếu đến anh giao hàng)
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        bool Delete(int ShipperID);
    }
}
