using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string inputFile = @"C:\Users\Lenovo\source\repos\Laboratory2\Laboratory2\INPUT.txt";
        string outputFile = @"C:\Users\Lenovo\source\repos\Laboratory2\Laboratory2\OUTPUT.txt";

        string[] lines = File.ReadAllLines(inputFile);
        int n = int.Parse(lines[0]);

        //Другий рядок розбираємо на числа та записуємо в масив
        int[] nails = lines[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                             .Select(int.Parse).ToArray();
        // Робим перевірку
        if (nails.Length != n)
        {
            Console.WriteLine("Error: the number of studs specified in the file does not correspond to the number of specified coordinates.");
            return;
        }

        Console.WriteLine($"Number of cloves: {n}");
        Console.WriteLine($"Coordinates of cloves: {string.Join(", ", nails)}");


        //Сортуємо в порядку зростання
        Array.Sort(nails);

        int totalLength = 0;
        for (int i = 1; i < nails.Length; i++)  //Обчислюємо мінімальну загальну довжину між гвоздиками
        {
            totalLength += nails[i] - nails[i - 1];
        }

        Console.WriteLine($"The minimum total length of all threads: {totalLength}");

        File.WriteAllText(outputFile, totalLength.ToString());
    }
}
