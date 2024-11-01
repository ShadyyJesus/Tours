using System;
using System.Windows;
using System.IO;
using System.Linq;

namespace LabWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ToursPage());
            Manager.MainFrame = MainFrame;
            //ImportTours();
        }

        private void ImportTours()
        {
            var fileData = File.ReadAllLines(@"C:\Users\Демоэкзамен\Desktop\ИСИП-22-1\Христофоров А. И\Туры.txt");
            var image = Directory.GetFiles(@"C:\Users\Демоэкзамен\Desktop\ИСИП-22-1\Христофоров А. И\Туры фото");
            
            foreach (var line in fileData)
            {
                var data = line.Split('\t');
                var tempTour = new Туры
                {
                    Название = data[0].Replace("\"", ""),
                    Код_страны = data[1].ToString(),
                    Количество_билетов = int.Parse(data[2]),
                    Стоимость = decimal.Parse(data[3]),
                    Статус = (data[4] == "0") ? false : true
                };
                foreach (var tourType in data[5].Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries))
                {
                    foreach (var currentType in toursEntities.GetContext().Тип_Тур.ToList())
                    {
                        if (currentType.Наименование.Trim() == tourType.Trim())
                            tempTour.Тип_Тур.Add(currentType);
                    }
                       
                }
                try
                {
                    tempTour.Превью = File.ReadAllBytes(image.FirstOrDefault(p => p.Contains(tempTour.Название)));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                toursEntities.GetContext().Туры.Add(tempTour);
                toursEntities.GetContext().SaveChanges();
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}
