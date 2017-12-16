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
        public byte[] ImageData { get; set; }
        /*
        public class ImageSet
        {
            public string Name { get; set; }

            public List<Image> Images { get; set; }
        }

        public class Image
        {
            public string FileName { get; set; }

            public string MimeType { get; set; }

            public byte[] ImageData { get; set; }
        }*/
    }
}
