﻿using BC_TH_Prac_Eval.Core.Repositories;
using BC_TH_Prac_Eval.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Controllers
{
    public class ContactController : BaseController
    {
        private ContactRepository _contactRepo;
        public ContactController(IConfiguration _configuration, ContactRepository contactRepo) : base(_configuration)
        {
            _contactRepo = contactRepo;
        }

        public async Task<IActionResult> Index()
        {
            ContactsVM Model = new ContactsVM();
            try
            {
                var items = await _contactRepo.GetAll();
                Model.Contacts = items.ToList();
            }
            catch (Exception ex)
            {

            }
            return View(Model);
        }
    }
}
