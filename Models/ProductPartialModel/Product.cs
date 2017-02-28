using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxLab.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product { }

    public class ProductMetaData
    {
        [Display(Name = "Product Id")]
        public object ProductID { get; set; }
       
        [Display(Name = "Product Name")]
        public object ProductName { get; set; }
        
        [Display(Name = "Supplier")]
        public object SupplierID { get; set; }

        [Display(Name = "Category")]
        public object CategoryID { get; set; }

        [DataType(DataType.Currency)]
        public object UnitPrice { get; set; }
    }
}