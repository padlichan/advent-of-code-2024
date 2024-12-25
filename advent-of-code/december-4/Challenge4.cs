﻿using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code.december_4;

internal class Challenge4
{
    private readonly string path = "Resources/challenge-4-input.txt";

    public Challenge4()
    {
        var verticalData = GetDataVertical();
        var horizontalData = GetDataHorizontal();
        int count = horizontalData.Select(line => CountXmas(line)).Sum();
        count += verticalData.Select(column => CountXmas(column)).Sum();
        count += GetDiagonals(horizontalData).Select(d => CountXmas(d)).Sum();
        count += GetAntiDiagonals(horizontalData).Select(ad => CountXmas(ad)).Sum();
        Console.WriteLine("December 4");
        Console.WriteLine($"XMAS count: {count}\n");
    }

    private int CountXmas(string input)
    {
        string xmasPattern = @"XMAS";
        string samxPattern = @"SAMX";
        return Regex.Matches(input, xmasPattern).Count + Regex.Matches(input, samxPattern).Count;
    }

    private string[] GetDiagonals(string[] data)
    {
        int rows = data.Length;
        int columns = data[0].Length;
        List<string> diagonals = new();  


        for (int i = 0; i < rows; i++)
        {
            string diagonal = GetDiagonal(data, 0, i); 
            diagonals.Add(diagonal);
        }

        for (int i = 1; i < columns; i++)
        {
            string diagonal = GetDiagonal(data, i, 0);
            diagonals.Add(diagonal);
        }
        return diagonals.ToArray();
    }

    private string[] GetAntiDiagonals(string[] data)
    {
        int rows = data.Length;
        int columns = data[0].Length;
        List<string> antiDiagonals = [];
        for (int i = 0; i < rows; i++)
        {
            string antiDiagonal = GetAntiDiagonal(data, columns - 1, i);
            antiDiagonals.Add(antiDiagonal);
        }
        for (int i = 0; i < columns-1; i++)
        {
            string antiDiagonal = GetAntiDiagonal(data, i, 0);
            antiDiagonals.Add(antiDiagonal);
        }
        return antiDiagonals.ToArray();
    }
    private string GetDiagonal(string[] horizontalData, int startColumn, int startRow)
    {
        int col = startColumn;
        int row = startRow;
        StringBuilder diagonal = new();

        while (col < horizontalData[0].Length && row < horizontalData.Length)
        {
            diagonal.Append(horizontalData[row][col]);
            row++;
            col++;
        }
        return diagonal.ToString();
    }

    private string GetAntiDiagonal(string[] horizontalData, int startColumn, int startRow)
    {
        int col = startColumn;
        int row = startRow;
        StringBuilder diagonal = new();
        while (col >= 0 && row < horizontalData.Length)
        {
            diagonal.Append(horizontalData[row][col]);
            row++;
            col--;
        }
        return diagonal.ToString();
    }

    private string[] GetDataHorizontal()
    {
        string[] horizontalDataArray = File.ReadAllText(path).Split(Environment.NewLine);
        return horizontalDataArray;
    }

    private string[] GetDataVertical()
    {
        string[] data = File.ReadAllText(path).Split(Environment.NewLine);
        List<string> verticalData = [];
        for (int i = 0; i < data[0].Length; i++)
        {
            var column = new string(data.Select(line => line[i]).ToArray());
            verticalData.Add(column);
        }
        return verticalData.ToArray();
    }
}