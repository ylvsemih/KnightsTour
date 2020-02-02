using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;


namespace KnightsTour
{

    class Program
    {
        public int N,NN, Xin, Yin, counter;
        public int[,] board;
        public int[] cx;
        public int[] cy;
        public bool check, hideTrace, correct,dontshowChoice;
        public int nChoice, xChoice, yChoice;
        public char aChar;
        public bool menu;

        public void initialize()
        {
            int i, j;

            cx = new int[9];
            cy = new int[9];
            board = new int[N + 1, N + 1];

            cy[1] = 2;   cx[1] =  1;
            cy[2] = 1;   cx[2] =  2;
            cy[3] = -1;  cx[3] =  2;
            cy[4] = -2;  cx[4] =  1;
            cy[5] = -2;  cx[5] = -1;
            cy[6] = -1;  cx[6] = -2;
            cy[7] = 1;   cx[7] = -2;
            cy[8] = 2;   cx[8] = -1;

            for (i = 1; i < N + 1; i++)
            {
                for (j = 1; j < N + 1; j++)
                {
                    board[i, j] = 0;
                }
            }

            
        }

        public void sData()
        {
            Console.WriteLine("\n\nPART 1-> Data\n\t" +
                "1) Board {0}x{1}.\n\t" +
                "2) Inizial Position X={2}, Y={3}. L=1.\n",N,N,Yin,Xin);

            
        }
        public string addMinus(int length)
        {
            string allMinuses = "";
            for(int i = 0; i < length; i++)
            {
                //Console.Write("-");
                allMinuses = allMinuses + "-";
                
            }
            return allMinuses;

        }
        public void run (int l, int x, int y, StreamWriter file)
        {
            

            int k, u, v;
            k = 0;
            do
            {
                file.WriteLine("");
                counter++;
                k++;


                u = x + cx[k];
                v = y + cy[k];
                if (!hideTrace)
                {
                    file.Write("\n{0,8})", counter);
                    file.Write(addMinus(l-1));
                    file.Write("R{0}. U={1}, V={2}. L={3}. ", k, u, v, l);
                    
                }
                
                if( u >= 1 && u <=N && v >=1 && v <= N)
                {
                    if(board[v,u] == 0)
                    {
                        board[v, u] = l;
                        if (!hideTrace)  file.Write("Free. BOARD[{0},{1}] := {2}.",u,v,l);
                        if (l < NN)
                        {
                            run(l + 1, u, v,file);
                            if (!check)
                            {
                                board[v, u] = 0;
                                if (!hideTrace)  file.Write(" Backtrack.");
                            }
                        }
                        else check = true;
                    }
                    else
                    {
                        if (!hideTrace)  file.Write("Thread.");
                    }
                }
                else
                {
                    if (!hideTrace)  file.Write("Out.");
                }


            } while (k < 8 && !check);

       }
        
        public void menuDisplay()
        {
            if (menu)
            {
                Console.WriteLine("Knight's Tour.");
                Console.Write("Insert N number according to Board Size: (3 to 8): ");

                correct = false;
                do
                {
                    if (int.TryParse(Console.ReadLine(), out nChoice))
                    {
                        if (nChoice < 3 || nChoice > 8)
                        {
                            Console.Write("Number out of range! (3 to 8): ");
                        }
                        else
                        {
                            correct = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You didn't insert a Number! (3 to 8)");
                    }
                } while (!correct);

                Console.Write("X: (Insert an integer between 1 and {0}): ", nChoice);
                correct = false;
                do
                {
                    if (int.TryParse(Console.ReadLine(), out xChoice))
                    {
                        if (xChoice < 1 || xChoice > nChoice)
                        {
                            Console.Write("Number out of range!\n X: (Insert an integer between 1 and {0}): ", nChoice);
                        }
                        else
                        {
                            correct = true;
                        }
                    }
                    else
                    {
                        Console.Write("You didn't insert a Number!\n X: (Insert an integer between 1 and {0}): ", nChoice);
                    }
                } while (!correct);

                Console.Write("Y: (Insert an integer between 1 and {0}): ", nChoice);
                correct = false;
                do
                {
                    if (int.TryParse(Console.ReadLine(), out yChoice))
                    {
                        if (yChoice < 1 || yChoice > nChoice)
                        {
                            Console.Write("Number out of range!\n Y:  (Insert an integer between 1 and {0}): ", nChoice);
                        }
                        else
                        {
                            correct = true;
                        }
                    }
                    else
                    {
                        Console.Write("You didn't insert a Number!\n Y: (Insert an integer between 1 and {0}): ", nChoice);
                    }
                } while (!correct);
                
                Console.Write("Go Trace Section: (y/n): ");
                correct = false;
                do
                {
                    if (char.TryParse(Console.ReadLine(), out aChar))
                    {
                        switch (aChar)
                        {
                            case 'y': dontshowChoice = false; correct = true; break;
                            case 'n': dontshowChoice = true; correct = true; break;
                            default: Console.WriteLine("Wrong Input Entry!\n Go to Trace : (y/n)"); break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You didn't insert a character!\n Go to Trace : (y/n)");
                    }
                } while (!correct);

            }
            else
            {
                nChoice = 5;
                xChoice = 1;
                yChoice = 1;
                dontshowChoice = false;

            }
        }
        
       public void TourPath(StreamWriter file)
        {
            menu = true;
            menuDisplay();
            N = nChoice;
            NN = N * N;
            hideTrace = dontshowChoice;
            initialize();
            check = false;
            Xin = yChoice;
            Yin = xChoice;
            board[Yin, Xin] = 1;
            sData();
            Console.WriteLine("PART 2-> Trace");
            counter = 0;
            run(2, Xin, Yin,file);
            if (hideTrace)
            {
                Console.WriteLine("\tDon't show trace option enabled. \n\tThe final value of counter steps: {0}",counter);
            }
            Results();

        }

        public void Results()
        {
            Console.Write("\n\nPART 3-> Results\n\t" +
                               "1) ");
            if (check)
            {
                Console.Write("Path is found.\n\t" +
                              "2) Path graphically\n\n");
                Graph();
            }
            else
            {
                Console.WriteLine("Path does not exist.\n\n\n");
            }
        }
        public void Graph()
        {
            Console.WriteLine("\tY, V ^");
            for (int i = N; i >= 1; i--)
            {
                Console.Write("\t   {0} | ", i);
                for (int j = 1; j < N + 1; j++)
                {
                    Console.Write(board[j, i] + "\t");
                }
                Console.Write("\n");
            }
            Console.Write("\t      ");
            for (int i = 0; i < N; i++)
            {
                Console.Write("--------");
            }
            Console.Write("> X, U\n\t\t");
            for (int i = 1; i < N + 1; i++)
            {
                Console.Write(i + "\t");
            }
            Console.Write("\n\n\n");
        }
    
}
    

    class MainClass
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            using (StreamWriter file = new StreamWriter(@"C:\Users\ASUS\Desktop\KnightsTour\KnightsTour\obj\Debug\out.txt"))
                p.TourPath(file);
            
            Console.WriteLine("Program Succesfully Executed! Press Any Button..");
            Console.ReadKey();
        }
    }
}
