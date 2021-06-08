using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int customerId = 0, int status = 0, string searchValue = "",
                            string orderTime = "", string finishedTime = "", int page = 1)
        {
            DateTime OT = new DateTime();
            DateTime FT = new DateTime();
            int rowCount = 0;
            int pageSize = 10;
            if (!string.IsNullOrEmpty(orderTime))
            {
                OT = DateTime.Parse(orderTime);
            }
            else
            {
                OT = new DateTime(1980, 1, 1);
            }
            if (!string.IsNullOrEmpty(finishedTime))
            {
                FT = DateTime.Parse(finishedTime);
            }
            else
            {
                FT = new DateTime(1980, 1, 1);
            }

            var listOfOrders = OrderService.List(page, pageSize, status, customerId, OT, FT ,searchValue, out rowCount);
            Models.OrderPaginationQueryResult model = new Models.OrderPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                CustomerID = customerId,
                FinishedTime = FT,
                OrderTime = OT,
                RowCount = rowCount,
                Data = listOfOrders,
                SearchValue =searchValue,
                Status =status
            };
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var model = OrderService.GetOrderEX(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}