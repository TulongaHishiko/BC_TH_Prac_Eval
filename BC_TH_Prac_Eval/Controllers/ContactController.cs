using BC_TH_Prac_Eval.Core.Repositories;
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
       
        public ContactController(IConfiguration _configuration) : base(_configuration)
        {
            
        }

        public async Task<IActionResult> Index()
        {
            ContactsVM Model = new ContactsVM();
            try
            {
                var items = await _contactRepo.GetAll();
                Model.Contacts = items.OrderBy(s => s.Surname).ThenBy(s => s.Name).ToList();

                if (TempData.ContainsKey("Toast"))
                    toastMessage = TempData["Toast"].ToString();
                ViewData["Toast"] = toastMessage;
            }
            catch (Exception ex)
            {

            }
            return View(Model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ContactsVM Model = new ContactsVM();
            try
            {
                var item = await _contactRepo.GetSingle(id);
                Model.Contact = item;
                var linkedClients = await _contactClientRepo.GetLinkedClients(id);
                var clients = await _clientRepo.GetAll();
                List<ClientModel> clientList = new List<ClientModel>();
                foreach(ContactClientModel link in linkedClients)
                {
                    clientList.Add(clients.Where(x => x.Id == link.ClientId).Single());
                }
                Model.LinkedClients = clientList;

                if (TempData.ContainsKey("Toast"))
                    toastMessage = TempData["Toast"].ToString();
                ViewData["Toast"] = toastMessage;
            }
            catch (Exception ex)
            {

            }
            return View(Model);
        }
        [HttpGet]
        public async Task<JsonResult> GetContacts()
        {
            List<ContactModel> list = new List<ContactModel>();
            try
            {
                var items = await _contactRepo.GetAll();
                list = items.OrderBy(s=>s.Surname).ThenBy(s=>s.Name).ToList();
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }
        [HttpPost]
        public async Task<JsonResult> Add([FromForm] AddContactModel model)
        {
            bool valid = false;
            try
            {
                if(ModelState.IsValid)
                {
                    await _contactRepo.Add(new ContactModel
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Email = model.Email
                    });
                    valid = true;
                    //TempData["Toast"] = "Contact: " + model.Name+" "+model.Surname + " has been successfully been added.";
                }
                
            }
            catch (Exception ex)
            {
                valid = false;
            }
            return Json(valid);
        }
        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
           
            try
            {
                await _contactRepo.Delete(id);
                TempData["Toast"] = "The Contact has successfully been deleted.";
            }
            catch (Exception ex)
            {
              
            }
            return Json(new { redirectToUrl = "/Contact/Index"});
        }
    }
}
