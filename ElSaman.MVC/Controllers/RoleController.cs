﻿using ElSaman.Repository;
using ELSaman.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ElSaman.MVC.Controllers
{
    public class RoleController:Controller
    {
        RoleRepository RoleRepository;
        public RoleController(RoleRepository _RoleRepository)
        {
            RoleRepository = _RoleRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.success = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await RoleRepository.Add(model);
                if (result.Succeeded)
                {
                    ViewBag.success = true;
                }
            }
            return View();
        }
    }
}
