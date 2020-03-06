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

        public string AboutMeDescription { get; set; }

        public string AboutMeImage { get; set; }

        public string HomePageImage { get; set; }

        public string HomePageText { get; set; }

        public string FooterText { get; set; }

    }
}
