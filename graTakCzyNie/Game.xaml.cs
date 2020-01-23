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
        private Dictionary<int, Rectangle> playersPawnsRectangles = new Dictionary<int, Rectangle>();
        private int currentTurnPlayerId = 0;
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
            SetPlayerTurn(0);

            // zainicjowane wyświetlania pionków na planszy
            foreach (Player player in engine.PlayersList)
            {
                GeneratePlayersPawnOnBoard(player);
            }
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
        private async void GenerateDice(int number)
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
            ButtonRollDice.IsEnabled = false;

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
            ButtonRollDice.IsEnabled = true;
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
        /// Wyświetlenie tury danego gracza
        /// </summary>
        /// <param name="id">id gracza</param>
        private void SetPlayerTurn(int id)
        {
            playerNameTextBlocks[id].FontWeight = FontWeights.Bold;

            if (id - 1 < 0)
            {
                playerNameTextBlocks[playerNameTextBlocks.Count - 1].FontWeight = FontWeights.Normal;
            }
            else
            {
                playerNameTextBlocks[id - 1].FontWeight = FontWeights.Normal;
            }
        }

        /// <summary>
        /// Dodawanie odpowiednich marginesów dla wszystkich pionków na danym polu
        /// </summary>
        /// <param name="fieldGrid">grid pola</param>
        private void AddMarginsToPawnsOnField(UniformGrid fieldGrid)
        {
            for (int i = 0; i < fieldGrid.Children.Count; i++)
            {
                Thickness pawnMargin;

                switch (i)
                {
                    case 1:
                        pawnMargin = new Thickness(2, 4, 4, 2);
                        break;
                    case 2:
                        pawnMargin = new Thickness(4, 2, 2, 4);
                        break;
                    case 3:
                        pawnMargin = new Thickness(2, 2, 4, 4);
                        break;
                    case 0:
                    default:
                        pawnMargin = new Thickness(4, 4, 2, 2);
                        break;
                }

                Rectangle pawn = fieldGrid.Children[i] as Rectangle;
                pawn.Margin = pawnMargin;
            }
        }
        
        /// <summary>
        /// Generowanie pionka na planszy
        /// </summary>
        /// <param name="player">obiekt gracza</param>
        private void GeneratePlayersPawnOnBoard(Player player)
        {
            Rectangle oldPawn;
            if (playersPawnsRectangles.TryGetValue(player.Id, out oldPawn))
            {
                UniformGrid oldFieldGrid = VisualTreeHelper.GetParent(oldPawn) as UniformGrid;
                oldFieldGrid.Children.Remove(oldPawn);
                playersPawnsRectangles.Remove(player.Id);
            }

            Border field = GridGameBoard.Children[player.CurrentPosition] as Border;
            UniformGrid fieldGrid = field.Child as UniformGrid;

            Rectangle newPawn = new Rectangle
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(player.Color)),
                Stretch = Stretch.UniformToFill
            };
            playersPawnsRectangles.Add(player.Id, newPawn);
            fieldGrid.Children.Add(newPawn);

            AddMarginsToPawnsOnField(fieldGrid);
        }

        /// <summary>
        /// Zapętlenie gifa wyświelającego wylosowaną liczbę na kostce
        /// </summary>
        private void MediaElementDiceLoading_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElementDiceLoading.Position = new TimeSpan(0, 0, 1);
            MediaElementDiceLoading.Play();
        }

        private async void ButtonRollDice_Click(object sender, RoutedEventArgs e)
        {
            int randomResult = await Cube.GetRandomResult(1, 6);
            GenerateDice(randomResult);
            
            Player player = engine.PlayersList.FirstOrDefault(f => f.Id == currentTurnPlayerId);
            EngineResult engineResult = await engine.Move(player, randomResult);

            SetPlayerTurn(engine.NextTurnPlayerId);
            currentTurnPlayerId = engine.NextTurnPlayerId;

            if (engineResult.Succedeed)
            {
                string displayedMessage = "";

                switch (engineResult.Field)
                {
                    case Field.Meta:
                        displayedMessage = string.Format("Gracz {0} zwyciężył i dotarł na metę jako pierwszy. Gratulacje!", player.Name);
                        // to do: endGame
                        break;
                    case Field.Question:
                        // to do: question
                        break;
                    case Field.Penalty:
                        displayedMessage = string.Format("{0} miałeś pecha i trafiłeś na pole karne. Cofasz się o 3 pola", player.Name);
                        break;
                    case Field.Bonus:
                        displayedMessage = string.Format("{0}, szczęście Ci dopisało! Idziesz 3 pola do przodu", player.Name);
                        break;
                    case Field.Trap:
                        displayedMessage = string.Format("Uwaga pułapka! {0} został uwięziony na 3 tury", player.Name);
                        break;
                }

                await Task.Delay(1000);
                GeneratePlayersPawnOnBoard(player);

                MessageBox.Show(displayedMessage);
            }
            else
            {
                MessageBox.Show(engineResult.ErrorMessage);
            }
        }
    }
}
