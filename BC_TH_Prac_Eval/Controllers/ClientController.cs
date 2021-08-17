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
    public class ClientController : BaseController
    {
        
        public ClientController(IConfiguration _configuration) : base(_configuration)
        {
            
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var items = await _clientRepo.GetAll();
                Model.Clients = items.OrderBy(s => s.Name).ToList();

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
        public async Task<IActionResult> Details([FromQuery]int id)
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var item = await _clientRepo.GetSingle(id);
                Model.Client = item;
                var linkedContacts = await _clientContactRepo.GetLinkedContacts(id);
                var contacts = await _contactRepo.GetAll();
                List<ContactModel> contactList = new List<ContactModel>();
                foreach (ClientContactModel link in linkedContacts)
                {
                    contactList.Add(contacts.Where(x => x.Id == link.ContactId).Single());
                }
                Model.LinkedContacts = contactList;

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
        public async Task<JsonResult> GetClients()
        {
            List<ClientModel> list = new List<ClientModel>();
            try
            {
                var items = await _clientRepo.GetAll();
                list = items.OrderBy(s=>s.Name).ToList();
            }
            catch (Exception ex)
            {

            }
            return Json(list);
        }

        [HttpPost]
        public async Task<JsonResult> Add([FromForm]AddClientModel model)
        {
            bool valid = false;
            try
            {
                if(ModelState.IsValid)
                {
                    ClientModel newClient = new ClientModel
                    {
                        Name = model.Name,
                        ClientCode = "Set Up"
                    };
                    newClient.ClientCode = await GenerateClientCode(newClient.Name);
                    await _clientRepo.Add(newClient);
                    valid = true;
                    //TempData["Toast"] = "Client: " + model.Name + " has been successfully been added.";
                }
                
            }
            catch (Exception ex)
            {
                valid = false;
            }
            return Json(valid);
        }

        private async Task<string> GenerateClientCode(string name)
        {
            string clientCode = "";
            try
            {
                List<ClientModel> clientList = new List<ClientModel>();
                var list = await _clientRepo.GetAll();
                clientList = list.ToList();

                //split name into word array and check word count
                var wordArray = name.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string preLimAlpha = "";
                
                if (wordArray.Length >= 3)
                {
                    int count = 1;
                    foreach (string word in wordArray)
                    {
                        if (count <= 3)
                        {
                            preLimAlpha += word.Substring(0, 1);
                            count++;
                        }
                        else
                            break;
                    }
                }
                else if (wordArray.Length == 2)
                {
                    int count = 1;
                    foreach (string word in wordArray)
                    {
                        if (count <= 2)
                        {
                            preLimAlpha += word.Substring(0, 1);
                            count++;
                        }
                        else
                            break;                     
                    }
                    preLimAlpha += "A";
                }
                else
                {
                    var charArray = name.ToUpper().ToCharArray();
                    if (charArray.Length >= 3)
                    {
                        preLimAlpha = charArray[0].ToString() + charArray[1].ToString()+ charArray[2].ToString();
                    }
                    else if (charArray.Length == 2)
                    {
                        preLimAlpha = charArray[0].ToString() + charArray[1].ToString();
                        preLimAlpha += "A";
                    }
                    else
                    {
                        preLimAlpha = charArray[0].ToString();
                        preLimAlpha += "AA";                        
                    }
                }
                //add numeric to aplha
                string preLimString = "";
                int preLimNum = 1;
                preLimString = preLimAlpha + preLimNum.ToString("D3");
                //check if exists and increment until it doest
                if (ClientCodeExists(clientList, preLimString))
                {
                    do
                    {
                        preLimNum++;
                        preLimString = preLimAlpha + preLimNum.ToString("D3");
                    } while (ClientCodeExists(clientList, preLimString));
                    return preLimString;
                }
                else
                {
                    return preLimString;
                }


            }
            catch (Exception ex)
            {

            }

            return clientCode;
        }
        public bool ClientCodeExists(List<ClientModel> clients,string clientCode)
        {
            return clients.Where(x => x.ClientCode == clientCode).Any();
        }

        [HttpGet]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                await _clientRepo.Delete(id);
                TempData["Toast"] = "The client has successfully been deleted.";
            }
            catch (Exception ex)
            {
               
            }
            return Json(new { redirectToUrl = "/Client/Index" });
        }
    }
}
