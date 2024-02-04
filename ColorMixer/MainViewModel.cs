using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;


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
    public class   MainViewModel : ViewModelBase
    {
        private ColorRGBA _color;
        private bool _alphaCheckState;
        private double _alphaValue;

        public ColorRGBA Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public bool AlphaCheckState
        {
            get => _alphaCheckState;
            set
            {
                _alphaCheckState = value;
                OnPropertyChanged(nameof(AlphaCheckState));
            }
        }

        public double AlphaValue
        {
            get => _alphaValue;
            set
            {
                _alphaValue = value;
                OnPropertyChanged(nameof(AlphaValue));
            }
        }

        public ICommand AddColorCommand { get; }
        public ICommand DeleteColorCommand { get; }

        public ObservableCollection<Color> ColorList { get; }

        public MainViewModel()
        {
            Color = new ColorRGBA() { Red = 0, Green = 0, Blue = 0, Alpha = 255 };
            AlphaCheckState = false;
            AlphaValue = 0;
            AddColorCommand = new RelayCommand(ExecuteAddColor);
            DeleteColorCommand = new RelayCommand(ExecuteDeleteColor);
            ColorList = new ObservableCollection<Color>();
        }
        private Color _selectedColor;

        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }
        private void ExecuteAddColor(object parameter)
        {
            // Логика добавления цвета в ColorList
            ColorList.Add(Color.Color);
        }

        private void ExecuteDeleteColor(object parameter)
        {
            // Логика удаления цвета из ColorList
            if (parameter is Color colorToDelete)
            {
                ColorList.Remove(colorToDelete);
            }
        }
    }
}

