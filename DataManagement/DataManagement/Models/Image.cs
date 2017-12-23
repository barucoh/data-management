using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class Image
    {
        public string _id { get; set; }
        public string _rev { get; set; }
        public string caption { get; set; }
        public string name { get; set; }
        public string imageB64 { get; set; }
    }
}
