

using Lab4_CHM;
using System;
using System.Runtime.ConstrainedExecution;


class Program
{
    static int IER = 0;

    public static int GetNFromFile(string path)
    {
        using (StreamReader reader = new StreamReader("C:\\Users\\nices\\source\\repos\\Lab4_CHM\\Lab4_CHM\\" + path))
        {
            string line = reader.ReadLine();
            return int.Parse(line);
        }
    }

    public static void Compacting(Table table)
    {
        Polinom2[] polinoms2 = new Polinom2[table.N / 2];
        for (int i = 0; i < table.N - 1; i+=2)
        {
            decimal[] subsetX2 = new decimal[3];
            decimal[] subsetY2 = new decimal[3];
            if (table.N % 2 == 0 && i == table.N - 2)
            {
                Array.Copy(table.X, i - 1, subsetX2, 0, 3);
                Array.Copy(table.Y, i - 1, subsetY2, 0, 3);
            }
            else
            {
                Array.Copy(table.X, i, subsetX2, 0, 3);
                Array.Copy(table.Y, i, subsetY2, 0, 3);
            }
            polinoms2[i / 2] = new Polinom2(subsetX2, subsetY2);
        }

        Polinom3[] polinoms3 = new Polinom3[(table.N + 1) / 3];
        for (int i = 0; i < table.N - 1; i+=3)
        {
            decimal[] subsetX3 = new decimal[4];
            decimal[] subsetY3 = new decimal[4];
            if (table.N % 3 == 2 && i == table.N - 1 - 1)
            {
                Array.Copy(table.X, i - 2, subsetX3, 0, 4);
                Array.Copy(table.Y, i - 2, subsetY3, 0, 4);
            }
            else if (table.N % 3 == 0 && i == table.N - 2 - 1)
            {
                Array.Copy(table.X, i - 1, subsetX3, 0, 4);
                Array.Copy(table.Y, i - 1, subsetY3, 0, 4);
            }
            else
            {
                Array.Copy(table.X, i, subsetX3, 0, 4);
                Array.Copy(table.Y, i, subsetY3, 0, 4);
            }
            polinoms3[i / 3] = new Polinom3(subsetX3, subsetY3);
        }

        decimal step = Math.Abs((table.end_X - table.initial_X)) / (table.XX.Length - 1);
        decimal tempX = table.initial_X;
        for (int i = 0; i < table.XX.Length; i++)
        {
            table.XX[i] = tempX;
            tempX += step;
        }
        
        for (int i = 0; i < table.XX.Length; i++)
        {
            if (i % 2 == 0)
            {
                table.YY[i] = table.Y[i/2];
                table.EPS_YY[i] = table.Y[i / 2];
            }
            else
            {
                table.YY[i] = polinoms2[i / 4].coefficients[0] + polinoms2[i / 4].coefficients[1] * table.XX[i] + polinoms2[i / 4].coefficients[2] * table.XX[i] * table.XX[i];
                table.EPS_YY[i] = polinoms3[i / 6].coefficients[0] + polinoms3[i / 6].coefficients[1] * table.XX[i] + polinoms3[i / 6].coefficients[2] * table.XX[i] * table.XX[i] +
                    polinoms3[i / 6].coefficients[3] * table.XX[i] * table.XX[i] * table.XX[i];
            }
        }
    }

    public static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Ввод таблицы значений с файла;\n2) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите название файла: ");
                    string path = Console.ReadLine();
                    int N_from_table = GetNFromFile(path);
                    Table table_from_file = new Table(N_from_table);
                    IER = table_from_file.Validation();
                    if (IER != 1)
                    {
                        table_from_file.InputFromFile(path);
                        table_from_file.OutputTableToConsole();
                        Compacting(table_from_file);
                        table_from_file.OutputCompactTableToConsole();
                    }
                    Console.WriteLine($"IER = {IER}");
                    break;
                case "2":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Неправильный ввод");
                    break;
            }
        }
    }
}
