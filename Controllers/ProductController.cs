 
using Microsoft.AspNetCore.Mvc;
using ShopBridgeApp.AppCommon;
using ShopBridgeApp.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Net.Http; 
namespace ShopBridgeApp.Controllers
{/// <summary>
/// class is used to perform all  database related activities to product 
/// </summary>
    public class ProductController : Controller
    {
        #region Variable Declaration
        APIHandler handler; 
        private string _strHostName = string.Empty;
        GlobalData globalData = new GlobalData();
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductController()
        {
            handler = new APIHandler();
        }
        #endregion 
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
           
            HttpResponseMessage response = handler.GetResponse("Products");
            response.EnsureSuccessStatusCode();
            List<ViewModel.Product.ProductViewModel> products = response.Content.ReadAsAsync<List<ViewModel.Product.ProductViewModel>>().Result; 
            var listingresult = products.Select(result => new ProductViewModel
            {
                Prod_Id = result.Prod_Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                Qty = result.Qty,
                Mf_Date = result.Mf_Date,
                Ex_date = result.Ex_date

            });

            var model = new ProductsListModel()
            {
                ProductView = listingresult
            };
            return View(model);
        }
        /// <summary>
/// 
/// </summary>
/// <returns></returns>
        public IActionResult Create()
        {
            try
            { 
                return View();
            }
            catch (Exception ex)
            {
 
                return View();
            }
        }


        /// <summary>
        /// Method is used to add new product.
        /// </summary>
        /// <param name="productviewmodel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productviewmodel)
        {
            try
            {
                if (TempData["globalData"] != null)
                {
                    globalData = TempData.Get<GlobalData>("GlobalData");
                }

                HttpResponseMessage response = handler.PostResponse("Products", productviewmodel);
                response.EnsureSuccessStatusCode(); 
                globalData.messageDetail = "Product added Sucessfully";
                TempData.Put("globalData", globalData);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {

                globalData.messageDetail = "Something Went Wrong";
                TempData.Put("globalData", globalData);
                return RedirectToAction(nameof(Index));

            } 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public IActionResult Edit(long ProductId)
        {
            try
            {
                HttpResponseMessage response = handler.GetResponse("Products/" + ProductId);
                response.EnsureSuccessStatusCode();
                ProductEditViewModel products = response.Content.ReadAsAsync<ProductEditViewModel>().Result;
                ProductEditViewModel productviewmodel = new ProductEditViewModel()
                {
                    Prod_Id = products.Prod_Id,
                    Name = products.Name,
                    Description = products.Description,
                    Price = products.Price,
                    Qty = products.Qty,
                    Mf_Date = products.Mf_Date,
                    Ex_date = products.Ex_date

                };
                return View(productviewmodel);
            }
            catch (Exception ex)
            {

                return View();
            }
            
           
        }
        /// <summary>
        /// method is used to update the product 
        /// </summary>
        /// <param name="producteditviewmodel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel producteditviewmodel)
        { 
            try
            {

                if (TempData["globalData"] != null)
                {
                    globalData = TempData.Get<GlobalData>("GlobalData");
                } 
                ProductEditViewModel prod = new ProductEditViewModel();
                prod.Prod_Id = producteditviewmodel.Prod_Id;
                prod.Name = producteditviewmodel.Name;
                prod.Description = producteditviewmodel.Description;
                prod.Price = producteditviewmodel.Price;
                prod.Qty = producteditviewmodel.Qty;
                prod.Mf_Date = producteditviewmodel.Mf_Date;
                prod.Ex_date = producteditviewmodel.Ex_date;
                prod.Inserted_Date = producteditviewmodel.Inserted_Date; 
                prod.Is_Deleted = false;
                prod.Is_Edited = true;  
                HttpResponseMessage response = handler.PutResponse("Products/" + prod.Prod_Id+"", prod);
                response.EnsureSuccessStatusCode();
                globalData.messageDetail = "Product updated Sucessfully"; 
                TempData.Put("globalData", globalData);
                return RedirectToAction(nameof(Index)); 
            }


            catch (Exception ex)
            {
                globalData.messageDetail = "Something Went Wrong";
                TempData.Put("globalData", globalData);
                return RedirectToAction(nameof(Index));
            }
        } 

        /// <summary>
        /// method is used to delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(string id)
        { 
            try
            { 
                HttpResponseMessage response = handler.DeleteResponse("Products/" + id + "");
                response.EnsureSuccessStatusCode();
                globalData.messageDetail = "Product deleted successfully";
                TempData.Put("globalData", globalData);
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {
                globalData.messageDetail = "Something Went Wrong";
                TempData.Put("globalData", globalData); 
                return RedirectToAction(nameof(Index));
            }

        }
        /// <summary>
        /// Method is used to check Item exist or not.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>

        [AcceptVerbs("Get", "Post")]  
        public IActionResult CheckIfProdExist(long Prod_Id,string Name)
        {

            HttpResponseMessage response = handler.GetResponse("Products/CheckIfProdExist/" + Prod_Id + "/" + Name + "");
            bool result = response.Content.ReadAsAsync<bool>().Result;
            if (result)
            {

                return Json($"Product {Name} is already exist");
            }
            else
            {
                return Json(data: true);
            }


        }
        #endregion
    }
}
