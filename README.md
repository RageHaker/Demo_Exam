# Demo_Exam

> **Plan**:
>> 1. :white_check_mark: Add Data Base
>> 2. :white_check_mark: Creating MainWindow (for convenient window layout)
>> 3. :white_check_mark: Creating class to connect DB in code
>> 3. :white_check_mark: Creating connect to Data Base in MainWindow code
>> 4. :white_check_mark: Creating Log in page
>> 5. :white_check_mark: Creating logic for Log in page
>> 6. :black_square_button: Creating GridView for data
>> 7. :black_square_button: Creating logic for GridView page
>> 8. :black_square_button: Creating profile page
>> 9. :black_square_button: Creating locic for profile page
>> 9. *still working hard*


## Done
1. __Creating DB in VS__
  * First, to create a database, you need to create a new model ADO.NET ADM. Secondly, select the server and choose which database you will use. And finally, we need to create a Connect Helper class to use the data in the program. 
  * Code ConnectHelper.cs:
```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Resourses.Classes
{
    internal class ConnectHelper
    {
        public static TradeEntities obdEnt;
    }
}
```
2. __Creating Main window__
  * Code MainWindow.xaml: 
```XAML
<Window x:Class="Trade.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:Trade"
        mc:Ignorable="d" Icon="Resourses/Images/icon.ico"
        Title="Trade" Height="450" Width="800" >
    <Grid>
        <StackPanel>
            <StackPanel VerticalAlignment="Top" Height="100">

            </StackPanel>
            <StackPanel Height="335" VerticalAlignment="Center">
                <Frame NavigationUIVisibility="Hidden" x:Name="MainFrame" Source="Resourses/Pages/Log_in.xaml"/>
            </StackPanel>
        </StackPanel>
    </Grid>
```
   * Code MainWindow.xaml.cs:
```c#
using Trade.Resourses.Classes; // added path to classes

namespace Trade
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameApp.frmobj = MainFrame;
            ConnectHelper.obdEnt = new TradeEntities();
        }
    }
}
```
3. __Creating Log in page__
  * Code Log_in.xaml:
   ```XAML
   <Page x:Class="Trade.Resourses.Pages.Log_in"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
      xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
      xmlns:local="clr-namespace:Trade.Resourses.Pages"
      mc:Ignorable="d" 
      d:DesignHeight="350" d:DesignWidth="800"
      Title="Log_in">

    <Grid>
        <StackPanel VerticalAlignment="Center" HorizontalAlignment="Center">
            <StackPanel Orientation="Horizontal">
                <TextBlock Width="120" FontSize="15" Margin="5,5,5,5">
                    Введите логин:
                </TextBlock>
                <TextBox x:Name="loginBox" Width="200" TextAlignment="Left" Height="24" FontSize="15"/> 
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <TextBlock Width="120" FontSize="15" Margin="5,5,5,5">
                    Введите пароль:
                </TextBlock>
                <PasswordBox x:Name="passwordBox" Width="200" Height="24" FontSize="15"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal" HorizontalAlignment="Center" VerticalAlignment="Center" Margin="15,15,15,15">
                <TextBlock x:Name="Captcha" Width="140" Height="30" Text="xxxx" FontFamily="Jokerman" FontSize="20" TextAlignment="Center"/>
                <Button Click="Reroll" Width="80"  Margin="10,0,0,0">Пропустить</Button>
            </StackPanel>
            <StackPanel Orientation="Vertical" HorizontalAlignment="Center" VerticalAlignment="Center">
                <TextBox Width="120" Height="24" FontSize="15" TextAlignment="Center" MaxLength="4" x:Name="captchaBox"/>
                <Button Margin="0,15,0,0" x:Name="btn_login" Width="120" IsEnabled="{Binding AreButtonsIsEnabled}" Height="40" Click="Press_login_btn">
                    Войти
                </Button>
            </StackPanel>
        </StackPanel>
    </Grid>
</Page>
   ```
   * Code Log_in.xaml.cs:
   ```c#
using Trade.Resourses.Classes;

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

        public void Roll() // class Roll generate random properties
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
            Roll(); // class roll
        }

        private void Press_login_btn(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = ConnectHelper.obdEnt.User.FirstOrDefault(x => x.UserName.ToLower() == loginBox.Text.ToLower() && x.UserPassword == passwordBox.Password);
                if (userObj != null)
                {
                    MessageBox.Show("Данные отсутствуют или неправильны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    timer(); // class timer
                }
                else if (captchaBox.Text != Captcha.Text)
                {
                    MessageBox.Show("Неверная капча", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    timer(); // class timer
                }
                else
                {
                    FrameApp.frmobj.Navigate(new Pages.Table_about());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Critical damage" + ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public async void timer() // class timer disable button
        {
            btn_login.IsEnabled = false;
            await Task.Delay(10000); // 10000 milliseconds convert into 10 sec
            btn_login.IsEnabled = true;
        }
        
    }
}
   ```
Creating new class for navigate your menu
   * Class FrameApp.cs:
   ```c#
   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls; //added

namespace Trade.Resourses.Classes
{
    internal class FrameApp
    {
        public static Frame frmobj; // object type Frame
    }
}
   ```
