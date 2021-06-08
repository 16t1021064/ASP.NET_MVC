using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult List(int categoryId = 0, int supplierId = 0, string searchValue ="", int page =1)
        {

            int rowCount = 0;
            int pageSize = 10;
            var listOfProducts = ProductService.List(page, pageSize, categoryId, supplierId, searchValue, out rowCount);
            Models.ProductPaginationQueryResult model = new Models.ProductPaginationQueryResult()
            {
                Page = page,
                PageSize = pageSize,
                CategoryID = categoryId,
                SupplierID = supplierId,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = listOfProducts,
            };
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Sửa thông tin sản phẩm";
            var model = ProductService.GetEx(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.title = "Thêm sản phẩm";
            Product model = new Product()
            {
                ProductID = 0
            };
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                ProductService.Delete(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = ProductService.Get(id);
                if (model == null)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }
        public ActionResult Save(Product data)
        {
            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError("ProductName", "Vui lòng nhập tên sản phẩm");
            }
            if (string.IsNullOrEmpty(data.Unit))
            {
                data.Unit = "";
            }
            if (string.IsNullOrEmpty(data.Price.ToString()))
            {
                data.Price = 0;
            }
            if (data.CategoryID == 0)
            {
                ModelState.AddModelError("CategoryID", "Vui lòng chọn loại hàng");
            }
            if (data.SupplierID == 0)
            {
                ModelState.AddModelError("ProductID", "Vui lòng chọn nhà cung cấp");
            }
            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Thêm mặt hàng";
                    return View("Add", data);
                }
                else
                {
                    ViewBag.Title = "Sửa thông tin mặt hàng";
                    var productEx = ProductService.GetEx(data.ProductID);
                    return View("Edit", productEx);
                }

            }

            if (data.ProductID == 0)
            { 
                int ID = ProductService.Add(data);
                return RedirectToAction("Edit", new { id = ID });
            }
            else
            {
                ProductService.Update(data);

                return RedirectToAction("Edit", new { id = data.ProductID });
            }
        }
        public ActionResult AddAttribute(int productId)
        {
            ViewBag.title = "Thêm thuộc tính sản phẩm";
            var model = new ProductAttribute()
            {
                AttributeID = 0,
                ProductID = productId
            };
            return View("EditAttribute", model);
        }
        public ActionResult EditAttribute(int id)
        {
            ViewBag.title = "Sửa thông tin thuộc tính";
            var model = ProductService.GetAttribute(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            if (string.IsNullOrWhiteSpace(data.AttributeName))
            {
                ModelState.AddModelError("AttributeName", "Vui lòng nhập tên thuộc tính");
            }
            if (string.IsNullOrWhiteSpace(data.AttributeValue))
            {
                ModelState.AddModelError("AttributeValue", "Vui lòng nhập giá trị thuộc tính");
            }
            if (string.IsNullOrEmpty(data.DisplayOrder.ToString()))
            {
                data.DisplayOrder = 0;
            }
            if (!ModelState.IsValid)
            {
                if (data.AttributeID == 0)
                    ViewBag.Title = "Thêm thuộc tính sản phẩm";
                else
                    ViewBag.Title = "Sửa thông tin thuộc tính";
                return View("EditAttribute", data);
            }
            
            if (data.AttributeID == 0)
            {
                ProductService.AddAttribute(data);
            }
            else
            {
                ProductService.UpdateAttribute(data);
            }
            return RedirectToAction("Edit",new { id = data.ProductID});
        }
        public ActionResult DeleteAttributes(int id, long[] attributeIds )
        {
            if (attributeIds == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            ProductService.DeleteAttributes(attributeIds);
            return RedirectToAction("Edit", new { id = id });
        }
        public ActionResult AddGallery(int productID)
        {
            ViewBag.title = "Thêm ảnh minh họa";
            var model = new ProductGallery()
            {
                GalleryID = 0,
                ProductID = productID
            };
            return View("EditGallery", model);
        }
        public ActionResult EditGallery(int id)
        {
            ViewBag.title = "Sửa thông tin ảnh minh họa";
            var model = ProductService.GetGallery(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model); 
        }
        public ActionResult SaveGallery(ProductGallery data)
        {
            if (string.IsNullOrWhiteSpace(data.Photo))
            {
                ModelState.AddModelError("Photo", "Vui lòng nhập link ảnh");
            }
            if (string.IsNullOrWhiteSpace(data.Description))
            {
                ModelState.AddModelError("Description", "Vui lòng nhập mô tả");
            }
            if (string.IsNullOrEmpty(data.DisplayOrder.ToString()))
            {
                data.DisplayOrder = 0;
            }
            
            if (!ModelState.IsValid)
            {
                if (data.GalleryID == 0)
                    ViewBag.Title = "Thêm ảnh mô tả";
                else
                    ViewBag.Title = "Sửa ảnh mô tả";
                return View("EditGallery", data);
            }

            if (data.GalleryID == 0)
            {
                ProductService.AddGallery(data);
            }
            else
            {
                ProductService.UpdateGallery(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });
        }
        public ActionResult DeleteGalleries(int id, long[] galleryIds)
        {
            if (galleryIds == null)
            {
                return RedirectToAction("Edit", new { id = id });
            }
            ProductService.DeleteGallery(galleryIds);
            return RedirectToAction("Edit", new { id = id });
        }
       
    }
}