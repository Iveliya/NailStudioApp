﻿using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace NailStudioApp.Webb.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.ApplicationConstants;
    [Area(AdminRoleName)]
    [Authorize(Roles =AdminRoleName)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
