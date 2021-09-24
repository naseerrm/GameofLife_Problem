using System;
using System.Collections.Generic;
using System.Linq;

namespace GameofLife_Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = new int[25, 25];
            var inputRowsColumns = new List<(int, int)>();
            int nextGenerationInput = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your inputs,to end press semicolon(;) on new line");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.Contains(";"))
                {
                    break;
                }
                else
                {
                    inputRowsColumns.Add((Convert.ToInt32(input.Split(' ')[0]), Convert.ToInt32(input.Split(' ')[1])));
                }
            }
            //update live values in the array
            foreach (var i in inputRowsColumns)
            {
                if (i.Item1 <= 24 && i.Item2 <= 24)
                {
                    array[i.Item1, i.Item2] = 1;
                }
            }
            for (int i = 0; i < nextGenerationInput; i++)
            {
                array = Program.SolveGrid(array);
            }
            Program.PrintOutput(array);
            Console.ReadKey();
        }
        public static void PrintOutput(int[,] grid)
        {
            for (int temprow = 0; temprow <= 24; temprow++)
            {
                for (int tempcolumn = 0; tempcolumn <= 24; tempcolumn++)
                {
                    if (grid[temprow, tempcolumn] == 1)
                    {
                        Console.Write($"({temprow},{tempcolumn})");
                    }
                }
            }
        }
        public static int[,] SolveGrid(int[,] gridValue)
        {
            int[,] orginalgrid = new int[25, 25];
            Array.Copy(gridValue, orginalgrid, gridValue.Length);
            for (int temprow = 0; temprow <= 24; temprow++)
            {
                for (int tempcolumn = 0; tempcolumn <= 24; tempcolumn++)
                {
                    if (gridValue[temprow, tempcolumn] == 0)
                    {
                        var adjacentValues = findAdjacentvalues(24, 24, orginalgrid, temprow, tempcolumn);
                        if (adjacentValues.Where(x => x == 1).Count() == 3)
                        {
                            gridValue[temprow, tempcolumn] = 1;
                        }
                    }
                    else if (gridValue[temprow, tempcolumn] == 1)
                    {
                        var adjacentValues = findAdjacentvalues(24, 24, orginalgrid, temprow, tempcolumn);
                        if (adjacentValues.Where(x => x == 1).Count() <= 1 || adjacentValues.Where(x => x == 1).Count() >= 4)
                        {
                            gridValue[temprow, tempcolumn] = 0;
                        }
                    }
                }
            }
            return gridValue;
        }
        public static List<int> findAdjacentvalues(int row, int column, int[,] array, int currentPlacerow, int currentPlaceColumn)
        {
            List<int> adjacentValues = new List<int>();
            if (currentPlacerow <= row && currentPlaceColumn - 1 <= column && currentPlaceColumn != 0)//current place left side
            {
                adjacentValues.Add(array[currentPlacerow, currentPlaceColumn - 1]);
            }
            if (currentPlacerow <= row && currentPlaceColumn + 1 <= column)//current place right side
            {
                adjacentValues.Add(array[currentPlacerow, currentPlaceColumn + 1]);
            }
            if (currentPlacerow + 1 <= row && currentPlaceColumn - 1 <= column && currentPlaceColumn != 0)
            {
                adjacentValues.Add(array[currentPlacerow + 1, currentPlaceColumn - 1]);
            }
            if (currentPlacerow + 1 <= row && currentPlaceColumn + 1 <= column)
            {
                adjacentValues.Add(array[currentPlacerow + 1, currentPlaceColumn + 1]);
            }
            if (currentPlacerow + 1 <= row && currentPlaceColumn <= column)
            {
                adjacentValues.Add(array[currentPlacerow + 1, currentPlaceColumn]);
            }
            if (currentPlacerow - 1 <= row && currentPlaceColumn - 1 <= column && currentPlaceColumn != 0 && currentPlacerow != 0)
            {
                adjacentValues.Add(array[currentPlacerow - 1, currentPlaceColumn - 1]);
            }
            if (currentPlacerow - 1 <= row && currentPlaceColumn + 1 <= column && currentPlacerow != 0)
            {
                adjacentValues.Add(array[currentPlacerow - 1, currentPlaceColumn + 1]);
            }
            if (currentPlacerow - 1 <= row && currentPlaceColumn <= column && currentPlacerow != 0)
            {
                adjacentValues.Add(array[currentPlacerow - 1, currentPlaceColumn]);
            }
            return adjacentValues;
        }
    }
}