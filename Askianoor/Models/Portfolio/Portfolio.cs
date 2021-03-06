﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class Portfolio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string SubTitle { get; set; }

        public string BackTitle { get; set; }

        public string Body { get; set; }

        public string BackBody { get; set; }

        public string PictureSrc { get; set; }
        public string CirclePictureSrc { get; set; }

        public string Technologies { get; set; }

        public string projectLink { get; set; }

        public Guid PortfolioCategoryId {get; set;}
    }
}
