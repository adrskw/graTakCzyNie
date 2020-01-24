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
using graTakCzyNieLibrary;

namespace graTakCzyNie
{
    /// <summary>
    /// Interaction logic for GameOver.xaml
    /// </summary>
    public partial class GameOver : Window
    {
        private Game gameWindow;
        public GameOver(Player winner, Game gameWindow)
        {
            InitializeComponent();
            this.gameWindow = gameWindow;

            TextBlockWinner.Text = winner.Name;
        }

        private void ButtonMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            gameWindow.Close();
        }

        private void ButtonNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame newGameWindow = new NewGame();
            newGameWindow.Show();
            gameWindow.Close();
        }
    }
}
