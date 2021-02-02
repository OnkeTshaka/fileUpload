using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Upload.Models
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        public byte[] Data { get; set; }
    }
}