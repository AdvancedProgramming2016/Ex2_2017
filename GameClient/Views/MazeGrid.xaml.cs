﻿using System;
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

        private double rectHeight;
        private double rectWidth;
        private int numOfRows;
        private int numOfCols;
        private int playerPosI;
        private int playerPosJ;
        private Rectangle playerRect;
        private string mazeString;
        private Dictionary<string, Rectangle> dicRect;
        private SolidColorBrush wallColor;
        private SolidColorBrush freeSpaceColor;
        private SolidColorBrush destinationColor;
        private MultiPlayerGameViewModel multiPlayerGameVM;

        public MultiPlayerGameViewModel MultiPlayerGameVM
        {
            get
            {
                return this.multiPlayerGameVM;
            }
            set
            {
                // It initialized here.
                this.multiPlayerGameVM = value;
            }
        }

        public String Maze
        {
            get
            {
                return (string)GetValue(MazeProperty);
            }
            set
            {
                SetValue(MazeProperty, value);
            }
        }

        public String Solution
        {
            get
            {
                //this.RunAnimation(value);
                return (string)GetValue(SolutionProperty);
            }
            set
            {
                SetValue(SolutionProperty, value);
            }
        }

        public String InitialPosition
        {
            get
            {
                return (string)GetValue(InitialPositionProperty);
            }
            set
            {
                SetValue(InitialPositionProperty, value);
            }
        }

        public String DestPosition
        {
            get
            {
                return (string)GetValue(DestPositionProperty);
            }
            set
            {
                SetValue(DestPositionProperty, value);
            }
        }

        public String PlayerPosition
        {
            get
            {
                return (string)GetValue(PlayerPositionProperty);
            }
            set
            {
                SetValue(PlayerPositionProperty, value);
            }
        }

        public String Rows
        {
            get
            {
                return (String)GetValue(RowsProperty);
            }
            set
            {
                SetValue(RowsProperty, value);
            }
        }

        public String Cols
        {
            get
            {
                return (String)GetValue(ColsProperty);
            }
            set
            {
                SetValue(ColsProperty, value);
            }
        }

        public String MazeName
        {
            get
            {
                return (string)GetValue(MazeNameProperty);
            }
            set
            {
                SetValue(MazeNameProperty, value);
            }
        }

        public void RunAnimation(string solution, string initPosition)
        {

            int currPlayerXPosition, currPlayerYPosition;
            Dispatcher.Invoke( () =>
            {
                // Reverse the solution.
                char[] reverseSolution = solution.ToCharArray();
                Array.Reverse(reverseSolution);

                // Get player initial position.
                string position = initPosition;
                position = position.Trim(new Char[] { '(', ')' });

                currPlayerXPosition =
                    Convert.ToInt32(position.Split(',')[0]);
                currPlayerYPosition =
                    Convert.ToInt32(position.Split(',')[1]);

                foreach (char movement in reverseSolution)
                {
                    string temp = "";
                    //string newPosition;

                    // Move the player to the next location.
                    switch (movement)
                    {
                        case '0': //Move right
                            temp = (++currPlayerXPosition).ToString() + ',' +
                                                currPlayerYPosition.ToString();
                            //newPosition = "(" + temp + ")";
                            //PlayerPosition = newPosition;

                            break;
                        case '1': //Move left
                            temp =
                               (--currPlayerXPosition).ToString() + ',' +
                               currPlayerYPosition.ToString();
                            //newPosition = "(" + temp + ")";
                            //PlayerPosition = newPosition;
                            break;
                        case '2': //Move up
                            temp =
                                currPlayerXPosition.ToString() + ',' +
                                (--currPlayerYPosition).ToString();
                            //newPosition = "(" + temp + ")";
                            //PlayerPosition = newPosition;
                            break;
                        case '3': //Move down
                            temp =
                                currPlayerXPosition.ToString() + ',' +
                                (++currPlayerYPosition).ToString();
                            //newPosition = "(" + temp + ")";
                            //PlayerPosition = newPosition;
                            break;
                    }

                    this.HandlePlayerPosChanged(temp);

                    // Sleep for 500 ms.
                    Thread.Sleep(500);
                }
            });
            // Update new location of player.
            //PlayerPosition = DestPosition;
        }

        protected override void OnInitialized(EventArgs e)
        {

            this.dicRect = new Dictionary<string, Rectangle>();
            this.playerRect = new Rectangle();
            this.wallColor = new SolidColorBrush(Colors.Black);
            this.freeSpaceColor = new SolidColorBrush(Colors.White);
            this.destinationColor = new SolidColorBrush(Colors.DarkCyan);

            InitializeComponent();

            base.OnInitialized(e);
        }

        public void Draw(string drawingString)
        {

            // Calculate number of rows and coloms in maze.
            string[] splitRows = drawingString.Split(',');
            this.numOfRows = splitRows.Length;
            this.numOfCols = splitRows[0].Length;

            // Calculate height and width of each rectangle.
            this.rectHeight = this.MazeCanvas.Height / this.numOfRows;
            this.rectWidth = this.MazeCanvas.Width / this.numOfCols;

            // Create the maze and add to the dictionary.
            for (int i = 0; i < splitRows.Count(); i++)
            {
                for (int j = 0; j < splitRows[0].Length; j++)
                {
                    Rectangle currRect = new Rectangle();
                    currRect.Height = this.rectHeight;
                    currRect.Width = this.rectWidth;
                    
                    char currChar = splitRows[i].ElementAt(j);

                    switch (currChar)
                    {
                        case '1':
                            currRect.Fill = this.wallColor;
                            break;
                        case '0':
                            currRect.Fill = this.freeSpaceColor;
                            break;
                        case '*': // Start point.
                            currRect.Fill = this.freeSpaceColor;
                            this.playerPosI = i;
                            this.playerPosJ = j;
                            break;
                        case '#': // Destination point.
                            DestPosition = i.ToString() + ',' + j.ToString();
                            currRect.Fill = this.destinationColor;
                            break;
                    }

                    // Color the rectangle and add to the canvas.
                    this.MazeCanvas.Children.Add(currRect);
                    Canvas.SetTop(currRect, i * this.rectHeight);
                    Canvas.SetLeft(currRect, j * this.rectWidth);
                    
                    // Add to dictionary rectangle.
                    this.dicRect.Add(i.ToString() + "," + j.ToString(), currRect);

                }
            }
            this.HandlePlayerPosChanged(this.playerPosI.ToString() + ',' + this.playerPosJ.ToString());
        }

        public static readonly DependencyProperty MovedPlayerProperty =
            DependencyProperty.Register("MovedPlayer", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty SolutionProperty =
            DependencyProperty.Register("Solution", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(String), typeof(MazeGrid), new UIPropertyMetadata(MazeStringInit));

        public static readonly DependencyProperty PlayerPositionProperty =
            DependencyProperty.Register("PlayerPosition", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty DestPositionProperty =
            DependencyProperty.Register("DestPosition", typeof(String), typeof(MazeGrid), null);

        public static readonly DependencyProperty InitialPositionProperty =
            DependencyProperty.Register("InitialPosition", typeof(String), typeof(MazeGrid), null);

        public static void MazeStringInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeGrid board = d as MazeGrid;
            board.Draw(e.NewValue.ToString());
        }

        public static void PlayerPosInit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MazeGrid board = d as MazeGrid;
            board.HandlePlayerPosChanged(e.NewValue);
        }

        /// <summary>
        ///  Update the location of the player.
        /// </summary>
        /// <param name="newPosition"></param>
        public void HandlePlayerPosChanged(object newPosition)
        {

            string position = newPosition as string;

            this.playerRect.Fill = new SolidColorBrush(Colors.Red);
            this.playerRect.Width = this.rectWidth;
            this.playerRect.Height = this.rectHeight;

            // Update the location of the player.
            if(!this.MazeCanvas.Children.Contains(this.playerRect))
            {
                this.MazeCanvas.Children.Add(this.playerRect);
            }
            
            Canvas.SetTop(this.playerRect, Convert.ToDouble(position.Split(',')[0]) * this.rectHeight);
            Canvas.SetLeft(this.playerRect, Convert.ToDouble(position.Split(',')[1]) * this.rectWidth);

            // If the player reached the Destination position.
            if (DestPosition != null && position.Split(',')[0].Equals(DestPosition.Split(',')[0]) &&
                position.Split(',')[1].Equals(DestPosition.Split(',')[1]))
            {
                MessageBox.Show("Finished!");
            }

            PlayerPosition = '(' + position + ')';
        }


        private void UserControl_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Focus();
        }

        /// <summary>
        /// Calculate the location of the player on the maze.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            while(MultiPlayerGameVM == null)
            {
                continue;
            }
            switch(e.Key.ToString())
            {
                case "Up":
                    if (this.playerPosI - 1 >= 0 &&
                    !(this.dicRect[(this.playerPosI - 1).ToString() + ',' + this.playerPosJ.ToString()]
                    .Fill.Equals(this.wallColor)))
                    {
                        this.playerPosI -= 1;
                    }
                    MultiPlayerGameVM.MovePlayer("Up");
                    break;

                case "Down":
                    if (this.playerPosI + 1 < this.numOfRows &&
                        !(this.dicRect[(this.playerPosI + 1).ToString() + ',' + this.playerPosJ.ToString()]
                    .Fill.Equals(this.wallColor)))
                    {
                        this.playerPosI += 1;
                    }
                    MultiPlayerGameVM.MovePlayer("Down");
                    break;

                case "Right":
                    if (this.playerPosJ + 1 < this.numOfCols && 
                        !(this.dicRect[this.playerPosI.ToString() + ',' + (this.playerPosJ + 1).ToString()]
                    .Fill.Equals(this.wallColor)))
                    {
                        this.playerPosJ += 1;
                    }
                    MultiPlayerGameVM.MovePlayer("Right");
                    break;

                case "Left":
                    if (this.playerPosJ - 1 >= 0 && 
                        !(this.dicRect[this.playerPosI.ToString() + ',' + (this.playerPosJ - 1).ToString()]
                    .Fill.Equals(this.wallColor)))
                    {
                        this.playerPosJ -= 1;
                    }
                    MultiPlayerGameVM.MovePlayer("Left");
                    break;

            }

            // Update the location of the player.
            this.HandlePlayerPosChanged(this.playerPosI.ToString() + ',' + this.playerPosJ.ToString());

        }
    }
}
