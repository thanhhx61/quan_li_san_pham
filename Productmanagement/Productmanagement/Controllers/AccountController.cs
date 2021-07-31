﻿using Productmanagement.Entities;
using Productmanagement.Models.Account;
using Productmanagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(login);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }

        public IActionResult Signout()
        {
            userService.Signout();
            return RedirectToAction("Login", "Account");
        }
    }
}
