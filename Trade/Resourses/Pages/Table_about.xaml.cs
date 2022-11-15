using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

            TxbCountItems.Text =Convert.ToString(TradeEntities.GetContext().Product.Count());
            TxbCountSearchItems.Text = Convert.ToString(TradeEntities.GetContext().Product.Count());
        }

        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {///сортировка по возрастанию стоимости
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.
                OrderBy(x => x.ProductCost).ToList();
        }

        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {///сортировка по убыванию стоимости
            LViewProduct.ItemsSource = TradeEntities.GetContext().Product.
                OrderByDescending(x => x.ProductCost).ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {///поиск
            string search = TxtSearch.Text;
            if (TxtSearch.Text != null)
                LViewProduct.ItemsSource = TradeEntities.GetContext().Product.Where(x => x.ProductManufacturer.Contains(search)
                    || x.ProductName.Contains(search)
                    || x.ProductDescription.Contains(search)
                    || x.ProductCost.ToString().Contains(search)).ToList();
            TxbCountSearchItems.Text = TradeEntities.GetContext().Product.Where(x => x.ProductManufacturer.Contains(search)
                    || x.ProductName.Contains(search)
                    || x.ProductDescription.Contains(search)
                    || x.ProductCost.ToString().Contains(search)).Count().ToString();
        }

        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {///фильтрация по производителю

            if (CmbFiltr.SelectedValue.ToString() == "Все производители")
            {
                LViewProduct.ItemsSource = TradeEntities.GetContext().Product.ToList();
            }
            else
            {
                LViewProduct.ItemsSource = TradeEntities.GetContext().Product.
                    Where(x => x.ProductManufacturer == CmbFiltr.SelectedValue.ToString()).ToList();
                TxbCountSearchItems.Text = TradeEntities.GetContext().Product.Where(x => x.ProductManufacturer == CmbFiltr.SelectedValue.ToString()).Count().ToString();
            }
            }

        private void Delete_data(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Удалить данные?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                {
                   Product prodDel = LViewProduct.SelectedItem as Product;
                   TradeEntities.GetContext().Product.Remove(prodDel);
                   TradeEntities.GetContext().SaveChanges();
                   LViewProduct.ItemsSource = ConnectHelper.obdEnt.Product.ToList();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Данный товар находится в заказе", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Edit_data(object sender, RoutedEventArgs e)
        {
            FrameApp.frmobj.Navigate(new Edit_data());
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmobj.Navigate(new Add_data());
        }
    }
}
