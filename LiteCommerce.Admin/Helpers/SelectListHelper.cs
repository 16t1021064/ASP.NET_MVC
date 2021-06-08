using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// cung cấp các hàm tiện ích liên quan đến selectListItem
    /// </summary>
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName
                });
            }
            return list;
        }
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "====Tất cả loại hàng===="
            });
            foreach (var item in ProductService.Categories())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName
                });
            }
            return list;

        }
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "====Tất cả nhà cung cấp===="
            });
            foreach (var item in ProductService.Suppliers())
            {
                
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName
                });
            }
            return list;

        }
        public static List<SelectListItem> OrderStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "====Chọn trạng thái===="
            });
            foreach (var item in OrderService.OrderStatuss())
            {

                list.Add(new SelectListItem()
                {
                    Value = item.Status.ToString(),
                    Text = item.Description
                });
            }
            return list;

        }
    }
}