using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ref Piece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Down
        for (int i = currY - 1; i >= 0; i--)
        {
            if (board[currX, i] == null)
                r.Add(new Vector2Int(currX, i));

            if (board[currX, i] != null)
            {
                if (board[currX, i].team != team)
                    r.Add(new Vector2Int(currX, i));
                break;
            }
        }

        //Up
        for (int i = currY + 1; i < tileCountY; i++)
        {
            if (board[currX, i] == null)
                r.Add(new Vector2Int(currX, i));

            if (board[currX, i] != null)
            {
                if (board[currX, i].team != team)
                    r.Add(new Vector2Int(currX, i));
                break;
            }
        }

        //Left
        for (int i = currX - 1; i >= 0; i--)
        {
            if (board[i, currY] == null)
                r.Add(new Vector2Int(i, currY));

            if (board[i, currY] != null)
            {
                if (board[i, currY].team != team)
                    r.Add(new Vector2Int(i, currY));
                break;
            }
        }

        //Right
        for (int i = currX + 1; i < tileCountX; i++)
        {
            if (board[i, currY] == null)
                r.Add(new Vector2Int(i, currY));

            if (board[i, currY] != null)
            {
                if (board[i, currY].team != team)
                    r.Add(new Vector2Int(i, currY));
                break;
            }
        }

        // Top right
        for (int x = currX + 1, y = currY + 1; x < tileCountX && y < tileCountY; x++, y++)
        {
            if (board[x, y] == null)
                r.Add(new Vector2Int(x, y));

            else
            {
                if (board[x, y].team != team)
                    r.Add(new Vector2Int(x, y));

                break;
            }

        }

        // Top left
        for (int x = currX - 1, y = currY + 1; x >= 0 && y < tileCountY; x--, y++)
        {
            if (board[x, y] == null)
                r.Add(new Vector2Int(x, y));

            else
            {
                if (board[x, y].team != team)
                    r.Add(new Vector2Int(x, y));

                break;
            }

        }

        // Bottom right
        for (int x = currX + 1, y = currY - 1; x < tileCountX && y >= 0; x++, y--)
        {
            if (board[x, y] == null)
                r.Add(new Vector2Int(x, y));

            else
            {
                if (board[x, y].team != team)
                    r.Add(new Vector2Int(x, y));

                break;
            }

        }

        // Bottom left
        for (int x = currX - 1, y = currY - 1; x >= 0 && y >= 0; x--, y--)
        {
            if (board[x, y] == null)
                r.Add(new Vector2Int(x, y));

            else
            {
                if (board[x, y].team != team)
                    r.Add(new Vector2Int(x, y));

                break;
            }

        }

        return r;
    }
}
