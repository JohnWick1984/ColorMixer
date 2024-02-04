using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace ColorMixer
{
    public class ColorRGBA : INotifyPropertyChanged
    {
        private byte _red;
        private byte _green;
        private byte _blue;
        private byte _alpha;

        public byte Red
        {
            get => _red;
            set
            {
                _red = value;
                OnPropertyChanged(nameof(Red));
                OnPropertyChanged(nameof(Color));
            }
        }

        public byte Green
        {
            get => _green;
            set
            {
                _green = value;
                OnPropertyChanged(nameof(Green));
                OnPropertyChanged(nameof(Color));
            }
        }

        public byte Blue
        {
            get => _blue;
            set
            {
                _blue = value;
                OnPropertyChanged(nameof(Blue));
                OnPropertyChanged(nameof(Color));
            }
        }

        public byte Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                OnPropertyChanged(nameof(Alpha));
                OnPropertyChanged(nameof(Color));
            }
        }

        public Color Color => Color.FromArgb(Alpha, Red, Green, Blue);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

