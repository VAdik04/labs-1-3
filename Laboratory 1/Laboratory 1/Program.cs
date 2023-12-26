using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string path = @"C:\Users\Lenovo\source\repos\Laboratory 1\Laboratory 1\INPUT.TXT";
        string[] lines = File.ReadAllLines(path);

        // Виведення вхідних даних
        Console.WriteLine("Plates:");
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }

        // Перевірка можливості зібрати коробку
        bool possible = CheckBoxPossibility(lines);

        Console.WriteLine(possible ? "POSSIBLE" : "IMPOSSIBLE");

        File.WriteAllText(Path.Combine(Path.GetDirectoryName(path), "OUTPUT.TXT"), possible ? "POSSIBLE" : "IMPOSSIBLE");
    }

    // Функція для перевірки можливості зібрати коробку
    static bool CheckBoxPossibility(string[] lines)
    {
        // Отримання розмірів плиток
        List<int[]> plates = GetPlates(lines);

        // Перевірка наявності трьох пар однакових плиток (з можливістю обертання)
        return CheckThreePairsWithRotation(plates);
    }

    // Функція яка розбирає рядки на дві частини за пробілами
    static List<int[]> GetPlates(string[] lines)
    {
        List<int[]> plates = new List<int[]>();
        foreach (var line in lines)
        {
            string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Перевіряємо, чи маємо масив розміром 2 та чи можна конвертувати в цілі числа.
            if (parts.Length == 2 && int.TryParse(parts[0], out int width) && int.TryParse(parts[1], out int height))
            {
                plates.Add(new int[] { width, height });
            }
        }
        return plates;
    }

    // Функція перевіряє чи є три пари
    static bool CheckThreePairsWithRotation(List<int[]> plates)
    {
        // без обертання
        var counts = plates.GroupBy(p => string.Join(",", p))
                           .Where(g => g.Count() >= 2)
                           .Count();

        if (counts >= 3)
        {
            return true;
        }

        // з обертанням
        var rotatedCounts = plates.GroupBy(p => string.Join(",", p.OrderBy(x => x)))
                                  .Where(g => g.Count() >= 2)
                                  .Count();

        return rotatedCounts >= 3;
    }
}
