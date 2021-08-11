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

        public BaseController(IConfiguration _configuration)
        {
            Configuration = _configuration;
            ConnectionString = Configuration.GetConnectionString("LocalDb");
        }
    }
}
