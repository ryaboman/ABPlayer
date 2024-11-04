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
    /// Логика взаимодействия для Slider.xaml
    /// </summary>
    public partial class Slider : UserControl{

        //Объявление нового типа делегата для реализации события
        public delegate void ChangeThumbPossitionEventHandler(TimeSpan value);

        public event ChangeThumbPossitionEventHandler WasChangeThumbPossition;

        //если ползунок захвачен мышью
        bool leftButtonIsDown = false;

        //ширина индикатора
        double indicatorWidth = 0;

        //старое положение ползунка
        Point oldPossitionMouse = new Point(0, 0);

        //максимальное значение ползунка
        public int maxValue{ set; get; }

        //текущее значение ползунка
        private double currentValue;

        public Slider(){
            InitializeComponent();

            //скрываем ползунок. Так как ползунок видно только при наведении мышью.
            circle.Visibility = Visibility.Hidden;
            //прогресс ползунка
            prog1.Width = 0;

            indicatorWidth = sliderBorder.Width;
        }
        
        private void MoveSlider(Point point){
            if(point.X > 0){
                prog1.Width = point.X;
            }
            else{
                prog1.Width = 0;
            }
            

            Thickness margin = new Thickness();

            margin.Left = point.X - circle.Width / 2;

            circle.Margin = (margin.Left > 0) ? margin : new Thickness();
        }

        private void rectangle_MouseLeave(object sender, MouseEventArgs e){
            circle.Visibility = Visibility.Hidden;

            sliderBorder.Height = 8;
            sliderBorder.CornerRadius = new CornerRadius(4);

            prog1.Height = 8;
            prog1.CornerRadius = new CornerRadius(4);            
        }

        private void rectangle_MouseUp(object sender, MouseButtonEventArgs e){
            circle_MouseUp(sender, e);            
        }

        public double GetCurrentValue(){
            return currentValue;
        }

        public void SetCurrentValue(double value){
            if (value <= maxValue && maxValue > 0){
                currentValue = value;

                double result = currentValue * indicatorWidth / maxValue;


                Point width = new Point(result, 0);

                if (!leftButtonIsDown)
                    MoveSlider(width);
            }
            else{
                currentValue = 0;
                //устанавливаем инидикатор в ноль

                Point width = new Point(currentValue, 0);

                if (!leftButtonIsDown)
                    MoveSlider(width);
            }
        }


        private void circle_MouseMove(object sender, MouseEventArgs e){
            if (leftButtonIsDown){
                Point pointMouse = e.GetPosition(prog1);

                MoveSlider(pointMouse);
            }
        }

        private void circle_MouseDown(object sender, MouseButtonEventArgs e){
            if(Mouse.LeftButton == MouseButtonState.Pressed){
                leftButtonIsDown = true;
                oldPossitionMouse.X = prog1.Width;
            }
        }        

        //Событие перемещения ползунка "circle"
        private void circle_MouseUp(object sender, MouseButtonEventArgs e){
            if (Mouse.LeftButton != MouseButtonState.Pressed){
                leftButtonIsDown = false;

                Point pointMouse = e.GetPosition(prog1);

                double result = maxValue * pointMouse.X / indicatorWidth;

                currentValue = result;                

                MoveSlider(pointMouse);

                //создаем событие
                WasChangeThumbPossition?.Invoke( TimeSpan.FromSeconds(currentValue) );
            }
        }

        //Функция отменяет перемещения слайдера
        public void cancelMove(){
            MoveSlider(oldPossitionMouse); //currentValue
        }


        private void rectangle_MouseEnter(object sender, MouseEventArgs e){
            circle.Visibility = Visibility.Visible;

            sliderBorder.Height = 10;
            sliderBorder.CornerRadius = new CornerRadius(5);

            prog1.Height = 10;
            prog1.CornerRadius = new CornerRadius(5);
        }

        private void circle_MouseEnter(object sender, MouseEventArgs e){
            this.Cursor = Cursors.Hand;            
            rectangle_MouseEnter(sender, e);
        }

        private void circle_MouseLeave(object sender, MouseEventArgs e){
            this.Cursor = Cursors.Arrow;
            if (leftButtonIsDown){
                leftButtonIsDown = false;
                cancelMove();
            }
        }
       
    }
}
