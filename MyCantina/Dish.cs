using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyCantina
{
    class Dish
    {
        private Image _thumbnail = Image.FromFile("img.jpg");
        public Image Thumbnail
        {
            get { return _thumbnail; }
            set { _thumbnail = value; }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private double _cost;

        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
    }
}
