using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Matching_Pair_Game
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        MatchingPairGame currentGame;

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            if (currentGame == null)
            {
                Button[] buttons = new Button[20];
                for (int i = 0; i < 20; i++)
                {
                    buttons[i] = wrapPanel.Children[i] as Button;
                }
                currentGame = new MatchingPairGame(buttons);
            }
            currentGame.Reset();
        }

        public class MatchingPairGame
        {
            private Button[] buttons;
            List<string> icons = new List<string>();

            public MatchingPairGame(Button[] buttons)
            {
                this.buttons = buttons;

                for (int i = 0; i < 20; i++)
                {
                    buttons[i].Click += new RoutedEventHandler(OnMove);
                    buttons[i].Content = "?";
                    buttons[i].Tag = i;
                }
            }

            //initiate the buttons for matching pairs
            public void Reset()
            {
                for (int i = 0; i < 20; i++)
                {
                    buttons[i].Content = "?";
                }

                string[] symbol = { "!1", "@1", "#1", "$1", "%1", "^1", "&1", "*1", "(1", ")1", "!2", "@2", "#2", "$2", "%2", "^2", "&2", "*2", "(2", ")2" };
                for (int i = 0; i < 20; i++)
                {
                    Random rnd = new Random();
                    do
                    {
                        int temp = rnd.Next(0, 20);
                        //Each icon appears twice in this list
                        if (icons.Contains(symbol[temp]) == false)
                        {
                            icons.Add(symbol[temp]);
                        }
                    } while (icons.Count < 20);
                }
            }

            private int cnt = 0; // for comparing two buttons
            private int wincnt = 0; // to count the number of matched buttons
            private Button firstClick = null; // store the first button openly temporarily
            private Button secondClick = null; // store the second button openly temporaily
            public void OnMove(object sender, RoutedEventArgs e)
            {
                Button button = sender as Button;

                if (button.Content == null || button.Content.ToString() == "?")
                {
                    button.Content = icons[(int)button.Tag][0];

                    if (button.Content != null || button.Content.ToString() != "?")
                    {

                        if (cnt % 2 == 0)
                        {
                            firstClick = button;
                            // MessageBox.Show("1: " + firstClick.Content.ToString());
                        }
                        else
                        {
                            secondClick = button;
                            //MessageBox.Show("2: " + secondClick.Content.ToString());
                            checkwin(firstClick, secondClick);
                            firstClick = null;
                            secondClick = null;
                        }
                        cnt++;
                    }
                }
            }

            //Check if the buttons are matched
            public void checkwin(Button first, Button second)
            {
                if (first.Content.ToString()[0] == second.Content.ToString()[0] && first.Content.ToString() != "?")
                {
                    //  MessageBox.Show(first.Content.ToString());
                    //  MessageBox.Show(second.Content.ToString());
                    wincnt++;
                    //   MessageBox.Show("Matched");
                }
                else
                {
                    MessageBox.Show("Wrong");
                    first.Content = "?";
                    second.Content = "?";

                }
                if (wincnt == 10)
                {
                    MessageBox.Show("You win!");
                    wincnt = 0;
                }
            }
        }
    }
}
