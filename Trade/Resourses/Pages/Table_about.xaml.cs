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
using Trade.Resourses.Classes;

namespace Trade.Resourses.Pages
{
    /// <summary>
    /// Логика взаимодействия для Table_about.xaml
    /// </summary>
    public partial class Table_about : Page
    {
        public Table_about()
        {
            InitializeComponent();

            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.ToList();

            CmbFiltr.Items.Add("Все производители");
            foreach (var item in TradeEntities.GetContext().Product.Select(x => x.ProductManufacturer).Distinct().ToList())
                CmbFiltr.Items.Add(item);
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {//поиск по нажатию на кнопку
            string search = TxtSearch.Text;
            if(TxtSearch.Text != null)
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.Where(x=>x.ProductArticleNumber.Contains(search) || x.ProductManufacturer.Contains(search) || x.ProductDescription.Contains(search) || x.ProductCost.ToString().Contains(search)).ToList();
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {//сортировка по возрастанию стоимости
            if (TxtSearch.Text != null)
                LViewProduct.ItemsSource = TradeEntities.GetContext().Product.OrderBy(x=>x.ProductCost).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {//сортировка по убыванию стоимости
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.OrderByDescending(x => x.ProductCost).ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {//поиск после изменения текста
            string search = TxtSearch.Text;
            if (TxtSearch.Text != null)
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.Where(x => x.ProductArticleNumber.Contains(search) || x.ProductManufacturer.Contains(search) || x.ProductDescription.Contains(search) || x.ProductCost.ToString().Contains(search)).ToList();
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.Where(x => x.ProductManufacturer == CmbFiltr.Text).ToList();
        }
    }
}
