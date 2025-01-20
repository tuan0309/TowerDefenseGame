using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    private List<Vector2Int> pathCells;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();
        int y = (int)(height / 2);
        int x = 0;

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));
            bool validMove = false;

            while (!validMove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0 || x > (width - 2))
                {
                    x++;
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < height - 2)
                {
                    y++;
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }
        return pathCells;
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public int getCellNeightbourValue(int x, int y)
    {
        int returnValue = 0;
        if (CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }
        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }
        if (CellIsTaken(x + 1, y))
        {
            returnValue += 4;
        }
        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }
        return returnValue;
    }


}