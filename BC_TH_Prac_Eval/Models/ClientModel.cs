using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClientCode { get; set; }
        public int LinkedContactsCount { get; set; }
    }
}
