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
using System.Windows.Threading;

namespace Trade.Resourses.Pages
{
    /// <summary>
    /// Логика взаимодействия для Log_in.xaml
    /// </summary>
    public partial class Log_in : Page
    {
        public Log_in()
        {
            InitializeComponent();
            Roll(); // initialization with load page
        }

        public void Roll() // generate random properties
        {
            string allowchar = string.Empty;
            allowchar += "Q,W,E,R,T,Y,U,I,O,P,L,K,J,H,G,F,D,S,Z,X,C,V,B,N,M,q,w,e,r,t,y,u,i,o,p,l,k,j,h,g,f,d,s,a,z,x,c,v,b,n,m,1,2,3,4,5,6,7,8,9,0";
            char[] deleted_st = { ',' };
            string[] split_this = allowchar.Split(deleted_st);
            string pwd = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                string temp = split_this[(r.Next(0, split_this.Length))];
                pwd += temp;
            }
            Captcha.Text = pwd;
        }

        private void Reroll(object sender, RoutedEventArgs e) // initialization wirh btn
        {
            Roll();
        }

        private void Press_login_btn(object sender, RoutedEventArgs e)
        {
            /*
            if (passwordBox.Text != "123" && loginBox.Text != "asd")
            {
                btn_login.IsEnabled = false;
                MessageBox.Show("кнопка отключена", caption: "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                timer();
            }
            */
        }
        /*
        public void timer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Start();
            if (dispatcherTimer. == TimeSpan.Zero)
            {
                btn_login.IsEnabled = true;
                dispatcherTimer.Stop();
            }
        }
        */
        }
    }
