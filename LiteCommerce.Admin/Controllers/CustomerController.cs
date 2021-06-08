using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class CustomerController : Controller
    {
        [Authorize]
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfCustomers = DataService.ListCustomers(page, 10, searchValue, out rowCount);
            Models.CustomerPaginationQueryResult model = new Models.CustomerPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfCustomers
            };
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Sửa thông tin khách hàng";
            var model = DataService.GetCustomer(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Save(Customer data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                {
                    ModelState.AddModelError("CustomerName", "Vui lòng nhập tên khách hàng");
                }
                if (string.IsNullOrWhiteSpace(data.ContactName))
                {
                    ModelState.AddModelError("ContactName", "Vui lòng nhập tên liên hệ");
                }
                if (string.IsNullOrEmpty(data.Address))
                {
                    data.Address = "";
                }
                if (string.IsNullOrEmpty(data.Country))
                {
                    data.Country = "";
                }
                if (string.IsNullOrEmpty(data.City))
                {
                    data.City = "";
                }
                if (string.IsNullOrEmpty(data.PostalCode))
                {
                    data.PostalCode = "";
                }
                if (string.IsNullOrEmpty(data.Email))
                {
                    data.Email = "";
                }
                if (string.IsNullOrEmpty(data.Password))
                {
                    data.Password = "1";
                }
                if (!ModelState.IsValid)
                {
                    if (data.CustomerID == 0)
                        ViewBag.Title = "Thêm khách hàng";
                    else
                        ViewBag.Title = "Sửa thông tin khách hàng";
                    return View("Edit", data);
                }
                
                if (data.CustomerID == 0)
                {
                    DataService.AddCustomer(data);
                }
                else
                {
                    DataService.UpdateCustomer(data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Trang này hình như không tồn tại");
            }
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                DataService.GetCustomer(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetCustomer(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
    }
}