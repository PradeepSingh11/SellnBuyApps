using sellnbuyapps.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sellnbuyapps.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["cart"] == null)
            {
                ViewBag.Check = "No item in your cart";
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Buy(int Id, Home home)
        {
            DataSet ds = new DataSet();
            if (Session["cart"] == null)
            {
                ds = home.GetByProductID_SingleProjectDetails();
                List<Home> cart = new List<Home>();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    cart.Add(new Home
                    {
                        Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]),
                        projectname = Convert.ToString(ds.Tables[0].Rows[0]["ProjectName"]),
                        screenshot = Convert.ToString(ds.Tables[0].Rows[0]["Screenshot"]),
                        features = Convert.ToString(ds.Tables[0].Rows[0]["Features"]),
                        technologyname = Convert.ToString(ds.Tables[0].Rows[0]["TechnologyName"]),
                        cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"]),
                    });
                }
                Session["cart"] = cart;
            }
            else
            {
                ds = home.GetByProductID_SingleProjectDetails();
                List<Home> cart = (List<Home>)Session["cart"];
                int index = isExist(Id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        cart.Add(new Home
                        {
                            Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]),
                            projectname = Convert.ToString(ds.Tables[0].Rows[0]["ProjectName"]),
                            screenshot = Convert.ToString(ds.Tables[0].Rows[0]["Screenshot"]),
                            features = Convert.ToString(ds.Tables[0].Rows[0]["Features"]),
                            technologyname = Convert.ToString(ds.Tables[0].Rows[0]["TechnologyName"]),
                            cost = Convert.ToInt32(ds.Tables[0].Rows[0]["Cost"]),
                        });
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int Id)
        {
            List<Home> cart = (List<Home>)Session["cart"];
            int index = isExist(Id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(int Id)
        {
            List<Home> cart = (List<Home>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Id.Equals(Id))
                    return i;
            return -1;
        }

        public ActionResult CheckOut()
        {
            return View();
        }
    }
}