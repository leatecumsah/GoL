using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;

//ToDo: -check alive Xx
//      -check neighbours --->next XX
//      -set status XX
//      -check rules xx
//      -Game Over Screen XXXXXXX --> immer noch clear game over trotz flag


//Regeln
//Für lebende Zellen:

//    Wenn weniger als 2 Nachbarn → Zelle stirbt (Regel 1).
//    Wenn 2 oder 3 Nachbarn → Zelle bleibt lebendig (Regel 2).
//    Wenn mehr als 3 Nachbarn → Zelle stirbt (Regel 3).

//Für tote Zellen:

//    Wenn genau 3 Nachbarn → Zelle wird lebendig (Regel 4).
//    In allen anderen Fällen bleibt die Zelle tot.

namespace Game_of_life
{
  
    public partial class MainWindow : Window
    {

        int generationCounter = 0;


        DispatcherTimer timer = new DispatcherTimer();
        static int total_x = 20;
        static int total_y = 20;

        Rectangle[,] list = new Rectangle[total_y, total_x];
        bool[,] board = new bool[total_y, total_x];

        bool isClearing = false;//Flag für gewolltes leeres speilfeld thanks stack





        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += gameTick;
            initializeBoard();

        }
        private void gameTick(object? sender, EventArgs e) 
        {
            startSimulation();
        }
        private void startSimulation()
        {
        

            bool[,] newboard = new bool[total_y, total_x]; //Temporäres Board---Gibts ne elegantere Lösung?

            for (int y = 0; y < total_y; y++)
            {
                for (int x = 0; x < total_x; x++)
                {
                    int aliveNeighbours = countNeighbours(x, y);

                    if (board[y, x]) // Zelle ist lebendig bei c# muss kein ==true hin, ist automatisch schon da fals wäre if (!board[y, x])
                    {
                        if (aliveNeighbours < 2 || aliveNeighbours > 3)
                        {
                            newboard[y, x] = false; // Zelle stirbt
                        }
                        else
                        {
                            newboard[y, x] = true; // Zelle überlebt
                        }
                    }
                    else // Zelle ist tot
                    {
                        if (aliveNeighbours == 3)
                        {
                            newboard[y, x] = true; // Zelle wird lebendig
                        }
                        else
                        {
                            newboard[y, x] = false; // Zelle bleibt tot
                        }
                    }
                }
            }
            UpdateBoard(newboard);
            generationCounter++;
            textBox_GenerationCounter.Text = "Generation: " + generationCounter.ToString();
           

            if (IsGameOver())
            {
                timer.Stop();
               
                return;
            }



        }

        private void UpdateBoard(bool[,] newBoard) //neue Generation
        {
            for (int y = 0; y < total_y; y++)
            {
                for (int x = 0; x < total_x; x++)
                {
                    board[y, x] = newBoard[y, x];
                    list[y, x].Fill = board[y,x]?Brushes.Plum:Brushes.DarkGray;  
                }
            }

           
        }
        private void initializeBoard()
        {
            for (int y = 0; y < total_y; y++)
            {
                for (int x = 0; x < total_x; x++)
                {
                    Rectangle newRec = new Rectangle
                    {
                        Fill = Brushes.DarkGray, // Zellen sind anfangs "dead"
                        Stroke = Brushes.Black, // Rahmen
                        Height = 20,
                        Width = 20
                    };

                    // Mausklick-Ereignis zu jedem Rechteck hinzufügen
                    newRec.MouseDown += Rectangle_MouseDown;

                    Main_Canvas.Margin = new Thickness(20, 20, 20, 20);
                    Main_Canvas.Children.Add(newRec);
                    Canvas.SetTop(newRec, 20 * y);
                    Canvas.SetLeft(newRec, 20 * x);
                    list[y, x] = newRec;

                    // Initialzustand der Zellen auf "dead" (false) setzen
                    board[y, x] = false;



                }


            }
            
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle clickedRectangle = (Rectangle)sender;

            // Finde die Position der angeklickten Zelle
            int x = (int)(Canvas.GetLeft(clickedRectangle) / total_x);
            int y = (int)(Canvas.GetTop(clickedRectangle) / total_y);

            // Zustandswechsel der Zelle (toggle alive/dead)
            board[y, x] = !board[y, x];

            // Ändere die Farbe der Zelle basierend auf ihrem Zustand
            clickedRectangle.Fill = board[y, x] ? Brushes.Plum : Brushes.DarkGray;  
        }

        private int countNeighbours(int x, int y) 
        {
            int aliveCount = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue; 

                    int neighbourX = x + i;
                    int neighbourY = y + j;

                    if (neighbourX >= 0 && neighbourX < total_x && neighbourY >= 0 && neighbourY < total_y)
                    {
                        if (board[neighbourY, neighbourX]) aliveCount++;
                    }
                }
            }
            return aliveCount;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //start
        {
            timer.Start();

        }


        private void Button_Click(object sender, RoutedEventArgs e) //pause
        {
            timer.Stop();
        }

        private void button_clear_Click(object sender, RoutedEventArgs e) //clear
        {
            isClearing = true;
            initializeBoard();
            generationCounter = 0; // Generationszähler auf0 zurücksetzen
            textBox_GenerationCounter.Text = "Generation: " + generationCounter.ToString();
            isClearing = false;
        }

        private bool IsGameOver() 
        
        {
            if (isClearing)
            {
                // Wenn das Spielfeld manuell cleart, ignoriere das Game-Over
                return false;
            }
            for (int y = 0; y < total_y; y++)
            {
                for (int x = 0; x < total_x; x++)
                {
                
                    { if (board[x, y]) 
                        return false; 
                    }
                 }
            }
            timer.Stop();
            DrawGameOver();
            GameOverWindow gameOverWindow = new GameOverWindow();
            gameOverWindow.ShowDialog();
            
            return  true;
           
        }

        public void DrawGameOver() 
        {
            //G
            ActivateCell(1, 1);
            ActivateCell(1, 2);
            ActivateCell(1, 3);
            ActivateCell(2, 1);
            ActivateCell(3, 1);
            ActivateCell(4, 1);
            ActivateCell(3, 3);
            ActivateCell(4, 3);
            ActivateCell(5, 2);
            ActivateCell(5, 3);

            //A
            ActivateCell(1, 5);
            ActivateCell(2, 5);
            ActivateCell(3, 5);
            ActivateCell(4, 5);
            ActivateCell(5, 5);
            ActivateCell(1, 6);
            ActivateCell(3, 6);
            ActivateCell(1, 7);
            ActivateCell(2, 7);
            ActivateCell(3, 7);
            ActivateCell(4, 7);
            ActivateCell(5, 7);

            //M
            ActivateCell(1, 9);
            ActivateCell(2, 9);
            ActivateCell(3, 9);
            ActivateCell(4, 9);
            ActivateCell(5, 9);
            ActivateCell(2, 10);
            ActivateCell(1, 11);
            ActivateCell(2, 11);
            ActivateCell(3, 11);
            ActivateCell(4, 11);
            ActivateCell(5, 11);

            //E 
            ActivateCell(1, 13); 
            ActivateCell(2, 13);
            ActivateCell(3, 13);
            ActivateCell(4, 13);
            ActivateCell(5, 13);
            ActivateCell(1, 14);
            ActivateCell(3, 14);
            ActivateCell(5, 14);
            ActivateCell(1, 15);
            ActivateCell(5, 15);

            //O
            ActivateCell(7, 4);
            ActivateCell(8, 3);
            ActivateCell(9, 3);
            ActivateCell(10, 3);
            ActivateCell(11, 4);
            ActivateCell(8, 5);
            ActivateCell(9, 5);
            ActivateCell(10, 5 );

            //V
            ActivateCell(7, 7);
            ActivateCell(8, 7);
            ActivateCell(9, 7);
            ActivateCell(10, 7);
            ActivateCell(11, 8);
            ActivateCell(7, 9);
            ActivateCell(8, 9);
            ActivateCell(10, 9);
            ActivateCell(9, 9);

            //E
            ActivateCell(7, 11);
            ActivateCell(8, 11); 
            ActivateCell(9, 11); 
            ActivateCell(10, 11); 
            ActivateCell(11, 11);
            ActivateCell(7, 12);
            ActivateCell(7, 13);
            ActivateCell(9, 12);
            ActivateCell(11, 12); 
            ActivateCell(11, 13);

            //R

            ActivateCell(7, 15);
            ActivateCell(8, 15);
            ActivateCell(9, 15);
            ActivateCell(10, 15);
            ActivateCell(11, 15);
            ActivateCell(7, 16);
            ActivateCell(7, 17);
            ActivateCell(8, 17);
            ActivateCell(8, 16);
            ActivateCell(9, 17);
            ActivateCell(10, 16);
            ActivateCell(11, 17);

        }

        private void ActivateCell(int y, int x)
        {
            board[y, x] = true;
            list[y, x].Fill = Brushes.Red;
        }

        private void OnGliderClick(object sender, MouseButtonEventArgs e)
        {
            // Glider Pattern 
            board[2, 1] = true;
            board[3, 2] = true;
            board[3, 3] = true;
            board[2, 3] = true;
            board[1, 3] = true;
            UpdateBoard(board); 
        }

        private void OnBlinkerClick(object sender, MouseButtonEventArgs e)
        {
            // Blinker Pattern
            board[2, 2] = true;
            board[2, 3] = true;
            board[2, 4] = true;
            UpdateBoard(board);
        }

        private void OnPulsarClick(object sender, MouseButtonEventArgs e) ///needs work still pattern not right
        {
            // Pulsar Pattern: 13x13 Grid 

            int centerX = total_x/2; 
            int centerY = total_y/2;



            board[centerY - 6, centerX - 1] = true;
            board[centerY - 6, centerX] = true;
            board[centerY - 6, centerX + 1] = true;

            board[centerY - 4, centerX - 2] = true;
            board[centerY - 4, centerX + 2] = true;

            board[centerY - 3, centerX - 4] = true;
            board[centerY - 3, centerX - 3] = true;
            board[centerY - 3, centerX + 3] = true;
            board[centerY - 3, centerX + 4] = true;

            board[centerY - 1, centerX - 6] = true;
            board[centerY - 1, centerX - 1] = true;
            board[centerY - 1, centerX + 1] = true;
            board[centerY - 1, centerX + 6] = true;

            board[centerY, centerX - 6] = true;
            board[centerY, centerX - 1] = true;
            board[centerY, centerX + 1] = true;
            board[centerY, centerX + 6] = true;

            board[centerY + 1, centerX - 6] = true;
            board[centerY + 1, centerX - 1] = true;
            board[centerY + 1, centerX + 1] = true;
            board[centerY + 1, centerX + 6] = true;

            board[centerY + 3, centerX - 4] = true;
            board[centerY + 3, centerX - 3] = true;
            board[centerY + 3, centerX + 3] = true;
            board[centerY + 3, centerX + 4] = true;

            board[centerY + 4, centerX - 2] = true;
            board[centerY + 4, centerX + 2] = true;

            board[centerY + 6, centerX - 1] = true;
            board[centerY + 6, centerX] = true;
            board[centerY + 6, centerX + 1] = true;


            UpdateBoard(board);
        }


    }
}