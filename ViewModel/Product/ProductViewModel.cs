using Microsoft.AspNetCore.Mvc;
using System; 
using System.ComponentModel.DataAnnotations;
 

namespace ShopBridgeApp.ViewModel.Product
{
    public class ProductViewModel
    {

        public long Prod_Id { get; set; }

        [Remote(action: "CheckIfProdExist", controller: "Product", AdditionalFields = "Prod_Id")]
        [Required(ErrorMessage = "Please enter Product name.")]       
        public string Name { get; set; } 
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter Product price.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter Product quantity.")]
        public long Qty { get; set; }
        [Required(ErrorMessage = "Please enter date of manufacturing.")]
        public DateTime  Mf_Date
        { get; set; }

        [Required(ErrorMessage = "Please enter expire date.")]
        public DateTime Ex_date
        { get; set; }


        public bool? Is_Edited { get; set; }

        public bool? Is_Deleted { get; set; }


        public int? Inserted_User_ID
        { get; set; }

        public DateTime? Inserted_Date
        { get; set; }



        public int? Updated_User_ID
        { get; set; }

        public DateTime? Updated_Date
        { get; set; }


    }
}
