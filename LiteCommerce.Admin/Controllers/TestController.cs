using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System.Configuration;
using LiteCommerce.DataLayers.SQLServer;

namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["litecommercedb"].ConnectionString;
            //ICityDAL dal = new CityDAL(connectionstring);
            //var data = dal.List();
            //ISupplierDAL dal = new SupplierDAL(connectionstring);
            //var data = dal.Count("11");
            //ISupplierDAL dal = new SupplierDAL(connectionstring);
            //var data = dal.Get(11);
            //ISupplierDAL dal = new SupplierDAL(connectionstring);
            //Supplier s = new Supplier
            //{
            //    SupplierName = "Võ Văn Huy",
            //    ContactName = "hihi",
            //    Address = "Quảng Trị",
            //    City = "Huế",
            //    PostalCode = "123",
            //    Country="Việt Nam",
            //    Phone="123456"
            //};
            //var data = dal.Add(s);
            //ISupplierDAL dal = new SupplierDAL(connectionstring);
            //var data = dal.Delete(32);
            
            ICategoryDAL dal = new CategoryDAL(connectionstring);
            var data = dal.Count("");
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Pagination(int page, int pageSize,String searchValue)
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            //ISupplierDAL dal = new SupplierDAL(connectionString);
            //var data = dal.List(page, pageSize, searchValue);
            //return Json(data, JsonRequestBehavior.AllowGet);
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            ICategoryDAL dal = new CategoryDAL(connectionString);
            var data = dal.List(page, pageSize, searchValue);
            return Json(data, JsonRequestBehavior.AllowGet);
            //string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            //IShipperDAL dal = new ShipperDAL(connectionString);
            //var data = dal.List(page, pageSize, searchValue);
            //return Json(data, JsonRequestBehavior.AllowGet);
            //string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            //ICustomerDAL dal = new CustomerDAL(connectionString);
            //var data = dal.List(page, pageSize, searchValue);
            //return Json(data, JsonRequestBehavior.AllowGet);
            //string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            //IEmployeeDAL dal = new EmployeeDAL(connectionString);
            //var data = dal.List(page, pageSize, searchValue);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }
        // test URL: hTTP://localhost:/Test/Pagination?page=2&pageSize=10&SearchValue=
    }
}