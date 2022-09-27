# Demo_Exam

> **Plan**:
>> 1. :white_check_mark: Add Data Base
>> 2. :white_check_mark: Creating MainWindow (for convenient window layout)
>> 3. :white_check_mark: Creating class to connect DB in code
>> 3. :white_check_mark: Creating connect to Data Base in MainWindow code
>> 4. :black_square_button: Creating Log in page
>> 5. :black_square_button: Creating logic for Log in page
>> 6. :black_square_button: Creating GridView for data
>> 7. :black_square_button: Creating logic for GridView page
>> 8. *still working hard*


## Done
1. __Creating DB in VS__
  * First, to create a database, you need to create a new model ADO.NET ADM. Secondly, select the server and choose which database you will use. And finally, we need to create a Connect Helper class to use the data in the program. Code ConnectHelper.cs:
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
