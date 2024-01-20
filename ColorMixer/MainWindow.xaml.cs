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
            mcolor = new ColorRGBA();
            mcolor.red = 0;
            mcolor.green = 0;
            mcolor.blue = 0;
            mcolor.alpha = 255;
        }

        public class ColorRGBA
        {
            public byte red { get; set; }
            public byte green { get; set; }
            public byte blue { get; set; }
            public byte alpha { get; set; }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            string crlName = slider.Name;
            double value = slider.Value;
            if (crlName.Equals("AlphaColor"))
            {
                lblAlpha.Content = Convert.ToString(slider.Value);
                mcolor.alpha = Convert.ToByte(value);
            }
            if (crlName.Equals("RedColor"))
            {
                lblRed.Content = Convert.ToString(slider.Value);
                mcolor.red = Convert.ToByte(value);
            }
            if (crlName.Equals("GreenColor"))
            {
                lblGreen.Content = Convert.ToString(slider.Value);
                mcolor.green = Convert.ToByte(value);
            }
            if (crlName.Equals("BlueColor"))
            {
                lblBlue.Content = Convert.ToString(slider.Value);
                mcolor.blue = Convert.ToByte(value);
            }

            UpdateColor();
        }

        private void AlphaCheck_Checked(object sender, RoutedEventArgs e)
        {
            AlphaColor.IsEnabled = false;
        }

        private void AlphaCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            AlphaColor.IsEnabled = true;
        }

        private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblAlpha.Content = Convert.ToString(AlphaColor.Value);
            mcolor.alpha = Convert.ToByte(AlphaColor.Value);

            UpdateColor();
        }

        private void UpdateColor()
        {
            clr = Color.FromArgb(mcolor.alpha, mcolor.red, mcolor.green, mcolor.blue);
            this.sel_colour.Background = new SolidColorBrush(clr);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RedColor.IsEnabled = false;
        }

        private void GreenCheck_Checked(object sender, RoutedEventArgs e)
        {
            GreenColor.IsEnabled = false;
        }

        private void BlueCheck_Checked(object sender, RoutedEventArgs e)
        {
            BlueColor.IsEnabled = false;
        }

        private void RedCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            RedColor.IsEnabled = true;
        }

        private void GreenCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            GreenColor.IsEnabled = true;
        }

        private void BlueCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            BlueColor.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = new Button();
            deleteButton.Content = "Delete";
            deleteButton.Click += DeleteButton_Click;

            ListBoxItem myListBoxItem = new ListBoxItem();
            myListBoxItem.Content = Tuple.Create($"#{clr.A:X2}{clr.R:X2}{clr.G:X2}{clr.B:X2}", deleteButton);

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            TextBlock colorNumber = new TextBlock();
            colorNumber.Text = $"#{clr.A:X2}{clr.R:X2}{clr.G:X2}{clr.B:X2}";
            colorNumber.Margin = new Thickness(5, 0, 5, 0);

            grid.Children.Add(colorNumber);
            Grid.SetColumn(colorNumber, 0);

            Rectangle colorRectangle = new Rectangle();
            colorRectangle.Width = 650;
            colorRectangle.Height = 20;
            colorRectangle.Fill = new SolidColorBrush(clr);

            grid.Children.Add(colorRectangle);
            Grid.SetColumn(colorRectangle, 1);

            grid.Children.Add(deleteButton);
            Grid.SetColumn(deleteButton, 2);

            myListBoxItem.Content = grid;
            ListBox1.Items.Add(myListBoxItem);
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
