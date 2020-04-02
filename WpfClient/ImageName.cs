using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    [Serializable]
    public class ImageName
    {
        public Image Image { get; set; }

        public string Name { get; set; }
    }
}
