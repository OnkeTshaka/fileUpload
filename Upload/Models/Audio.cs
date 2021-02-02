using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Upload.Models
{
    public class Audio
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AudioID { get; set; }
        public string name { get; set; }
        public byte[] Data { get; set; }
    }
}