using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics;


namespace ColorMixer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private ColorModel _color;
        private ObservableCollection<ColorModel> _colorList;
        private Color _currentColor;

        public MainViewModel()
        {
            Color = new ColorModel() { Red = 0, Green = 0, Blue = 0, Alpha = 0 };

            SliderValueChangedCommand = new RelayCommand(ExecuteSliderValueChanged);
            AddColorCommand = new RelayCommand(ExecuteAddColor);
            DeleteColorCommand = new RelayCommand(ExecuteDeleteColor);

            ColorList = new ObservableCollection<ColorModel>();
            CurrentColor = Color.Color;
        }
        

        public Color CurrentColor
        {

            get => _currentColor;
            set
            {
                _currentColor = value;
                OnPropertyChanged(nameof(CurrentColor));
                OnPropertyChanged(nameof(ColorBackground));
            }
        }
        public Brush ColorBackground => new SolidColorBrush(_currentColor);
        public ColorModel Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(ColorBackground));
                CurrentColor = _color.Color;
            }
        }
      
        public ObservableCollection<ColorModel> ColorList
        {
            get => _colorList;
            set
            {
                _colorList = value;
                OnPropertyChanged(nameof(ColorList));

            }
        }

        private byte _alphaValue;

        public byte AlphaValue
        {
            get => _alphaValue;
            set
            {
                _alphaValue = value;
                OnPropertyChanged(nameof(AlphaValue));
                Color.Alpha = (byte)value;
            }
        }

     


        public string ColorToHex => Color.ColorToHex;

        public ICommand SliderValueChangedCommand { get; }
        public ICommand AddColorCommand { get; }
        public ICommand DeleteColorCommand { get; }

        private void ExecuteSliderValueChanged(object parameter)
        {
           
            Color = new ColorModel
            {
                Red = Color.Red,
                Green = Color.Green,
                Blue = Color.Blue,
                Alpha = Color.Alpha
            };
           
            OnPropertyChanged(nameof(ColorToHex));
        }

        private void ExecuteAddColor(object parameter)
        {
            ColorList.Add(Color);
        }

        private void ExecuteDeleteColor(object parameter)
        {
            if (parameter is ColorModel colorToDelete)
            {
                ColorList.Remove(colorToDelete);
            }
        }
    }
}