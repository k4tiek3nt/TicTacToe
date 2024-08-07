using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading;
using System.Threading.Channels;

namespace TicTacToe
{
    class Program
    {

        /// <summary>
        /// Variables for game
        /// </summary>
        static char[] spaces = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;
        static int flag;

        /// <summary>
        /// Draws the game board
        /// </summary>

        static void DrawBoard()
        {
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", spaces[0], spaces[1], spaces[2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", spaces[3], spaces[4], spaces[5]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2}  ", spaces[6], spaces[7], spaces[8]);
            Console.WriteLine("     |     |     ");
        }

        /// <summary>
        /// Checks if game was won, tied, or should continue
        /// </summary>

        static int CheckWin()
        {
            if (spaces[0] == spaces[1] &&
                spaces[1] == spaces[2] ||  //row 1
                spaces[3] == spaces[4] &&
                spaces[4] == spaces[5] ||  //row 2
                spaces[6] == spaces[7] &&
                spaces[7] == spaces[8] ||  //row 3
                spaces[0] == spaces[3] &&
                spaces[3] == spaces[6] ||  //column 1
                spaces[1] == spaces[4] &&
                spaces[4] == spaces[7] ||  //column 2
                spaces[2] == spaces[5] &&
                spaces[5] == spaces[8] ||  //column 3
                spaces[0] == spaces[4] &&
                spaces[4] == spaces[8] ||  //diagonal 1
                spaces[2] == spaces[4] &&
                spaces[4] == spaces[6])    //diagonal 2
            {
                return 1;
            }
            else if (spaces[0] != '1' &&
                    spaces[1] != '2' &&
                    spaces[2] != '3' &&
                    spaces[3] != '4' &&
                    spaces[4] != '5' &&
                    spaces[5] != '6' &&
                    spaces[6] != '7' &&
                    spaces[7] != '8' &&
                    spaces[8] != '9')
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Draws an X on the game board
        /// </summary>
        /// <param name="pos"></param>
        static void DrawX(int pos)
        {
            spaces[pos] = 'X';
        }

        /// <summary>
        /// Draws an O on the game board
        /// </summary>
        /// <param name="pos"></param>
        static void DrawO(int pos)
        {
            spaces[pos] = 'O';
        }

        /// <summary>
        /// Resets the variables for game.
        /// </summary>

        static void Reset()
        {
            flag = 0;
            spaces = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            player = 1;
            gameLoop();
        }

        /// <summary>
        /// Resets the game to start again or exits
        /// </summary>
        static void NewGame()
        {
            Console.WriteLine("\n If you want to play another game, type new and press enter.");
            Console.WriteLine("\n If you would like to exit the game type stop and press enter.");

            string? userChoice = Console.ReadLine();
            if (userChoice == "new")
            {
                Reset();
            }
            else
            {
                Console.WriteLine("\n Thanks for playing!");
                Thread.Sleep(1000);
                Console.Clear();
                userChoice = "";
            }
        }

        /// <summary>
        /// How to handle if game is won or tied
        /// </summary>
        static void WonOrTied()
        {
            if (flag == 1)
            {
                Console.WriteLine("Player {0} has won", (player % 2) + 1);

                NewGame();
            }

            if (flag == -1)
            {
                Console.WriteLine("Tie Game");

                NewGame();
            }
        }

        static void MarkSpot(int choice)
        {
            if (spaces[choice] != 'X' && spaces[choice] != 'O')
            {
                if (player % 2 == 0)
                {
                    DrawO(choice);
                }
                else
                {
                    DrawX(choice);
                }
                player++;

                Thread.Sleep(1000);
                Console.Clear();
                DrawBoard();
                flag = CheckWin();
                WonOrTied();
            }
            else
            {
                Console.WriteLine("Sorry the row {0} is already marked with {1} \n", choice + 1, spaces[choice]);
                Console.WriteLine("Please wait while the board is loading again...");
                Thread.Sleep(1000);
            }
        }

        static void RandomMarkSpot(int choice)
        {
            if(choice <= 8)
            {
                if (spaces[choice] != 'X' && spaces[choice] != 'O')
                {
                    System.Diagnostics.Debug.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                    Console.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                    choice = choice - 1;
                    MarkSpot(choice);
                }
                else
                {
                    Random rnd = new Random();
                    int RandomDefault = rnd.Next(1, 9);
                    choice = RandomDefault;

                    System.Diagnostics.Debug.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                    Console.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                    choice = choice - 1;
                    MarkSpot(choice);
                }
            }
            if (choice == 9)
            {
                choice = choice - 1;
                System.Diagnostics.Debug.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                Console.WriteLine("You didn't enter a value, so I used a random value of " + choice + ".");
                choice = choice - 1;
                MarkSpot(choice);
            }
        }

        /// <summary>
        /// Game loop to follow as long as game is not won or tied.
        /// </summary>

        static void gameLoop()
        {
            while (flag != 1 && flag != -1)
            {
                Console.Clear();
                Console.WriteLine("Let's Play Tic Tac Toe! Try to get 3 in a row or diagonal... Good Luck!");
                Console.WriteLine("Player 1 : will play X");
                Console.WriteLine("\nPlayer 2 : will play O" + "\n");

                if (player % 2 == 0)
                {
                    Console.WriteLine("It's Player 2's turn: ");
                }
                else
                {
                    Console.WriteLine("It's Player 1's turn: ");
                }

                Console.WriteLine("\n");
                DrawBoard();

                Console.WriteLine("\nPlease choose a spot on the board by entering it's number and pressing enter.");

                //Accept user's input for their turn
                string? userPosition = Console.ReadLine();

                // Assign default value if userPosition is null or empty
                if (!int.TryParse(userPosition, out int RandomDefault))
                {
                    //Variable to take random turn if user does not enter a number
                    Random rnd = new Random();
                    RandomDefault = rnd.Next(1, 9);
                    RandomMarkSpot(RandomDefault);                    
                }

                // Try to convert the result string to an integer
                if (int.TryParse(userPosition, out int result))
                {

                    if (result != 0)
                    {
                        System.Diagnostics.Debug.WriteLine("You entered " + result + " for your turn.");
                        Console.WriteLine("You entered " + result + " for your turn.");
                        Thread.Sleep(0500);

                        choice = result - 1;
                        MarkSpot(choice);
                    }

                }
                /* else
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid input. Please try again and enter a valid number for your turn.");
                        Console.WriteLine("Invalid input. Please try again and enter a valid number for your turn.");
                    } */

            }
        }
    

        /// <summary>
        /// The main game loop
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            gameLoop();
        }
        
    }
}
