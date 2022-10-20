using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Art Stuff")]
    [SerializeField] private Material tileMaterial;
    [SerializeField] private float tileSize = 1.0f;
    [SerializeField] private float yOffset = 0.15f;
    [SerializeField] private Vector3 boardCenter = Vector3.zero;

    [Header("Prefabs & Materials")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Material[] teamMaterials;

    // LOGIC
    private Piece[,] boardPieces;
    private const int TILE_COUNT_X = 8;
    private const int TILE_COUNT_Y = 8;
    private GameObject[,] tiles;
    private Camera currentCamera;
    private Vector2Int currentHover;
    [SerializeField] private Vector3 bounds;

    private void Awake()
    {
        GenerateTiles(tileSize, TILE_COUNT_X, TILE_COUNT_Y);

        SpawnAllPieces();
        PositionAllPieces();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentCamera /*|| !boardActive*/)
        {
            currentCamera = Camera.main;
            return;
        }

        RaycastHit info;
        Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out info, 100, LayerMask.GetMask("Tile", "Hover")))
        {
            // Get the indexes of the tile i've hit
            Vector2Int hitPosition = LookupTileIndex(info.transform.gameObject);

            // If we're hovering a tile after not hovering any tiles
            if (currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            // If we were already hovering a tile, change the previous one
            if (currentHover != hitPosition)
            {
                tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }
            else
            {
                if (currentHover != -Vector2Int.one)
                {
                    tiles[currentHover.x, currentHover.y].layer = LayerMask.NameToLayer("Tile");
                    currentHover = -Vector2Int.one;
                }
            }
        }
    }
    private void GenerateTiles(float tileSize, int tileCountX, int tileCountY)
    {
        yOffset += transform.position.y;
        bounds = new Vector3((tileCountX / 2) * tileSize, 0, (tileCountY / 2) * tileSize) + boardCenter;

        tiles = new GameObject[tileCountX, tileCountY];
        for (int x = 0; x < tileCountX; ++x)
        {
            for (int y = 0; y < tileCountY; ++y)
            {
                tiles[x, y] = GenerateTile(tileSize, x, y);
            }
        }
    }

    private GameObject GenerateTile(float tileSize, int posX, int posY)
    {
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", posX, posY));
        tileObject.transform.parent = transform;

        Mesh mesh = new Mesh();
        tileObject.AddComponent<MeshFilter>().mesh = mesh;
        tileObject.AddComponent<MeshRenderer>().material = tileMaterial;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(posX * tileSize, yOffset, posY * tileSize) - bounds;
        vertices[1] = new Vector3(posX * tileSize, yOffset, (posY + 1) * tileSize) - bounds;
        vertices[2] = new Vector3((posX + 1) * tileSize, yOffset, posY * tileSize) - bounds;
        vertices[3] = new Vector3((posX + 1) * tileSize, yOffset, (posY + 1) * tileSize) - bounds;

        int[] tris = new int[] { 0, 1, 2, 1, 3, 2 };

        mesh.vertices = vertices;
        mesh.triangles = tris;
        mesh.RecalculateNormals();

        tileObject.layer = LayerMask.NameToLayer("Tile");
        tileObject.AddComponent<BoxCollider>();


        return tileObject;
    }

    private void SpawnAllPieces()
    {
        boardPieces = new Piece[TILE_COUNT_X, TILE_COUNT_Y];

        int whites = 0;
        int blacks = 1;

        // white team
        boardPieces[0, 0] = SpawnSinglePiece(PieceType.Rook, whites);
        boardPieces[0, 1] = SpawnSinglePiece(PieceType.Knight, whites);
        boardPieces[0, 2] = SpawnSinglePiece(PieceType.Bishop, whites);
        boardPieces[0, 3] = SpawnSinglePiece(PieceType.Queen, whites);
        boardPieces[0, 4] = SpawnSinglePiece(PieceType.King, whites);
        boardPieces[0, 5] = SpawnSinglePiece(PieceType.Bishop, whites);
        boardPieces[0, 6] = SpawnSinglePiece(PieceType.Knight, whites);
        boardPieces[0, 7] = SpawnSinglePiece(PieceType.Rook, whites);

        // black team
        boardPieces[7, 0] = SpawnSinglePiece(PieceType.Rook, blacks);
        boardPieces[7, 1] = SpawnSinglePiece(PieceType.Knight, blacks);
        boardPieces[7, 2] = SpawnSinglePiece(PieceType.Bishop, blacks);
        boardPieces[7, 3] = SpawnSinglePiece(PieceType.King, blacks);
        boardPieces[7, 4] = SpawnSinglePiece(PieceType.Queen, blacks);
        boardPieces[7, 5] = SpawnSinglePiece(PieceType.Bishop, blacks);
        boardPieces[7, 6] = SpawnSinglePiece(PieceType.Knight, blacks);
        boardPieces[7, 7] = SpawnSinglePiece(PieceType.Rook, blacks);

        for(int i = 0; i < TILE_COUNT_X; ++i)
        {
            boardPieces[1, i] = SpawnSinglePiece(PieceType.Pawn, whites);
            boardPieces[6, i] = SpawnSinglePiece(PieceType.Pawn, blacks);
        }

    }

    // Positioning

    private void PositionAllPieces()
    {
        for(int x = 0; x < TILE_COUNT_X; ++x)
        {
            for (int y = 0; y < TILE_COUNT_Y; ++y)
            {
                if (boardPieces[x, y] != null)
                {
                    PositionSinglePiece(x, y, true);
                }
            }
        }
    }

    private void PositionSinglePiece(int x, int y, bool force = false)
    {
        boardPieces[x, y].currX = x;
        boardPieces[x, y].currY = y;
        boardPieces[x, y].transform.position = GetTileCenter(x, y);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        return new Vector3(x * tileSize, yOffset, y * tileSize) - bounds +  new Vector3(tileSize/2, 0, tileSize/2);
    }

    private Piece SpawnSinglePiece(PieceType type, int team)
    {
        Piece p = Instantiate(prefabs[(int)type - 1], transform).GetComponent<Piece>();
        
        p.type = type;
        p.team = team;
        // p.GetComponent<MeshRenderer>().material = teamMaterials[team];

        return p;
    }
    private Vector2Int LookupTileIndex(GameObject hitInfo)
    {
        for (int x = 0; x < TILE_COUNT_X; ++x)
        {
            for (int y = 0; y < TILE_COUNT_Y; ++y)
            {
                if (tiles[x, y] == hitInfo)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return -Vector2Int.one;
    }


}
