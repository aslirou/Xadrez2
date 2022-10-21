using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override List<Vector2Int> GetAvailableMoves(ref Piece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;

        // One in front
        if (board[currX, currY + direction] == null)
            r.Add(new Vector2Int(currX, currY + direction));

        //Two in front
        if (board[currX, currY + direction] == null)
        {
            //White team
            if (team == 0 && currY == 1 && board[currX, currY + (direction * 2)] == null)
                r.Add(new Vector2Int(currX, currY + (direction * 2)));

            if (team == 1 && currY == 6 && board[currX, currY + (direction * 2)] == null)
                r.Add(new Vector2Int(currX, currY + (direction * 2)));
        }

        //Kill move
        if (currX != tileCountX - 1)
            if (board[currX + 1, currY + direction] != null && board[currX + 1, currY + direction].team != team)
                r.Add(new Vector2Int(currX + 1, currX + direction));

        if (currX != 0)
            if (board[currX - 1, currY + direction] != null && board[currX - 1, currY + direction].team != team)
                r.Add(new Vector2Int(currX - 1, currX + direction));

        return r;
    }
}
