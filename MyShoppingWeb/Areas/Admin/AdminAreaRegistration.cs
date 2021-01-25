﻿using System.Web.Mvc;

namespace MyShoppingWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                   namespaces: new[] { "MyShoppingWeb.Areas.Admin.Controllers" } //add this line
            );
        }
    }
}