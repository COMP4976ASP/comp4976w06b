﻿using Lab6b.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Lab6b.Controllers
{
    public class ProductsController : Controller
    {
        NorthwindContext ctx = new NorthwindContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        private SelectList dropDownListValue(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "-1", Text = "Show All" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        }

        public ActionResult ProductSearch(string categoryId, string supplierId)
        {

            ViewData["categoryId"] = dropDownListValue(new SelectList(ctx.Categories, "CategoryID", "CategoryName"));
            ViewData["supplierId"] = dropDownListValue(new SelectList(ctx.Suppliers, "SupplierID", "CompanyName"));

            var selectedCategory = Convert.ToInt32(categoryId);
            var selectedSupplier = Convert.ToInt32(supplierId);
            var categoryName = "";
            var supplierName = "";
            var result = "";

            var prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                .OrderBy(c => c.ProductID)
                .ToList();

            if (Request.IsAjaxRequest())
            {
                if (selectedCategory != -1 && selectedSupplier != -1)
                {
                    prod = ctx.Products.Include(p => p.Category)
                        .Include(p => p.Supplier)
                        .Where(p => p.CategoryID == selectedCategory && p.SupplierID == selectedSupplier)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                    categoryName = ctx.Categories.Where(c => c.CategoryID == selectedCategory).Select(c => c.CategoryName).First().ToString();
                    supplierName = ctx.Suppliers.Where(s => s.SupplierID == selectedSupplier).Select(s => s.CompanyName).First().ToString();
                    result = "Product with category = " + categoryName + ", and Supplier = " + supplierName + ".";
                }
                else if (selectedCategory != -1 && selectedSupplier == -1)
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                        .Where(p => p.CategoryID == selectedCategory)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                    categoryName = ctx.Categories.Where(c => c.CategoryID == selectedCategory).Select(c => c.CategoryName).First().ToString();
                    result = "All products with category = " + categoryName + ".";
                }
                else if (selectedCategory == -1 && selectedSupplier != -1)
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                        .Where(p => p.SupplierID == selectedSupplier)
                        .OrderBy(c => c.ProductID)
                        .ToList();
                    supplierName = ctx.Suppliers.Where(s => s.SupplierID == selectedSupplier).Select(s => s.CompanyName).First().ToString();
                    result = "All products with supplier = " + supplierName + ".";
                }
                else
                {
                    prod = ctx.Products.Include(p => p.Category).Include(p => p.Supplier)
                    .OrderBy(c => c.ProductID)
                    .ToList();
                    result = "All products.";
                }

                ViewBag.Message = result;
                return PartialView("_ProductSearchPartial", prod);
            }

            return View(prod);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
