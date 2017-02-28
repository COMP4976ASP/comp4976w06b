using AjaxLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace AjaxLab.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();


        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Message = "Products";

            return View();
        }

        private SelectList AddFirstItem(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "-1", Text = "----ALL----" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        }

        public ActionResult ProductSearch(string categoryId, string supplierId)
        {

            ViewData["categoryId"] = AddFirstItem(new SelectList(ctx.Categories, "CategoryID", "CategoryID"));
            ViewData["supplierId"] = AddFirstItem(new SelectList(ctx.Suppliers, "SupplierID", "SupplierID"));

            var cid = Convert.ToInt32(categoryId);
            var sid = Convert.ToInt32(supplierId);

            // if (String.IsNullOrEmpty(category) && String.IsNullOrEmpty(supplier)) {
            var prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                .OrderBy(c => c.ProductID)
                .ToList();
            //   }



            if (Request.IsAjaxRequest())
            {
                if (cid != -1 && sid != -1)
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                        .Where(p => p.CategoryID == cid && p.SupplierID == sid)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                }
                else if (cid == -1 && sid != -1)
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                        .Where(p => p.SupplierID == sid)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                }
                else if (cid != -1 && sid == -1)
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                        .Where(p => p.CategoryID == cid)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                }
                else
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                    .OrderBy(c => c.ProductID)
                    .ToList();
                }

                return PartialView("_ProductSearchPartial", prod);
            }


            return View(prod);

        }
    }
}