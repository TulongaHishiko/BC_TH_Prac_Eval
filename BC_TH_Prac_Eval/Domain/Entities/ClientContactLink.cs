using BC_TH_Prac_Eval.Domain.Abstract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Domain.Entities
{
    public class ClientContactLink:Entity
    {
        public int ClientId { get; set; }
        public int ContactId { get; set; }
    }
}
