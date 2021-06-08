using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            var listOfEmployees = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);
            Models.EmployeePaginationQueryResult model = new Models.EmployeePaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfEmployees,
            };
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm anh nhân viên";
            Employee model = new Employee()
            {
                EmployeeID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Sửa thông tin anh nhân viên";
            var model = DataService.GetEmployee(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Save(Employee data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.FirstName))
                {
                    ModelState.AddModelError("FirstName", "Vui lòng tên anh nhân viên");
                }
                if (string.IsNullOrWhiteSpace(data.LastName))
                {
                    ModelState.AddModelError("LastName", "Vui lòng họ anh nhân viên");
                }
                
                if (string.IsNullOrEmpty(data.Photo))
                {
                    data.Photo = "";
                }
                if (string.IsNullOrEmpty(data.Notes))
                {
                    data.Notes = "";
                }
                if (string.IsNullOrEmpty(data.Email))
                {
                    data.Email = "";
                }
                if (string.IsNullOrEmpty(data.Password))
                {
                    data.Password = "";
                }
                if (!ModelState.IsValid)
                {
                    if (data.EmployeeID == 0)
                        ViewBag.Title = "Thêm anh nhân viên";
                    else
                        ViewBag.Title = "Sửa thông tin anh nhân viên";
                    return View("Edit", data);
                }
                if (data.EmployeeID == 0)
                {
                    DataService.AddEmployee(data);
                }
                else
                {
                    DataService.UpdateEmployee(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetEmployee(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
    }
}