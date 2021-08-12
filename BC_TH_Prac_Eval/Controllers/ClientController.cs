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
    public class ClientController:BaseController
    {
        private ClientRepository _clientRepo;
        public ClientController(IConfiguration _configuration, ClientRepository clientRepo):base(_configuration)
        {
            _clientRepo = clientRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ClientsVM Model = new ClientsVM();
            try
            {
                var items = await _clientRepo.GetAll();
                Model.Clients = items.ToList();
            }
            catch(Exception ex)
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
                var items = await _clientRepo.GetAll();
                Model.Clients = items.ToList();
            }
            catch (Exception ex)
            {

            }
            return View(Model);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]AddClientModel model)
        {
            try
            {
                //generate client code
            }
            catch(Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
        [HttpPut]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            try
            {
                await _clientRepo.Delete(id);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }
    }
}
