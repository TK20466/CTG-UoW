using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.Enums.Images;

namespace CtgModels.DataModels.Images
{
    public class Image : BaseEntity
    {
        public string Format { get; set; }
        public string Url { get; set; }
        public ImageType ImageType { get; set; }
    }
}
