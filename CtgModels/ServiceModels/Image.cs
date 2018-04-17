using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtgModels.DataModels.Images;
using CtgModels.Enums.Images;

namespace CtgModels.ServiceModels
{
    public class Image : BaseModel
    {
        public string Url { get; set; }
        public string Format { get; set; }
        public ImageType ImageType { get; set; }
    }
}
