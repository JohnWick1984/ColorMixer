using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace ColorMixer
{

    public class ColorModel
    {
        private byte _alpha;
        private byte _red;
        private byte _green;
        private byte _blue;

        public byte Alpha
        {
            get => _alpha;
            set => _alpha = value;
        }

        public byte Red
        {
            get => _red;
            set => _red = value;
        }

        public byte Green
        {
            get => _green;
            set => _green = value;
        }

        public byte Blue
        {
            get => _blue;
            set => _blue = value;
        }

        public Color Color => Color.FromArgb(Alpha, Red, Green, Blue);

        public string ColorToHex => Color.ToString();

      
    }
}