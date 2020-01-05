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
using System.Windows.Shapes;

namespace graTakCzyNie
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        // lista dostępnych kolorów dla graczy
        private List<Color> AvailablePlayerColors { get; } = new List<Color> {
            Colors.Black,
            Colors.Red,
            Colors.Magenta,
            Colors.Blue,
            Colors.Purple
        };

        public NewGame()
        {
            InitializeComponent();

            // ustawienie domyślnej ilości graczy na 2
            totalPlayers.SelectedIndex = 0;

            // dodanie eventu lewego kliknięcia myszy dla 4 prostokątów odpowiedzialnych za kolor gracza
            player1_color.MouseLeftButtonDown += ChangePlayerColor;
            player2_color.MouseLeftButtonDown += ChangePlayerColor;
            player3_color.MouseLeftButtonDown += ChangePlayerColor;
            player4_color.MouseLeftButtonDown += ChangePlayerColor;
        }

        /// <summary>
        /// Zmienia kolor prostokąta przy danym graczu na kolejny na liście dostępnych kolorów
        /// </summary>
        private void ChangePlayerColor(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = (Rectangle)sender;
            SolidColorBrush solidColorBrush = rectangle.Fill as SolidColorBrush;

            int currentColorIndex = AvailablePlayerColors.IndexOf(solidColorBrush.Color);

            if (currentColorIndex < AvailablePlayerColors.Count - 1)
            {
                rectangle.Fill = new SolidColorBrush(AvailablePlayerColors[currentColorIndex + 1]);
            }
            else
            {
                rectangle.Fill = new SolidColorBrush(AvailablePlayerColors[0]);
            }
        }

        /// <summary>
        /// Ukrywanie/pokazywanie pól gracza na podstawie wybranej ilości
        /// </summary>
        private void TotalPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = totalPlayers.SelectedItem as ComboBoxItem;
            string value = selectedItem.Content.ToString();

            switch (value)
            {
                case "2":
                    player3_color.Visibility =
                        player3_name.Visibility =
                        player3_type.Visibility =
                        player4_color.Visibility =
                        player4_name.Visibility =
                        player4_type.Visibility = Visibility.Collapsed;
                    break;
                case "3":
                    player3_color.Visibility = 
                        player3_name.Visibility = 
                        player3_type.Visibility = Visibility.Visible;
                    player4_color.Visibility = 
                        player4_name.Visibility =
                        player4_type.Visibility = Visibility.Collapsed;
                    break;
                case "4":
                default:
                    player3_color.Visibility =
                        player3_name.Visibility =
                        player3_type.Visibility =
                        player4_color.Visibility =
                        player4_name.Visibility = 
                        player4_type.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
