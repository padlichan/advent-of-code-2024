
using System.Diagnostics;

namespace advent_of_code.december_6;

internal class Challenge6
{
    private char[,] map {  get; set; }
    private GuardPosition guardPos {  get; set; }
    public Challenge6()
    {
        map = GetData();
        guardPos = FindGuard();
        Go();
        Console.WriteLine("December 6");
        Console.WriteLine($"Number of positions visited: {CountX()+1}");
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
        while(isOnMap())
        {
            int canMove = CanMove();
            if (canMove == 1) Move();
            else if (canMove == 0) TurnRight();
            else if (canMove == -1) break;
        }
    }

    private bool isOnMap()
    {
        return guardPos.Row >= 0 && guardPos.Row < map.GetLength(0) 
               && guardPos.Column >= 0 && guardPos.Column < map.GetLength(1);
    }


    private void DrawMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Console.Write(map[i,j]);
            }
            Console.WriteLine();
        }
    }

    private void Move()
    {
        switch(guardPos.Direction)
        {
            case Direction.Up:
            map[guardPos.Row, guardPos.Column] = 'X';
            guardPos.Row--;
            map[guardPos.Row, guardPos.Column] = '^';
            break;

            case Direction.Down:
            map[guardPos.Row, guardPos.Column] = 'X';
            guardPos.Row++;
            map[guardPos.Row, guardPos.Column] = 'V';
            break;

            case Direction.Left:
            map[guardPos.Row, guardPos.Column] = 'X';
            guardPos.Column--;
            map[guardPos.Row, guardPos.Column] = '<';
            break;
            case Direction.Right:
            map[guardPos.Row, guardPos.Column] = 'X';
            guardPos.Column++;
            map[guardPos.Row, guardPos.Column] = '>';
            break;
        }
    }

    private int CanMove()
    {
        switch (guardPos.Direction)
        {
            case Direction.Up:
            if (guardPos.Row - 1 < 0) return -1;
            if(map[guardPos.Row - 1, guardPos.Column] != '#') return 1;
            return 0;

            case Direction.Down:
            if (guardPos.Row + 1 >= map.GetLength(0)) return -1;
            if (map[guardPos.Row + 1, guardPos.Column] != '#') return 1;
            return 0;

            case Direction.Left:
            if (guardPos.Column - 1 < 0) return -1;
            if (map[guardPos.Row, guardPos.Column - 1] != '#') return 1;
            return 0;

            case Direction.Right:
            if (guardPos.Column + 1 >= map.GetLength(1)) return -1;
            if (map[guardPos.Row, guardPos.Column + 1] != '#') return 1;
            return 0;
        }
        return -1;
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
