using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// lớp biễu diễn dữ liệu nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Photo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Notes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Password { get; set; }
    }
}
