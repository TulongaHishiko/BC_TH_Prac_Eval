using BC_TH_Prac_Eval.Domain.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Domain.Entities
{
    public class Contact:Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
