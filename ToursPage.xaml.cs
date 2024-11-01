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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();
            var allTypes = toursEntities.GetContext().Тип_Тур.ToList();
            allTypes.Insert(0, new Тип_Тур
            {
                Наименование = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            CheckActual.IsChecked = true;
            ComboType.SelectedIndex = 0;


            var currentTours = toursEntities.GetContext().Туры.ToList();
            LViewTours.ItemsSource = currentTours;

            UpdateTour();
        }

        private void UpdateTour()
        {
            var courrentTour = toursEntities.GetContext().Туры.ToList();
            if(ComboType.SelectedIndex > 0)
                courrentTour = courrentTour.Where(p => p.Тип_Тур.Contains(ComboType.SelectedItem as Тип_Тур)).ToList();

            courrentTour = courrentTour.Where(p => p.Название.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (CheckActual.IsChecked.Value)
                courrentTour = courrentTour.Where(p => p.Статус.Value).ToList();
            LViewTours.ItemsSource = courrentTour.OrderBy(p => p.Количество_билетов).ToList();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTour();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTour();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTour();
        }
    }
}
