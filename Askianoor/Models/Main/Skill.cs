using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models.Main
{
    public class Skill
    {
        [Key]
        public Guid SkillId { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public string cssClass { get; set; }
        public int Group { get; set; }
    }
}
