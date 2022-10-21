using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ref Piece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        //Right
        if(currX + 1 < tileCountX)
        {
            //Right
            if (board[currX + 1, currY] == null)
                r.Add(new Vector2Int(currX + 1, currY));
            else if(board[currX+1,currY].team != team)
                r.Add(new Vector2Int(currX + 1, currY));

            //Top right
            if(currY + 1 < tileCountY)
                if (board[currX + 1, currY + 1] == null)
                    r.Add(new Vector2Int(currX + 1, currY + 1));
                else if (board[currX + 1, currY + 1].team != team)
                    r.Add(new Vector2Int(currX + 1, currY + 1));

            //Bottom right
            if (currY -1 >= 0)
                if (board[currX + 1, currY - 1] == null)
                    r.Add(new Vector2Int(currX + 1, currY - 1));
                else if (board[currX + 1, currY - 1].team != team)
                    r.Add(new Vector2Int(currX + 1, currY - 1));

        }

        //Left
        if (currX - 1 >= 0)
        {
            //Left
            if (board[currX - 1, currY] == null)
                r.Add(new Vector2Int(currX - 1, currY));
            else if (board[currX - 1, currY].team != team)
                r.Add(new Vector2Int(currX - 1, currY));

            //Top right
            if (currY + 1 < tileCountY)
                if (board[currX - 1, currY + 1] == null)
                    r.Add(new Vector2Int(currX - 1, currY + 1));
                else if (board[currX - 1, currY + 1].team != team)
                    r.Add(new Vector2Int(currX - 1, currY + 1));

            //Bottom right
            if (currY - 1 >= 0)
                if (board[currX - 1, currY - 1] == null)
                    r.Add(new Vector2Int(currX - 1, currY - 1));
                else if (board[currX - 1, currY - 1].team != team)
                    r.Add(new Vector2Int(currX - 1, currY - 1));

        }

        //Up
        if (currY + 1 < tileCountY)
            if (board[currX, currY + 1] == null || board[currX, currY + 1].team != team)
                r.Add(new Vector2Int(currX, currY + 1));

        //Down
        if (currY - 1 >= 0)
            if (board[currX, currY - 1] == null || board[currX, currY - 1].team != team)
                r.Add(new Vector2Int(currX, currY - 1));

        return r;
    }
}
