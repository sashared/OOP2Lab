using ClassLibrary;
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

namespace Lab2IT6222
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Menagerie menagerie = new Menagerie();
        private AnimalCreator creator = new AnimalCreator();

        public MainWindow()
        {
            InitializeComponent();
            lbAnimals.ItemsSource = menagerie.GetAnimals();
        }

        private void btnSaySelected_Click(object sender, RoutedEventArgs e)
        {
            var animal = (IComponent)lbAnimals.SelectedItem;
            if (animal == null)
            {
                DisplayMessage("No animal is selected");
                return;
            }

            SaySomethingCommon(animal);
        }

        private void btnSayAll_Click(object sender, RoutedEventArgs e)
        {
            SaySomethingCommon(menagerie);
        }

        private void SaySomethingCommon(IComponent component)
        {
            var result = component.SaySomething();
            DisplayMessage(result);
        }

        private void btnSetNight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                menagerie.SetNight();
                RefreshLb();
                DisplayMessage("The night has come");
            }
            catch(IsNightException)
            {
                DisplayMessage("It is already night");
            }
        }

        private void btnSetDay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                menagerie.SetDay();
                RefreshLb();
                DisplayMessage("The day has come");
            }
            catch (IsDayException)
            {
                DisplayMessage("It is already day");
            }
        }

        private void btnWakeUp_Click(object sender, RoutedEventArgs e)
        {
            var animal = (Animal)lbAnimals.SelectedItem;
            if (animal == null)
            {
                DisplayMessage("No animal is selected");
                return;
            }

            try
            {
                animal.WakeUp();
                RefreshLb();
                DisplayMessage($"{animal.Name} has woken up");
            }
            catch (IsNotSleepingException)
            {
                DisplayMessage($"{animal.Name} is not sleping");
            }
        }

        private void btnNightNight_Click(object sender, RoutedEventArgs e)
        {
            var animal = (Animal)lbAnimals.SelectedItem;
            if (animal == null)
            {
                DisplayMessage("No animal is selected");
                return;
            }

            try
            {
                animal.NightNight();
                RefreshLb();
                DisplayMessage($"{animal.Name} has gone to sleep");
            }
            catch (IsSleepingException)
            {
                DisplayMessage($"{animal.Name} is already sleping");
            }
        }

        private void DisplayMessage(string s)
        {
            MessageBox.Show(s);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var name = tbName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                DisplayMessage("The name is required");
                return;
            }

            if (!double.TryParse(tbWeight.Text, out var weight))
            {
                DisplayMessage("Could not parse the weight");
                return;
            }

            try
            {
                var animal = creator.CreateAnimal(name, weight);
                menagerie.AddAnimal(animal);
                RefreshLb();
                DisplayMessage("The menagerie has a new animal!");
            }
            catch(ArgumentOutOfRangeException ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void RefreshLb()
        {
            lbAnimals.ItemsSource = null;
            lbAnimals.ItemsSource = menagerie.GetAnimals();
            rtbMenagerie.SelectAll();
            rtbMenagerie.Cut();
            rtbMenagerie.AppendText(menagerie.ToString());
        }
    }
}
