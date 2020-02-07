using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class DashboardSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string WebsiteTitle { get; set; }

        public string WebsiteHeader { get; set; }

        public string OwnerName { get; set; }

        public string OwnerPictureSrc { get; set; }
    }
}
