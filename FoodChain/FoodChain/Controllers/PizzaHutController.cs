using FoodChain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodChain.Controllers
{
    public class PizzaHutController : Controller
    {
        //
        // GET: /PizzaHut/

        public ActionResult Index()
        {
            List<Pizzas> oPizzasList = new List<Pizzas>();

            int Id = 1;
            Pizzas oPizzas = new Pizzas();
            oPizzas.ProductId = Id;
            oPizzas.ProductName = "Paneer Soya Supreme";
            oPizzas.Quantity = 2;
            oPizzas.Price = 569;

            oPizzasList.Add(oPizzas);

            oPizzas = new Pizzas();
            oPizzas.ProductId = ++Id;
            oPizzas.ProductName = "Veg Exotica";
            oPizzas.Quantity = 2;
            oPizzas.Price = 569;

            oPizzasList.Add(oPizzas);

            oPizzas = new Pizzas();
            oPizzas.ProductId = ++Id;
            oPizzas.ProductName = "Veggie Italiano";
            oPizzas.Quantity = 2;
            oPizzas.Price = 569;

            oPizzasList.Add(oPizzas);

            oPizzas = new Pizzas();
            oPizzas.ProductId = ++Id;
            oPizzas.ProductName = "Veggie Supreme";
            oPizzas.Quantity = 2;
            oPizzas.Price = 569;

            oPizzasList.Add(oPizzas);
            return View(oPizzasList);
        }

    }
}
