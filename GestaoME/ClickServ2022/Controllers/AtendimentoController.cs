﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickServ2022.Controllers
{
    public class AtendimentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
