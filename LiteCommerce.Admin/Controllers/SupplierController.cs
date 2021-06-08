using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class SupplierController : Controller
    {
        [Authorize]
        // GET: Supplier
        public ActionResult Index()
        {   
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfSuppliers = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
            Models.SupplierPaginationQueryResult model = new Models.SupplierPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfSuppliers,
            };
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm nhà cung cấp";
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Sửa thông tin nhà cung cấp";
            var model = DataService.GetSupplier(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Save(Supplier data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                {
                    ModelState.AddModelError("SupplierName", "Vui lòng nhập tên nhà cung cấp");
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
                if (string.IsNullOrEmpty(data.Phone))
                {
                    data.Phone = "";
                }
                if (!ModelState.IsValid)
                {
                    if (data.SupplierID == 0)
                        ViewBag.Title = "Thêm nhà cung cấp";
                    else
                        ViewBag.Title = "Sửa thông tin nhà cung cấp";
                    return View("Edit", data);
                }

                //return Json(data);
                if (data.SupplierID == 0)
                {
                    DataService.AddSupplier(data);
                }
                else
                {
                    DataService.UpdateSupplier(data);
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
                DataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetSupplier(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
    }
}