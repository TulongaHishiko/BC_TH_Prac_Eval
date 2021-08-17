using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BC_TH_Prac_Eval.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int LinkedClientsCount { get; set; }

        public override string ToString()
        {
            return Surname + " " + Name;
        }
    }
}
