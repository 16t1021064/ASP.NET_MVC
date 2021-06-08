using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfCategories = DataService.ListCategories(page, 10, searchValue, out rowCount);
            Models.CategoryPaginationQueryResult model = new Models.CategoryPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfCategories
            };
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm nhà loại hàng";
            Category model = new Category()
            {
                CategoryID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.title = "Sửa thông tin loại hàng";
                var model = DataService.GetCategory(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
            catch
            {
                return RedirectToAction("google.com.vn");
            }
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetCategory(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        public ActionResult Save(Category data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Vui lòng nhập tên loại hàng");
                }
                if (string.IsNullOrEmpty(data.Description))
                {
                    data.Description = "";
                }
                if (data.ParentCategoryId == null)
                {
                    data.ParentCategoryId = 0;
                }
                if (!ModelState.IsValid)
                {
                    if (data.CategoryID == 0)
                        ViewBag.Title = "Thêm loại hàng";
                    else
                        ViewBag.Title = "Sửa thông tin loại hàng";
                    return View("Edit", data);
                }

                //return Json(data);
                if (data.CategoryID == 0)
                {
                    DataService.AddCategory(data);
                }
                else
                {
                    DataService.UpdateCategory(data);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Trang này hình như không tồn tại");
            }
        }
    }
}