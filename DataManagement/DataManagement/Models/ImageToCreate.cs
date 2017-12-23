using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataManagement.Models
{
    public class ImageToCreate
    {
        public string _id;
        public Attachment _attachments;
    }
    public class Attachment
    {
        public Image image;
        public Attachment()
        {
        }
    }
}
