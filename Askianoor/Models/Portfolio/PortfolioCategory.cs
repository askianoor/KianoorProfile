using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class PortfolioCategory
    {
        [Key]
        public Guid PortfolioCategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CategoryType { get; set; }
    }
}
