using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến thành phố
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// lấy danh sách tất cả các thành phố
        /// </summary>
        /// <returns></returns>
        List<City> List();
        /// <summary>
        /// lấy danh sách các thành phố thuộc một quốc gia
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        List<City> List(String countryName);
    }
}
