using BC_TH_Prac_Eval.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Controllers
{
    public abstract class BaseController:Controller
    {
        protected string ConnectionString;
        private IConfiguration Configuration;
        protected ClientRepository _clientRepo;
        protected ContactRepository _contactRepo;
        protected ContactClientRepository _contactClientRepo;
        protected ClientContactRepository _clientContactRepo;
        protected string toastMessage = "";
        public BaseController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            ConnectionString = Configuration.GetConnectionString("LocalDb");
            _clientRepo = new ClientRepository(ConnectionString);
            _contactRepo = new ContactRepository(ConnectionString);
            _contactClientRepo = new ContactClientRepository(ConnectionString);
            _clientContactRepo = new ClientContactRepository(ConnectionString);

        }
    }
}
