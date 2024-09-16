using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_CHM
{
    internal class Table
    {
        public decimal[] Y;
        public decimal[] X;
        public decimal[] XX;
        public decimal[] YY;
        public decimal[] EPS_YY;
        public int N;

        public decimal initial_X;
        public decimal end_X;

        public Table(int N)
        {
            X = new decimal[N];
            Y = new decimal[N];
            XX = new decimal[N * 2 - 1];
            YY = new decimal[N * 2 - 1];
            EPS_YY = new decimal[N * 2 - 1];
            this.N = N;
        }

        public void InputFromFile(string path)
        {
            using (StreamReader reader = new StreamReader("C:\\Users\\nices\\source\\repos\\Lab4_CHM\\Lab4_CHM\\" + path))
            {
                reader.ReadLine();
                initial_X = decimal.Parse(reader.ReadLine());
                end_X = decimal.Parse(reader.ReadLine());

                decimal step = Math.Abs((end_X - initial_X)) / (N - 1);
                decimal tempX = initial_X;

                for (int i = 0; i < N; i++)
                {
                    X[i] = tempX;
                    tempX += step;
                }

                string[] line_Y = reader.ReadLine().Split(' ');
                for (int i = 0; i < N; i++)
                {
                    Y[i] = decimal.Parse(line_Y[i]);
                }
            }
        }

        public void OutputTableToConsole()
        {
            Console.Write("X | ");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"{X[i]:f4}" + " |");
            }
            Console.WriteLine();
            Console.Write("Y | ");
            for (int i = 0; i < N; i++)
            {
                Console.Write($"{Y[i]:f4}" + " |");
            }
            Console.WriteLine();
        }

        public void OutputCompactTableToConsole()
        {
            Console.Write("XX | ");
            for (int i = 0; i < XX.Length; i++)
            {
                Console.Write($"{XX[i]:f4}" + " |");
            }
            Console.WriteLine();
            Console.Write("YY | ");
            for (int i = 0; i < YY.Length; i++)
            {
                Console.Write($"{YY[i]:f4}" + " |");
            }
            Console.WriteLine();
            Console.Write("EPS_YY | ");
            for (int i = 0; i < EPS_YY.Length; i++)
            {
                Console.Write($"{EPS_YY[i]:f4}" + " |");
            }
            Console.WriteLine();
            Console.Write("EPS | ");
            for (int i = 0; i < EPS_YY.Length; i++)
            {
                decimal EPS = Math.Abs(EPS_YY[i] - YY[i]);
                Console.Write($"{EPS:f4}" + " |");
            }
            Console.WriteLine();
        }

        public int Validation()
        {
            int ier = 0;
            if (N < 4)
            {
                ier = 1;
            }
            return ier;
        }
    }
}
