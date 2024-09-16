using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Polinom2
{
    public decimal[] coefficients = new decimal[3];

    public Polinom2(decimal[] XValues, decimal[] YValues)
    {
        decimal[,] system = new decimal[3, 3];
        for (int col = 0; col < 3; col++)
        {
            for (int row = 0; row < 3; row++)
            {
                if (col == 0)
                {
                    system[row, col] = 1;
                }
                else if (col == 1)
                {
                    system[row, col] = XValues[row];
                }
                else if (col == 2)
                {
                    system[row, col] = XValues[row] * XValues[row];
                }
            }
        }

        decimal[] y_col = new decimal[3];
        for (int i = 0; i < 3; i++)
        {
            y_col[i] = YValues[i];
        }

        for (int row = 0; row < 2; row++)
        {
            DivideLine(row, system, y_col);

            for (int subrow = row + 1; subrow < 3; subrow++)
            {
                Subtract(row, subrow, system, y_col);
            }
        }

        for (int row = 2; row > 0; row--)
        {
            DivideLine(row, system, y_col);

            for (int subrow = row - 1; subrow >= 0; subrow--)
            {
                Subtract(row, subrow, system, y_col);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            coefficients[i] = y_col[i];
        }
    }

    public static bool ArePolysEqual(Polinom2 poly1, Polinom2 poly2)
    {
        for (int coef = 0; coef < 3; coef++)
        {
            if (poly1.coefficients[coef] != poly2.coefficients[coef])
            {
                return false;
            }
        }
        return true;
    }

    public static void DivideLine(int rowIndex, decimal[,] matrix, decimal[] y_col)
    {
        decimal b_element = matrix[rowIndex, rowIndex];
        for (int col = 0; col < 3; col++)
        {
            matrix[rowIndex, col] /= b_element;
        }
        y_col[rowIndex] /= b_element;
    }

    public static void Subtract(int firstRow, int secondRow, decimal[,] matrix, decimal[] y_col) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        decimal koef = matrix[secondRow, firstRow];
        for (int col = 0; col < 3; col++)
        {
            matrix[secondRow, col] -= matrix[firstRow, col] * koef;
        }
        y_col[secondRow] -= y_col[firstRow] * koef;
    }

    public void OutputPolinomToConsole()
    {
        Console.WriteLine($"P = {coefficients[0]:f4} + {coefficients[1]:f4}x + {coefficients[2]:f4}x^2");
    }
}

