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

namespace RMControls.Player {
    /// <summary>
    /// Логика взаимодействия для BtnForward.xaml
    /// </summary>
    public partial class BtnForward : UserControl{

        public delegate void WasClickButtonEventHandler();

        public event WasClickButtonEventHandler WasClickButton;

        public BtnForward(){
            InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e) {
            WasClickButton?.Invoke();
        }

    }
}
