using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Upload.Models
{
    public class Video
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Vid { get; set; }
        public string Vname { get; set; }
        public byte[] Data { get; set; }
    }
}