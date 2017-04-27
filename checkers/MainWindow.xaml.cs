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

namespace checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            for(int i = 0; i < 8; i++)
            {   
                for(int j = 0; j < 8; j++)
                {
                    Rectangle boardField = new Rectangle();
                    SolidColorBrush brush = new SolidColorBrush();

                    if ((j % 2 == 1 && i % 2 == 1) || (j % 2 == 0 && i % 2 == 0))
                    {
                        brush.Color = Colors.White;
                        boardField.Fill = brush;
                    }
                    else
                    {
                        brush.Color = Colors.Gray;
                        boardField.Fill = brush;
                    }

                    board.Children.Add(boardField);
                    Grid.SetRow(boardField, i);
                    Grid.SetColumn(boardField, j);
                }
            }
        }
    }
}
