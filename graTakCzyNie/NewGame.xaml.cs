using graTakCzyNieLibrary;
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

        private Engine engine = new Engine();

        public NewGame()
        {
            InitializeComponent();

            // ustawienie domyślnej ilości graczy na 2
            ComboBoxTotalPlayers.SelectedIndex = 0;

            // dodanie eventu lewego kliknięcia myszy dla 4 prostokątów odpowiedzialnych za kolor gracza
            Player0_Color.MouseLeftButtonDown += ChangePlayerColor;
            Player1_Color.MouseLeftButtonDown += ChangePlayerColor;
            Player2_Color.MouseLeftButtonDown += ChangePlayerColor;
            Player3_Color.MouseLeftButtonDown += ChangePlayerColor;
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
        /// Dodawanie listy graczy do silnika gry
        /// </summary>
        private async Task<EngineResult> AddPlayersToEnginePlayerList()
        {
            List<Player> players = new List<Player>();
            List<string> usedColors = new List<string>();

            ComboBoxItem totalPlayersSelectedItem = ComboBoxTotalPlayers.SelectedItem as ComboBoxItem;
            int totalPlayers = Convert.ToInt32(totalPlayersSelectedItem.Content.ToString());

            for (int i = 0; i < totalPlayers; i++)
            {
                TextBox nameTextBox = GridPlayerInfo.FindName("Player" + i + "_Name") as TextBox;
                string name = nameTextBox.Text;

                Rectangle colorRectangle = GridPlayerInfo.FindName("Player" + i + "_Color") as Rectangle;
                string color = colorRectangle.Fill.ToString();

                ComboBox typeComboBox = GridPlayerInfo.FindName("Player" + i + "_Type") as ComboBox;
                ComboBoxItem typeSelectedItem = typeComboBox.SelectedItem as ComboBoxItem;
                string type = typeSelectedItem.Content.ToString();
                bool isComputer = false;

                if (type == "Komputer")
                {
                    isComputer = true;
                }

                players.Add(new Player(i, name, color, isComputer));
            }

            return await engine.CreatePlayers(players);
        }

        /// <summary>
        /// Ukrywanie/pokazywanie pól gracza na podstawie wybranej ilości
        /// </summary>
        private void ComboBoxTotalPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = ComboBoxTotalPlayers.SelectedItem as ComboBoxItem;
            string value = selectedItem.Content.ToString();

            switch (value)
            {
                case "2":
                    Player2_Color.Visibility =
                        Player2_Name.Visibility =
                        Player2_Type.Visibility =
                        Player3_Color.Visibility =
                        Player3_Name.Visibility =
                        Player3_Type.Visibility = Visibility.Collapsed;
                    break;
                case "3":
                    Player2_Color.Visibility = 
                        Player2_Name.Visibility = 
                        Player2_Type.Visibility = Visibility.Visible;
                    Player3_Color.Visibility = 
                        Player3_Name.Visibility =
                        Player3_Type.Visibility = Visibility.Collapsed;
                    break;
                case "4":
                default:
                    Player2_Color.Visibility =
                        Player2_Name.Visibility =
                        Player2_Type.Visibility =
                        Player3_Color.Visibility =
                        Player3_Name.Visibility = 
                        Player3_Type.Visibility = Visibility.Visible;
                    break;
            }
        }

        private async void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            EngineResult addPlayersResult = await AddPlayersToEnginePlayerList();
            EngineResult startGameResult = await engine.StartGame(66);

            if (addPlayersResult.Succedeed == false)
            {
                MessageBox.Show(addPlayersResult.ErrorMessage);
            }
            else if (startGameResult.Succedeed == false)
            {
                MessageBox.Show(startGameResult.ErrorMessage);
            }
            else
            {
                Game game = new Game(engine);
                game.Show();
                this.Close();
            }
        }
    }
}
