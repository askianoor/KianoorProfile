using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class Navbar
    {
        [Key]
        public Guid MenuId { get; set; }

        public int MenuOrder { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
    }
}
