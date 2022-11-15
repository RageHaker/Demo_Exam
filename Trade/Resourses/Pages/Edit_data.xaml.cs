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
    /// Interaction logic for Edit_data.xaml
    /// </summary>
    public partial class Edit_data : Page
    {
        private Product _currentProduct = new Product();

        public Edit_data(Product product)
        {
            InitializeComponent();

            CmbProductCategory.ItemsSource = TradeEntities.GetContext().
               Product.Select(x => x.ProductCategory).Distinct().ToList();
            CmbProductManufacturer.ItemsSource = TradeEntities.GetContext().Product.
                Select(x => x.ProductManufacturer).Distinct().ToList();
            CmbProductEdin.Items.Add("шт");
            CmbProductEdin.Items.Add("кг");
            CmbProductEdin.Items.Add("куб.м");
            CmbProductEdin.Items.Add("кв.м");
            CmbProductEdin.Items.Add("мм");
            CmbProductEdin.Items.Add("м");
            CmbProductEdin.Items.Add("л");

            if (product != null)
                _currentProduct = product;
            ///создаем контекст
            DataContext = _currentProduct;
        }

        /// <summary>
        /// Save objects
        /// </summary>
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder(); ///объект для сообщения об ошибке

            ///проверка полей объекта
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductArticleNumber))
                error.AppendLine("Укажите артикул");
            if (string.IsNullOrWhiteSpace(_currentProduct.ProductName))
                error.AppendLine("Укажите название");
            if (_currentProduct.ProductCost <= 0)
                error.AppendLine("Стоимость должна быть больше нуля");
            if (_currentProduct.ProductQuantityInStock < 0)
                error.AppendLine("Количество не может быть отрицательным");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            ///если продукт новый
            if (TradeEntities.GetContext().Product.Find(_currentProduct.ProductArticleNumber) == null)
                TradeEntities.GetContext().Product.Add(_currentProduct); ///добавить в контекст
            try
            {
                TradeEntities.GetContext().SaveChanges(); /// сохранить изменения

                MessageBox.Show("Данные сохранены");
                FrameApp.frmobj.Navigate(new Table_about());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
