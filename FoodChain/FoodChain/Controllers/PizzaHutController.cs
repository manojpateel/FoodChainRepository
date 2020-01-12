using FoodChain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace FoodChain.Controllers
{
    public class PizzaHutController : Controller
    {
        //
        // GET: /PizzaHut/

        public ActionResult Index()
        {
            Pizzas oPizzas=new Pizzas();

            List<Pizzas> oPizzasList = oPizzas.GetProduct();
            oPizzas = null;
            
            //int Id = 1;
            //Pizzas oPizzas = new Pizzas();
            //oPizzas.ProductId = Id;
            //oPizzas.ProductName = "Paneer Soya Supreme";
            //oPizzas.Quantity = 2;
            //oPizzas.Price = 569;

            //oPizzasList.Add(oPizzas);

            //oPizzas = new Pizzas();
            //oPizzas.ProductId = ++Id;
            //oPizzas.ProductName = "Veg Exotica";
            //oPizzas.Quantity = 2;
            //oPizzas.Price = 569;

            //oPizzasList.Add(oPizzas);

            //oPizzas = new Pizzas();
            //oPizzas.ProductId = ++Id;
            //oPizzas.ProductName = "Veggie Italiano";
            //oPizzas.Quantity = 2;
            //oPizzas.Price = 569;

            //oPizzasList.Add(oPizzas);

            //oPizzas = new Pizzas();
            //oPizzas.ProductId = ++Id;
            //oPizzas.ProductName = "Veggie Supreme";
            //oPizzas.Quantity = 2;
            //oPizzas.Price = 569;

            //oPizzasList.Add(oPizzas);
            return View(oPizzasList);
        }
        public string GetProduct()
        {
            StringBuilder oBuilder = new StringBuilder();

            oBuilder.AppendLine("<table>");

            Pizzas oPizzas=new Pizzas();
            List<Pizzas> oPizzasList = oPizzas.GetProduct();
            int count=0;
            foreach (var item in oPizzasList)
            {      
                count++;
                if(count==5)
                    count=1;
                if(count==1)
                    oBuilder.AppendLine("<tr>");
                oBuilder.AppendLine("<td>");
                oBuilder.AppendLine("<input type=\"hidden\" value=\""+item.ProductId+"\" Id =\"hdn" + item.ProductId + "\"");        
                oBuilder.AppendLine("<div style=\"height: 200px; width: 100px;\">");
                oBuilder.AppendLine("<p><span>"+item.ProductName+"</span></p>");
                oBuilder.AppendLine("</div>");
                oBuilder.AppendLine("<div style=\"height: 30px; width: 100px; bottom: 0px;\">");
                oBuilder.AppendLine("<button id=\"add_"+item.ProductId+"\" style=\"height:30px;width:100px;background-color:lightgreen;border-radius: 6px;\">");
                oBuilder.AppendLine("<span style=\"float: left;\">Add</span>");
                oBuilder.AppendLine("<span style=\"float: right;\">"+item.Price+" </span>");
                oBuilder.AppendLine("</button>");
                oBuilder.AppendLine("</div>");
                oBuilder.AppendLine("</td>");
                if(count==4)
                    oBuilder.AppendLine("</tr>");      
            }

            oBuilder.AppendLine("</table>");

            return oBuilder.ToString();
        }
        public ActionResult Basket(string ProductId)
        {
            string ProductFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\debug", "").Replace("bin\\release", "") + "App_Data\\PizzaHut\\Pizzas.xml";
            string FileData = General.FileData(ProductFilePath);
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(FileData);

            var oProducts = doc.GetElementsByTagName("Product").OfType<XmlElement>().ToList();
            string price = "";
            string ProductName = "";

            for (var i = 0; i < oProducts.Count; i++)
            {
                var ProdcutIdd = oProducts[i].GetElementsByTagName("ProductId")[0].InnerText;

                if (ProductId == ProdcutIdd)
                {
                    price = oProducts[i].GetElementsByTagName("Price")[0].InnerText;
                    ProductName = oProducts[i].GetElementsByTagName("ProductName")[0].InnerText;
                    break;
                }
            }

            return PartialView();
        }
        [HttpPost]
        public JsonResult AddToBasket(string SelProductArray,string ProductId)         
        {
            dynamic oSelectProduct = JsonConvert.DeserializeObject(SelProductArray);
            
            string ProductFilePath = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace("bin\\debug", "").Replace("bin\\release","") + "App_Data\\PizzaHut\\Pizzas.xml";
            string FileData = General.FileData(ProductFilePath);
            XmlDocument doc=new XmlDocument();

            doc.LoadXml(FileData);

            var oProducts = doc.GetElementsByTagName("Product").OfType<XmlElement>().ToList();
            string price = "";
            string ProductName = "";
            string productId = "";
            decimal TotalPrice = 0;
            for (var i = 0; i < oProducts.Count; i++)
            {
                var ProdcutIdd = oProducts[i].GetElementsByTagName("ProductId")[0].InnerText;
                
                    if (ProductId == ProdcutIdd)
                    {
                        price = oProducts[i].GetElementsByTagName("Price")[0].InnerText;
                        ProductName = oProducts[i].GetElementsByTagName("ProductName")[0].InnerText;
                        TotalPrice =Convert.ToDecimal(price);
                        break;
                    }
                
                
            }

            for (var i = 0; i < oProducts.Count; i++)
            {
                var ProdcutIdd = oProducts[i].GetElementsByTagName("ProductId")[0].InnerText;
                for (var j = 0; j < oSelectProduct.Count;j++)
                {
                    if (oSelectProduct[j] == "") continue;
                    if (oSelectProduct[j]== ProdcutIdd)
                    {
                        decimal amount = Convert.ToDecimal(oProducts[i].GetElementsByTagName("Price")[0].InnerText);

                        TotalPrice += amount;
                    }
                }


            }


            return Json(new { Price = price, ProductName = ProductName, ProductId = productId, TotalAmount = TotalPrice });
            
        }

    }
}
