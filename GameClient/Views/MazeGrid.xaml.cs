using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using GameClient.ViewModel;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MazeGrid.xaml
    /// </summary>
    public partial class MazeGrid : UserControl
    {
        private Rectangle[,] board;

        /// <summary>
        /// The player cell.
        /// </summary>
        private Rectangle playerCell;

        /// <summary>
        /// The border painter.
        /// </summary>
        private Brush borderPainter;

        /// <summary>
        /// The path painter.
        /// </summary>
        private Brush pathPainter;

        /// <summary>
        /// The player painter.
        /// </summary>
        private Brush playerPainter;

        /// <summary>
        /// The destination painter.
        /// </summary>
        private Brush destinationPainter;

        /// <summary>
        /// Initializes user control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();

            board = new Rectangle[Rows, Cols];

            double cellHeight = MazeCanvas.Height / Rows;
            double cellWidth = MazeCanvas.Width / Cols;

            // Initializing painters.
            pathPainter = new SolidColorBrush(Colors.White);
            borderPainter = new SolidColorBrush(Colors.Black);

            //Sets images.
            playerPainter = new ImageBrush
            {
                ImageSource =
                    new BitmapImage(
                        new Uri(
                            @"pack://application:,,,/GameClient;component/images/player.png"))
            };

            destinationPainter = new ImageBrush
            {
                ImageSource =
                    new BitmapImage(
                        new Uri(
                            @"pack://application:,,,/GameClient;component/images/exit.png"))
            };

            //Paints the cells.
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Cols; column++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = cellHeight,
                        Width = cellWidth,
                        Fill = Maze[row * Cols + column] == '1'
                            ? borderPainter
                            : pathPainter
                    };

                    MazeCanvas.Children.Add(rectangle);

                    Canvas.SetLeft(rectangle, column * cellWidth);
                    Canvas.SetTop(rectangle, row * cellHeight);

                    board[row, column] = rectangle;
                }
            }

            //Creates the player cell.
            playerCell = new Rectangle
            {
                Height = cellHeight,
                Width = cellWidth,
                Fill = playerPainter
            };

            // Paint player cell.
            MazeCanvas.Children.Add(playerCell);

            Canvas.SetLeft(playerCell,
                ColumnCoordinate(InitialPos) * cellWidth);

            Canvas.SetTop(playerCell, RowCoordinate(InitialPos) * cellHeight);

            // Paint destination cell.
            board[RowCoordinate(GoalPos), ColumnCoordinate(GoalPos)].Fill =
                destinationPainter;

            //Initialize base.
            base.OnInitialized(e);
        }

        
        /// <summary>
        /// Player position changed property.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPlayerPosChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            try
            {
                MazeGrid grid = d as MazeGrid;

                string nextPos = e.NewValue as string;

                grid.MovePlayerRectangle(nextPos);
                
                // Check if player reached destination.
                if (nextPos == grid.GoalPos && (grid.Name =="LeftMaze" || grid.Name == "MazeBoard"))
                {
                    MessageBox.Show("You won.", "Victory", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }

                // Check if opponent reached destination.
                if (nextPos == grid.GoalPos && grid.Name == "RightMaze")
                {
                    MessageBox.Show("You lost.", "Defeat", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (NullReferenceException)
            {
            }
        }

        /// <summary>
        /// Row coordinate getter.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns></returns>
        public static int RowCoordinate(string position)
        {
            return int.Parse(position.Split(',')[0].Replace("(", ""));
        }

        /// <summary>
        /// Column coordinate getter.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns></returns>
        public static int ColumnCoordinate(string position)
        {
            return int.Parse(position.Split(',')[1].Replace(")", ""));
        }

        /// <summary>
        /// Finds next position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static string NextPosition(string position, char direction)
        {
            int row = RowCoordinate(position), col = ColumnCoordinate(position);

            switch (direction)
            {
                case '0':
                    // left
                    col--;
                    break;

                case '1':
                    // rigth
                    col++;
                    break;

                case '2':
                    // up
                    row--;
                    break;

                case '3':
                    // down
                    row++;
                    break;

                default:
                    return "ERROR";
            }

            string final = "(" + row + "," + col + ")";

            return final;
        }

        /// <summary>
        /// Creates player movement.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string MovePlayer(string position, string direction)
        {
            int rowPos = RowCoordinate(position);
            int colPos = ColumnCoordinate(position);

            switch (direction)
            {
                case "Up":
                    --rowPos;
                    break;
                case "Down":
                    ++rowPos;
                    break;
                case "Right":
                    ++colPos;
                    break;
                case "Left":
                    --colPos;
                    break;
            }

            string final = "(" + rowPos + "," + colPos + ")";

            return final;
        }

        /// <summary>
        /// Checks if the move is legal.
        /// </summary>
        /// <param name="rowPos"></param>
        /// <param name="colPos"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsLegalMove(int rowPos, int colPos, string direction)
        {
            switch (direction)
            {
                case "Up":
                    if ((rowPos - 1) >= 0 &&
                        Maze[(rowPos - 1) * Cols + colPos] == '0')
                    {
                        return true;
                    }
                    break;

                case "Down":
                    if ((rowPos + 1) < Rows &&
                        Maze[(rowPos + 1) * Cols + colPos] == '0')
                    {
                        return true;
                    }
                    break;

                case "Right":
                    if ((colPos + 1) < Cols &&
                        Maze[(rowPos * Cols) + colPos + 1] == '0')
                    {
                        return true;
                    }
                    break;

                case "Left":
                    if ((colPos - 1) >= 0 &&
                        Maze[(rowPos * Cols) + colPos - 1] == '0')
                    {
                        return true;
                    }
                    break;

                default:
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Maze property.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public string Maze
        {
            get { return (string) GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        /// <summary>
        /// Moves the player rectangle.
        /// </summary>
        /// <param name="newPosition">The new position.</param>
        public void MovePlayerRectangle(string newPosition)
        {
            Canvas.SetLeft(playerCell,
                ColumnCoordinate(newPosition) * playerCell.Width);
            Canvas.SetTop(playerCell,
                RowCoordinate(newPosition) * playerCell.Height);
        }

        /// <summary>
        ///Maze dependency property.
        /// </summary>
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(string),
                typeof(MazeGrid), null);

        /// <summary>
        ///Rows property.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows
        {
            get { return (int) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Rows dependency property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeGrid),
                null);

        /// <summary>
        /// Columns property.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols
        {
            get { return (int) GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        /// <summary>
        /// Columns dependency property.
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeGrid),
                null);


        /// <summary>
        /// Initial position property.
        /// </summary>
        /// <value>
        /// The initial position.
        /// </value>
        public string InitialPos
        {
            get { return (string) GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        /// <summary>
        /// Initial position dependency property.
        /// </summary>
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(string),
                typeof(MazeGrid), null);


        /// <summary>
        /// Destination position.
        /// </summary>
        /// <value>
        /// The goal position.
        /// </value>
        public string GoalPos
        {
            get { return (string) GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        /// <summary>
        /// Destination dependency position.
        /// </summary>
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(string),
                typeof(MazeGrid), null);

        /// <summary>
        /// Player position property.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public string PlayerPos
        {
            get { return (string) GetValue(PlayerPosProperty); }
            set { SetValue(PlayerPosProperty, value); }
        }

        /// <summary>
        /// Player position dependency property.
        /// </summary>
        public static readonly DependencyProperty PlayerPosProperty =
            DependencyProperty.Register("PlayerPos", typeof(string),
                typeof(MazeGrid),
                new PropertyMetadata(OnPlayerPosChanged));

        /// <summary>
        /// Handle key pressed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //Check if the move is legal.
            if (IsLegalMove(RowCoordinate(PlayerPos),
                ColumnCoordinate(PlayerPos), e.Key.ToString()))
            {
                PlayerPos = MovePlayer(PlayerPos, e.Key.ToString());
            }
        }

        private void UserControl_LostKeyboardFocus(object sender,
            KeyboardFocusChangedEventArgs e)
        {
            Focus();
        }
    }
}