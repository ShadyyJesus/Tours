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

namespace LabWPF
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Отели _currentHotel = new Отели();

        public AddEditPage(Отели selectedHotel)
        {
            InitializeComponent();

            if(selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = toursEntities.GetContext().Страны.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentHotel.Название))
                errors.AppendLine("Укажите название отели");
            if (_currentHotel.Количество_звезд < 1 || _currentHotel.Количество_звезд > 5)
                errors.AppendLine("Количество звезд - число от 1 до 5");
            if (_currentHotel.Код_страны == null)
                errors.AppendLine("Выберите страну");
    
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentHotel.Код_отеля == 0)
                toursEntities.GetContext().Отели.Add(_currentHotel);
            try
            {
                toursEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранения");
                Manager.MainFrame.Navigate(new HotelsPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
