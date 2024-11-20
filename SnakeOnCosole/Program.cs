
internal class Program
{
    private static void Main(string[] args)
    {
        //default console size
        int width = 120;
        int height = 30;

        //saddly this is not working in windowns 11, in other verions the game can be resized
        System.Console.SetWindowSize(width, height);


        int[,] game = new int[width, height];

        int dir = 0;
        int snake_length = 1;
        int head_x = 0;
        int head_y = 0;

        NewGame();

        while (true)
        {


            if (Console.KeyAvailable)
            {
                CheckInput(Console.ReadKey(true));
                RenderGame();
            }
            else
            {
                RenderGame();

            }
            Thread.Sleep(300 / snake_length);
            Console.Clear();
        }

        void RenderGame()
        {
            MoveSnake();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    if (game[x, y] == -3)
                    {
                        //rendering the border
                        Console.Write("-");
                    }
                    else if (game[x, y] == 0)
                    {
                        //rendering whitespaces
                        Console.Write(' ');
                    }
                    else if (game[x, y] == -1)
                    {
                        //rendering the food
                        Console.Write("0");
                    }
                    else
                    {
                        //rendering the snake
                        game[x, y] -= 1;
                        Console.Write("@");
                    }
                }
            }
        }
        void CheckInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.RightArrow)
            {
                dir = 0;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                dir = 1;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                dir = 2;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                dir = 3;
            }
            else if (key.Key == ConsoleKey.Q)
            {
                Environment.Exit(0);
            }
            else if (key.Key == ConsoleKey.R)
            {
                NewGame();
            }
        }

        void MoveSnake()
        {
            if (dir == 0)
            {
                head_x += 1;
            }
            else if (dir == 1)
            {
                head_y += 1;
            }
            else if (dir == 2)
            {
                head_x -= 1;
            }
            else if (dir == 3)
            {
                head_y -= 1;
            }

            if (game[head_x, head_y] == -3 || game[head_x, head_y] > 0)
            {
                Console.Clear();
                Console.WriteLine("game over");
                Environment.Exit(0);
            }
            else if (game[head_x, head_y] == -1)
            {
                snake_length++;
                SpawnFood();
            }

            game[head_x, head_y] = snake_length;
        }

        void NewGame()
        {

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //set the borders
                    if (y == 0 || y == height-1 || x == 0 || x == width-1)
                    {
                        game[x, y] = -3;
                    }
                    else
                    {
                        game[x, y] = 0;
                    }
                }
            }

            Random rnd = new Random();
            head_x = rnd.Next(1, width - 2);
            head_y = rnd.Next(1, height - 2);
            game[head_x, head_y] = snake_length;
            SpawnFood();
        }
        void SpawnFood()
        {
            Random rnd = new Random();
            int fx = rnd.Next(0, width - 1);
            int fy = rnd.Next(0, height - 1);
            if (game[fx, fy] != 0)
            {
                SpawnFood();
            }
            else
            {
                //set the food
                game[fx, fy] = -1;
            }
        }
    }
}