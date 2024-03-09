using System;
using System.Threading;

namespace Cronometro
{
    class Program
    {
        public static void Start()
        {
            short h = 0, m = 0, s = 0;
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.Clear();
                    s++;

                    if (s == 60)
                    {
                        s = 0;
                        m++;
                    }

                    if (m == 60)
                    {
                        m = 0;
                        h++;
                    }

                    if (h == 24)
                    {
                        h = 0;
                    }

                    Console.WriteLine($"{h.ToString("00")}:{m.ToString("00")}:{s.ToString("00")}");
                    Console.WriteLine("Press enter key to stop");
                    Thread.Sleep(1000);
                }
            } while (Console.ReadKey().Key != ConsoleKey.Enter);
        }

        static void Main(string[] args)
        {
            Start();
        }
    }
}
