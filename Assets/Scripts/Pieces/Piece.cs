using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    None = 0,
    Pawn = 1,
    Rook = 2,
    Knight = 3,
    Bishop = 4,
    Queen = 5,
    King = 6
}

public class Piece : MonoBehaviour
{
    public int team;
    public int currX;
    public int currY;
    public PieceType type;

    private Vector3 newPosition;
    private Vector3 newScale = Vector3.one;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition,Time.deltaTime * 10);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * 10); 
    }

    public virtual List<Vector2Int> GetAvailableMoves(ref Piece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();
        r.Add(new Vector2Int(3, 3));
        r.Add(new Vector2Int(3, 4));
        r.Add(new Vector2Int(4, 3));
        r.Add(new Vector2Int(4, 4));

        return r;

    }

    public virtual void SetPosition(Vector3 position, bool force = false)
    {
        newPosition = position;
        if (force)
            transform.position = newPosition;
    }

    public virtual void SetScale(Vector3 position, bool force = false)
    {
        newScale = position;
        if (force)
            transform.localScale = newScale;
    }
}
