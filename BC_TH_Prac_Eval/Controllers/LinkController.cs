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
    public class LinkController : BaseController
    {
        
        public LinkController(IConfiguration _configuration) : base(_configuration)
        {
            
        }
        [HttpGet]
        public async Task<JsonResult> GetLinkedContacts(int clientId)
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var list = await _clientContactRepo.GetLinkedContacts(clientId);
                var contactList = await _contactRepo.GetAll();

                List<ContactModel> linkedContacts = new List<ContactModel>();
                if(list.Any())
                {
                    foreach (ClientContactModel link in list)
                    {
                        linkedContacts.Add(contactList.Where(s => s.Id == link.ContactId).Single());
                    }
                }
                
                return Json(linkedContacts.OrderBy(x => x.Surname).ThenBy(x => x.Name));

            }
            catch (Exception ex)
            {

            }
            return Json(Model);
        }
        [HttpGet]
        public async Task<JsonResult> GetUnlinkedContacts(int clientId)
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var list = await _clientContactRepo.GetLinkedContacts(clientId);
                var contactList = await _contactRepo.GetAll();

                List<ContactModel> unLinkedContacts = contactList.ToList();
                if (list.Any())
                {
                    foreach (ClientContactModel link in list)
                    {

                        var toBeRemoved = unLinkedContacts.Single(s => s.Id == link.ContactId);
                        unLinkedContacts.Remove(toBeRemoved);

                    }
                }
                   
                return Json(unLinkedContacts.OrderBy(x => x.Surname).ThenBy(x => x.Name));

            }
            catch (Exception ex)
            {

            }
            return Json(Model);
        }
        [HttpPost]
        public async Task<JsonResult> LinkContacts([FromForm]LinkContactModel model)
        {
            try
            {
                var ids = model.SelectedContacts.Split(',');
                foreach(string id in ids)
                {
                    int contactId = int.Parse(id);
                    await _clientContactRepo.LinkContact(model.ClientId, contactId);
                }
                TempData["Toast"] = "The contact(s) has been successfully linked";
                return Json(new { redirectToUrl = "/Client/Details?id=" + model.ClientId });
            }
            catch (Exception ex)
            {

            }
            return Json(false);
        }
        [HttpGet]
        public async Task<JsonResult> UnlinkContact(int clientId,int contactId)
        {
            try
            {
                await _clientContactRepo.UnLinkContact(clientId,contactId);
                TempData["Toast"] = "The contact(s) has been successfully unlinked";
                return Json(new { redirectToUrl = "/Client/Details?id=" + clientId });
            }
            catch (Exception ex)
            {

            }
            return Json(false);
        }


        [HttpGet]
        public async Task<JsonResult> GetLinkedClients(int contactId)
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var list = await _contactClientRepo.GetLinkedClients(contactId);
                var clientList = await _clientRepo.GetAll();

                List<ClientModel> linkedClients = new List<ClientModel>();
                foreach (ContactClientModel link in list)
                {
                    linkedClients.Add(clientList.Where(s => s.Id == link.ClientId).Single());
                }
                return Json(linkedClients.OrderBy(s => s.Name));

            }
            catch (Exception ex)
            {

            }
            return Json(Model);
        }
        [HttpGet]
        public async Task<JsonResult> GetUnlinkedClients(int contactId)
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var list = await _contactClientRepo.GetLinkedClients(contactId);
                var clientList = await _clientRepo.GetAll();

                List<ClientModel> unlinkedClients = clientList.ToList();
                if (list.Any())
                {
                    foreach (ContactClientModel link in list)
                    {

                        var toBeRemoved = unlinkedClients.Single(s => s.Id == link.ClientId);
                        unlinkedClients.Remove(toBeRemoved);

                    }
                }
                return Json(unlinkedClients.OrderBy(s => s.Name));

            }
            catch (Exception ex)
            {

            }
            return Json(Model);
        }
        [HttpPost]
        public async Task<JsonResult> LinkClients([FromForm]LinkClientModel model)
        {
            try
            {
                var ids = model.SelectedClients.Split(',');
                foreach (string id in ids)
                {                
                    await _contactClientRepo.LinkClient(model.ContactId,int.Parse(id));
                }
                TempData["Toast"] = "The client(s) has been successfully linked";
                return Json(new { redirectToUrl = "/Contact/Details?id=" + model.ContactId });
            }
            catch (Exception ex)
            {

            }
            return Json(false);
        }
        [HttpGet]
        public async Task<JsonResult> UnlinkClient(int contactId, int clientId)
        {
            try
            {
                await _contactClientRepo.UnLinkClient(contactId,clientId);
                TempData["Toast"] = "The client(s) has been successfully unlinked";
                return Json(new { redirectToUrl = "/Contact/Details?id=" + contactId });
            }
            catch (Exception ex)
            {

            }
            return Json(false);
        }

    }
}
