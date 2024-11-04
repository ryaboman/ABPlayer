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
    /// Логика взаимодействия для BtnPlay.xaml
    /// </summary>
    public partial class BtnPlay : UserControl{
        public delegate void ChangeStateButtonEventHandler();

        public event ChangeStateButtonEventHandler WasChangeStateButton;

        enum StateButton { pushUp, pushIn}

        StateButton btnState = StateButton.pushUp;

        public BtnPlay(){
            InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e) {
            if (btnState == StateButton.pushUp){
                SetBtnStateInPlay();
            }
            else{
                SetBtnStateInPause();
            }

            WasChangeStateButton?.Invoke();
        }

        public void SetBtnStateInPlay() {
            btnState = StateButton.pushIn;

            pauseBtn.Visibility = Visibility.Visible;

            playBtn.Visibility = Visibility.Hidden;
        }

        public void SetBtnStateInPause() {
            btnState = StateButton.pushUp;

            pauseBtn.Visibility = Visibility.Hidden;

            playBtn.Visibility = Visibility.Visible;
        }
    }
}
