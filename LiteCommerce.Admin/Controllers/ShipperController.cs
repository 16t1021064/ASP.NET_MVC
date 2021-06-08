using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ShipperController : Controller
    {
        [Authorize]
        // GET: Shipper
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfShipper = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
            Models.ShipperPaginationQueryResult model = new Models.ShipperPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfShipper,
            };
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm nhà vận chuyển";
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Sửa thông tin nhà vận chuyển";
            var model = DataService.GetShipper(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Save(Shipper data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                {
                    ModelState.AddModelError("ShipperName", "Vui lòng nhập tên nhà vận chuyển");
                }
                if (string.IsNullOrEmpty(data.Phone))
                {
                    data.Phone = "";
                }
                if (!ModelState.IsValid)
                {
                    if (data.ShipperID == 0)
                        ViewBag.Title = "Thêm nhà vận chuyển";
                    else
                        ViewBag.Title = "Sửa thông tin nhà vận chuyển";
                    return View("Edit", data);
                }

                //return Json(data);
                if (data.ShipperID == 0)
                {
                    DataService.AddShipper(data);
                }
                else
                {
                    DataService.UpdateShipper(data);
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
                DataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetShipper(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
    }
}