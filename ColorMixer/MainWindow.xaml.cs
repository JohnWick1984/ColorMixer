using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorMixer
{
    public partial class MainWindow : Window
    {
        public ColorRGBA mcolor { get; set; }
        public Color clr { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Grid grid = clickedButton?.Parent as Grid;

            if (grid != null)
            {
                ListBox1.Items.Remove(grid.Parent);
            }
        }
    }
}
