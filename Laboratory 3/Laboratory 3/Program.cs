using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CheckersGame
{
    private const char WhiteChecker = 'W';  // Символ білої шашки
    private const char BlackChecker = 'B';  // Символ чорної шашки
    private const char Empty = '.';         // Символ порожньої клітинки
    private char[,] board = new char[8, 8]; // Гравнева дошка 8x8

    // Конструктор класу, який ініціалізує гравневу дошку з переданими рядками
    public CheckersGame(string[] lines)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = lines[i][j];
            }
        }
    }

    // Метод для пошуку всіх можливих взять на дошці
    public List<string> FindPossibleTakes()
    {
        var possibleTakes = new List<string>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                char currentChecker = board[i, j];
                if (currentChecker == WhiteChecker || currentChecker == BlackChecker)
                {
                    var takes = FindPossibleTakes(i, j, currentChecker);
                    possibleTakes.AddRange(takes.Select(take => $"{currentChecker}: {take}"));
                }
            }
        }

        return possibleTakes;
    }

    // Метод для пошуку можливих взять для конкретної шашки
    private IEnumerable<string> FindPossibleTakes(int x, int y, char checker)
    {
        var takes = new List<string>();

        // Можливі напрямки руху для шашки
        int[] dx = { -1, -1, 1, 1 };
        int[] dy = { -1, 1, -1, 1 };

        for (int d = 0; d < dx.Length; d++)
        {
            int nx = x + dx[d];
            int ny = y + dy[d];

            if (IsWithinBounds(nx, ny) && IsOpponent(checker, board[nx, ny]))
            {
                int nnx = nx + dx[d];
                int nny = ny + dy[d];

                if (IsWithinBounds(nnx, nny) && board[nnx, nny] == Empty)
                {
                    takes.Add($"({x + 1}, {y + 1}) -> ({nnx + 1}, {nny + 1})");
                }
            }
        }

        return takes;
    }

    // Метод для перевірки, чи координати знаходяться в межах дошки
    private bool IsWithinBounds(int x, int y) => x >= 0 && x < 8 && y >= 0 && y < 8;

    // Метод для перевірки, чи символ other є супротивником для шашки checker
    private bool IsOpponent(char checker, char other) =>
        (checker == WhiteChecker && other == BlackChecker) ||
        (checker == BlackChecker && other == WhiteChecker);
}

class Program
{
    static void Main()
    {
        string inputFilePath = @"C:\Users\Lenovo\source\repos\Laboratory 3\Laboratory 3\INPUT.txt";
        string outputFilePath = @"C:\Users\Lenovo\source\repos\Laboratory 3\Laboratory 3\OUTPUT.txt";

        try
        {
            string[] lines = File.ReadAllLines(inputFilePath);
            var game = new CheckersGame(lines);
            var possibleTakes = game.FindPossibleTakes();

            File.WriteAllLines(outputFilePath, possibleTakes);
            possibleTakes.ForEach(Console.WriteLine);

            Console.WriteLine("Done.Check the OUTPUT.txt file for possible takes.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
