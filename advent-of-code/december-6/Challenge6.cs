
using System.Diagnostics;

namespace advent_of_code.december_6;

internal class Challenge6
{
    private char[,] map { get; set; }
    private GuardPosition guardPos { get; set; }
    public Challenge6()
    {
        map = GetData();
        guardPos = FindGuard();
        Go();
        Console.WriteLine("December 6");
        Console.WriteLine($"Number of positions visited: {CountX() + 1}");
        Console.WriteLine();
    }

    private int CountX()
    {
        int count = 0;
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == 'X') count++;
            }
        }
        return count;
    }

    private void Go()
    {
        while (IsOnMap(guardPos.Row, guardPos.Column))
        {
            int canMove = CanMove();
            if (canMove == 1) Move();
            else if (canMove == 0) TurnRight();
            else if (canMove == -1) break;
        }
    }

    private bool IsOnMap(int row, int column)
    {
        return row >= 0 && row < map.GetLength(0)
               && column >= 0 && column < map.GetLength(1);
    }

    private void DrawMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i, j]);
            }
            Console.WriteLine();
        }
    }

    private void Move()
    {
        var newpos = GetNextPosition();
        map[guardPos.Row, guardPos.Column] = 'X';
        guardPos.Row = newpos.row;
        guardPos.Column = newpos.column;
        map[guardPos.Row, guardPos.Column] = DirectionToChar(guardPos.Direction);
    }

    private (int row, int column) GetNextPosition()
    {
        switch (guardPos.Direction)
        {
            case Direction.Up:
            return (guardPos.Row - 1, guardPos.Column);

            case Direction.Down:
            return (guardPos.Row + 1, guardPos.Column);

            case Direction.Left:
            return (guardPos.Row, guardPos.Column - 1);

            case Direction.Right:
            return (guardPos.Row, guardPos.Column + 1);
        }
        return (guardPos.Row, guardPos.Column);
    }
    private int CanMove()
    {
        var nextpos = GetNextPosition();
        if (!IsOnMap(nextpos.row, nextpos.column)) return -1;
        if (map[nextpos.row, nextpos.column] == '#') return 0;
        return 1; 
    }

    private void TurnRight()
    {
        switch (guardPos.Direction)
        {
            case Direction.Up: guardPos.Direction = Direction.Right; break;
            case Direction.Down: guardPos.Direction = Direction.Left; break;
            case Direction.Left: guardPos.Direction = Direction.Up; break;
            case Direction.Right: guardPos.Direction = Direction.Down; break;
        }
        map[guardPos.Row, guardPos.Column] = DirectionToChar(guardPos.Direction);
    }

    private GuardPosition FindGuard()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {

                if (map[i, j] != '.' && map[i, j] != '#')
                {
                    Direction direction = CharToDirection(map[i, j]);
                    return new GuardPosition(i, j, direction);
                }

            }
        }
        return new GuardPosition(-1, -1, Direction.Up);
    }

    private Direction CharToDirection(char c)
    {
        if (c == '^') return Direction.Up;
        if (c == 'V') return Direction.Down;
        if (c == '<') return Direction.Left;
        if (c == '>') return Direction.Right;
        return Direction.Up;
    }

    private char DirectionToChar(Direction direction)
    {
        if (direction == Direction.Up) return '^';
        if (direction == Direction.Down) return 'v';
        if (direction == Direction.Left) return '<';
        if (direction == Direction.Right) return '>';
        return '^';
    }

    private char[,] GetData()
    {
        var dataArray = File.ReadAllText("Resources/challenge-6-input.txt").Split(Environment.NewLine);
        var map = new char[dataArray.Length, dataArray[0].Length];

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = dataArray[i][j];
            }
        }
        return map;
    }
}

public class GuardPosition(int x, int y, Direction direction)
{
    public int Row = x;
    public int Column = y;
    public Direction Direction = direction;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
