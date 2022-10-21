using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ref Piece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Down
        for(int i = currY -1; i >= 0; i--)
        {
            if (board[currX, i] == null)
                r.Add(new Vector2Int(currX, i));

            if (board[currX, i] != null)
            {
                if(board[currX, i].team != team)
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

        return r;
    }
}
