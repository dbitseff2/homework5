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

namespace homework5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string lastPlay;

        public MainWindow()
        {
            InitializeComponent();
            uxTurn.Text = "X's turn.";
            lastPlay = null;
        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            List<Button> buttons = GetButtons();
            foreach(Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
            uxTurn.Text = "X's turn.";
            lastPlay = null;
        }

        private void uxExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {                               
            if ((sender as Button).Content == null)
                (sender as Button).Content = lastPlay = string.IsNullOrEmpty(lastPlay) || lastPlay.Equals("o",StringComparison.InvariantCultureIgnoreCase) ? "X" : "O";

            uxTurn.Text = lastPlay.Equals("X",StringComparison.InvariantCultureIgnoreCase) ? "O's turn." : "X's turn.";

            CheckWinner();
        }

        private void CheckWinner()
        {
            List<Button> buttons = GetButtons();            

            int[] xColumnCount = new int[3];
            int[] oColumnCount = new int[3];
            int[] xRowCount = new int[3];
            int[] oRowCount = new int[3];
            int[] xDiagonalCount = new int[2];
            int[] oDiagonalCount = new int[2];                      

            foreach (Button button in buttons)
            {
                if (button.Content != null)
                {
                    int row = Convert.ToInt16(button.Tag.ToString().Substring(0, 1));
                    int column = Convert.ToInt16(button.Tag.ToString().Substring(2, 1));
                    
                    if (button.Content.ToString().Equals("X", StringComparison.InvariantCultureIgnoreCase))
                    {
                        xColumnCount[column]++;
                        xRowCount[row]++;
                        if (row == column)
                        {
                            xDiagonalCount[0]++;
                            if (row == 1 && column == 1) xDiagonalCount[1]++;
                        }
                        if((row == 0 && column == 2) || row == 2 && column == 0)
                        {
                            xDiagonalCount[1]++;
                        }

                    }
                    else if (button.Content.ToString().Equals("O", StringComparison.InvariantCultureIgnoreCase))
                    {
                        oColumnCount[column]++;
                        oRowCount[row]++;
                        if (row == column)
                        {
                            oDiagonalCount[0]++;
                            if (row == 1 && column == 1) oDiagonalCount[1]++;
                        }
                        if ((row == 0 && column == 2) || row == 2 && column == 0)
                        {
                            oDiagonalCount[1]++;
                        }
                    }                    

                    for(int i=0; i < 3; i++)
                    {
                        if(xColumnCount[i] == 3 || xRowCount[i] == 3 || (i<2 && xDiagonalCount[i] == 3))
                        {
                            HandleWinner("X");
                        }
                        if (oColumnCount[i] == 3 || oRowCount[i] == 3 || (i < 2 && oDiagonalCount[i] == 3))
                        {
                            HandleWinner("O");
                        }
                    }                    
                }
            }
        }

        private void HandleWinner(string winner)
        {
            List<Button> buttons = GetButtons();
            uxTurn.Text = winner + " Wins!";
            foreach(Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }

        private List<Button> GetButtons()
        {
            UIElementCollection element = uxGrid.Children;
            List<FrameworkElement> elements = element.Cast<FrameworkElement>().ToList();
            List<Button> buttons = elements.OfType<Button>().ToList();
            return buttons;
        }
    }
}
