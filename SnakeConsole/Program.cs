using System;

class Program
{
    static int foot_x;
    static int foot_y;

    static int[] body_x = new int[100];
    static int[] body_y = new int[100];

    static Random rnd = new Random();
    static void SpawnFood() {
        foot_x = rnd.Next(2, 120) - body_x[body_x.Length-1];
        if (foot_x % 2 != 0) foot_x += 1;

        foot_y = rnd.Next(0, 40) - body_y[body_y.Length-1];
    }

    static void Main(string[] args)
    {
        Console.SetWindowSize(120, 40);
        Console.SetBufferSize(120, 40);
        Console.CursorVisible = false;

        int head_x = 20;
        int head_y = 10;
        int dir = 0;
        int SnakeLen = 10;
        

        bool isFault = false;

        int score = 0;
        int record = 0;
        for (int i = 0; i < SnakeLen; i++)
        {
            body_x[i] = head_x - (i * 2);
            body_y[i] = 10;
        }


        SpawnFood();

        while (true) {
            
            for (int i = 0; i < SnakeLen; i++)
            {
                Console.SetCursorPosition(body_x[i], body_y[i]);

                Console.Write("  ");
            }

            Console.SetCursorPosition(head_x, head_y);
            Console.Write("  ");

            Console.SetCursorPosition(foot_x, foot_y);
            Console.Write("  ");            

            if (Console.KeyAvailable == true) {
                ConsoleKeyInfo key;
                Console.SetCursorPosition(0, 0);
                key = Console.ReadKey();
                Console.SetCursorPosition(0, 0);
                Console.Write(" ");

                if(key.Key == ConsoleKey.D && dir != 2) dir = 0;
                if(key.Key == ConsoleKey.S && dir != 3) dir = 1;
                if(key.Key == ConsoleKey.A && dir != 0) dir = 2;
                if(key.Key == ConsoleKey.W && dir != 1) dir = 3;

                if (isFault) {
                    
                    if (key.Key == ConsoleKey.R)
                    {
                        if (record < score)
                        {
                            record = score;
                        }
                        head_x = 20;
                        head_y = 10;
                        dir = 0;
                        SnakeLen = 10;
                        score = 0;
                        for (int i = 0; i < SnakeLen; i++)
                        {
                            body_x[i] = head_x - (i * 2);
                            body_y[i] = 10;
                        }
                        Console.SetCursorPosition(60, 20);
                        Console.Write("             ");
                        Console.SetCursorPosition(60, 21);
                        Console.Write("                                 ");
                        isFault = false;
                    }
                }
            }

            if (isFault == false)
            {
                switch (dir)
                {
                    case 0:
                        head_x += 2;
                        break;
                    case 1:
                        head_y += 1;
                        break;
                    case 2:
                        head_x -= 2;
                        break;
                    case 3:
                        head_y -= 1;
                        break;
                }

                if (head_x < 0) head_x = 118;
                if (head_x > 118) head_x = 0;
                if (head_y < 0) head_y = 39;
                if (head_y > 39) head_y = 0;

                for (int i = SnakeLen; i > 0; i--)
                {
                    body_x[i] = body_x[i - 1];
                    body_y[i] = body_y[i - 1];
                }
            }
            
            

            body_x[0] = head_x;
            body_y[0] = head_y;

            for (int i = 1; i < SnakeLen; i++)
            {
                if (head_x == body_x[i] && head_y == body_y[i]) {
                    Console.SetCursorPosition(60, 20);
                    Console.Write("Вы проиграли!");
                    Console.SetCursorPosition(60, 21);
                    Console.Write("Нажми \"R\" чтобы продолжить игру");
                    isFault = true;
                }
                
            }

            if (head_x == foot_x && head_y == foot_y) {
                SpawnFood();
                SnakeLen++;
                score++;
            }

            Console.SetCursorPosition(0, 0);
            Console.Write("Ваш счёт: " + score);

            Console.SetCursorPosition(0, 1);
            Console.Write("Ваш рекорд: " + record);



            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < SnakeLen; i++)
            {
                Console.SetCursorPosition(body_x[i], body_y[i]);
                
                Console.Write("##");
            }

            Console.SetCursorPosition(head_x, head_y);
            Console.Write("##");
            Console.ResetColor();


            Console.SetCursorPosition(foot_x, foot_y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("**");
            Console.ResetColor();

            System.Threading.Thread.Sleep(50);
        }

        
    }
}

