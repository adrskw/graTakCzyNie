using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using graTakCzyNieLibrary;

namespace graTakCzyNie
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private Engine engine;
        private int FieldId { get; set; } = 0;
        private byte GameBoardWidth { get; } = 14;
        private byte GameBoardHeight { get; } = 12;
        private Dictionary<int, TextBlock> playerNameTextBlocks = new Dictionary<int, TextBlock>();
        private readonly DoubleAnimation diceLoadingAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(500)),
            AutoReverse = true
        };
        private readonly DoubleAnimation diceGridFadeInAnimation = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = new Duration(TimeSpan.FromMilliseconds(300))
        };
        private readonly DoubleAnimation diceGridFadeOutAnimation = new DoubleAnimation
        {
            From = 1,
            To = 0,
            Duration = new Duration(TimeSpan.FromMilliseconds(300))
        };

        public Game(Engine engine)
        {            
            InitializeComponent();
            this.engine = engine;

            GenerateBoard(GameBoardWidth, GameBoardHeight);
            GenerateDice(6);
            GeneratePlayersList();
        }

        /// <summary>
        /// Generowanie pola o danym położeniu na planszy 
        /// </summary>
        /// <param name="x">lokalizacja x</param>
        /// <param name="y">lokalizacja y</param>
        private void GenerateFieldOnBoardGrid(int x, int y)
        {
            Border fieldBorder = new Border
            {
                Background = new SolidColorBrush(Colors.LightGray),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1)
            };
            Grid.SetColumn(fieldBorder, x);
            Grid.SetRow(fieldBorder, y);
            fieldBorder.SetValue(FrameworkElement.NameProperty, "Field" + FieldId);

            UniformGrid pawnsWrap = new UniformGrid
            {
                Columns = 2,
                Rows = 2
            };

            fieldBorder.Child = pawnsWrap;

            GridGameBoard.Children.Add(fieldBorder);
            FieldId++;
        }

        /// <summary>
        /// Generowanie planszy na podstawie jej wielkości
        /// </summary>
        /// <param name="width">szerokość planszy</param>
        /// <param name="height">wysokość planszy</param>
        private void GenerateBoard(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                GridGameBoard.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }

            for (int i = 0; i < height; i++)
            {
                GridGameBoard.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }

            for (int j = 0; j < (width - 2) / 4; j++)
            {
                for (int i = 1; i < height - 1; i++)
                {
                    GenerateFieldOnBoardGrid(j*4 + 1, i);
                }

                GenerateFieldOnBoardGrid(j * 4 + 2, height - 2);
                
                for (int i = height - 2; i > 0; i--)
                {
                    GenerateFieldOnBoardGrid(j * 4 + 3, i);
                }

                GenerateFieldOnBoardGrid(j * 4 + 4, 1);
            }
        }

        /// <summary>
        /// Generowanie kostki do gry
        /// </summary>
        /// <param name="number">Liczba od 1 do 6</param>
        private async void GenerateDice(byte number)
        {
            void GenerateDot(Grid diceGridElement, int x, int y)
            {
                Ellipse dot = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Black),
                    Margin = new Thickness(1, 1, 1, 1)
                };

                Grid.SetColumn(dot, x);
                Grid.SetRow(dot, y);
                diceGridElement.Children.Add(dot);
            }
            
            GridDice.BeginAnimation(OpacityProperty, diceGridFadeOutAnimation);
            await Task.Delay(300);

            GridDice.Children.Clear();
            GridDice.Visibility = Visibility.Collapsed;
            MediaElementDiceLoading.Visibility = Visibility.Visible;
            MediaElementDiceLoading.BeginAnimation(OpacityProperty, diceLoadingAnimation);

            switch (number)
            {
                case 1:
                    GenerateDot(GridDice, 2, 2);
                    break;
                case 2:
                    GenerateDot(GridDice, 1, 1);
                    GenerateDot(GridDice, 3, 3);
                    break;
                case 3:
                    GenerateDot(GridDice, 1, 1);
                    GenerateDot(GridDice, 2, 2);
                    GenerateDot(GridDice, 3, 3);
                    break;
                case 4:
                    GenerateDot(GridDice, 1, 1);
                    GenerateDot(GridDice, 3, 1);
                    GenerateDot(GridDice, 1, 3);
                    GenerateDot(GridDice, 3, 3);
                    break;
                case 5:
                    GenerateDot(GridDice, 1, 1);
                    GenerateDot(GridDice, 3, 1);
                    GenerateDot(GridDice, 2, 2);
                    GenerateDot(GridDice, 1, 3);
                    GenerateDot(GridDice, 3, 3);
                    break;
                case 6:
                    GenerateDot(GridDice, 1, 1);
                    GenerateDot(GridDice, 1, 2);
                    GenerateDot(GridDice, 1, 3);
                    GenerateDot(GridDice, 3, 1);
                    GenerateDot(GridDice, 3, 2);
                    GenerateDot(GridDice, 3, 3);
                    break;
            }

            await Task.Delay(700);

            GridDice.BeginAnimation(OpacityProperty, diceGridFadeInAnimation);
            GridDice.Visibility = Visibility.Visible;
            MediaElementDiceLoading.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Generowanie listy graczy
        /// </summary>
        private void GeneratePlayersList()
        {
            List<Player> players = engine.PlayersList;
            int currentPlayer = 0;

            foreach (Player player in players)
            {
                Rectangle rectangle = new Rectangle
                {
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(player.Color)),
                    Margin = new Thickness(0, 0, 0, 10)
                };

                Grid.SetColumn(rectangle, 0);
                Grid.SetRow(rectangle, currentPlayer);
                GridPlayersList.Children.Add(rectangle);

                TextBlock textBlock = new TextBlock
                {
                    Text = player.Name,
                    Margin = new Thickness(10, 0, 0, 10)
                };

                if (player.IsComputer)
                {
                    textBlock.Text += " (komputer)";
                }
                Grid.SetColumn(textBlock, 1);
                Grid.SetRow(textBlock, currentPlayer);
                GridPlayersList.Children.Add(textBlock);
                playerNameTextBlocks.Add(player.Id, textBlock);

                currentPlayer++;
            }
        }

        /// <summary>
        /// Zapętlenie gifa wyświelającego wylosowaną liczbę na kostce
        /// </summary>
        private void MediaElementDiceLoading_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElementDiceLoading.Position = new TimeSpan(0, 0, 1);
            MediaElementDiceLoading.Play();
        }
    }
}
