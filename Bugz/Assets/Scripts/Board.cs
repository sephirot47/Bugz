using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour 
{
    public static readonly int WidthInTiles = 6, HeightInTiles = 6;
    public static float Width, Height, TileWidth, TileHeight;

    private List<List<BoardTile>> tiles;

    private BoardGameController gameController;
    private BoardMarkManager markManager;

    [SerializeField]
    private BoardCanvasManager canvasManager;

    void Awake()
    {
        Width = GetComponent<MeshRenderer>().bounds.size.x;
        Height = GetComponent<MeshRenderer>().bounds.size.z;
        TileWidth = Width / WidthInTiles;
        TileHeight = Height / HeightInTiles;
    }

	void Start () 
    {
        FillTiles();                                    //Fill the tiles matrix with new BoardTiles()
        gameController = new BoardGameController(this); //Create the game controller
        markManager = new BoardMarkManager(this);       //Create the mark manager (utils to mark the board)
	}
	
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0)) //Handling the selection of a tile
        {
            Vector3 touchPoint = GetTouchWorldPoint();
            if (touchPoint != Vector3.zero) //When a tile is touched
            {
                Vector2 tilePos = GetTilePos(touchPoint);
                BoardTile tile = GetTile(tilePos);
                gameController.OnTileSelected(tile); //Notify the gameController which tile has been selected
            }
        }
	}

    //Fill the tiles matrix with new BoardTiles()
    void FillTiles() 
    {
        tiles = new List<List<BoardTile>>();
        for (int i = 0; i < HeightInTiles; ++i)
        {
            List<BoardTile> tileRow = new List<BoardTile>();
            for (int j = 0; j < WidthInTiles; ++j)
            {
                Vector2 tilePos = new Vector2(j, i);
                BoardTile tile = new BoardTile(tilePos, GetWorldPos(tilePos)); 
                tileRow.Add(tile);

                if (UnityEngine.Random.Range(1, 11) > 9) tile.CreateBug(); //Put some random bugs around
            }
            tiles.Add(tileRow);
        }
    }

    //Gets the worl point where the mouse is pointing right now.
    private Vector3 GetTouchWorldPoint() 
    {
        Vector3 origin = Camera.main.transform.position;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 dir = Camera.main.ScreenToWorldPoint(mousePos) - origin;
        RaycastHit hit;
        if(Physics.Raycast(origin, dir, out hit, 99999.9f)) return hit.point; //We got the point where it's hitting

        return Vector3.zero;
    }

    //Gets the tile in tilePos tile position
    public BoardTile GetTile(Vector2 tilePos) 
    {
        return OutOfBoard(tilePos) ? null : tiles[(int)tilePos.y][(int)tilePos.x];
    }

    //Is the tilePos out of the board boundaries?
    public bool OutOfBoard(Vector2 tilePos)
    {
        return tilePos.x < 0 || tilePos.x >= Board.WidthInTiles || tilePos.y < 0 || tilePos.y >= Board.HeightInTiles;
    }

    //Gets the tilePosition that corresponds to the world position worldPos (returns (0,0), (0,1), (1,0), (1,1), (1,2), etc )
    private Vector2 GetTilePos(Vector3 worldPos) 
    {
        float x = Mathf.Floor( (worldPos.x -  (transform.position.x - Board.Width / 2.0f))   / Board.TileWidth);
        float y = Mathf.Floor( (worldPos.z - (transform.position.z - Board.Height / 2.0f))  / Board.TileHeight);
        return new Vector2(x, y);
    }

    //Gets the CENTER world position of the tile at tilePos (returns world coordinates for the CENTER of the tile)
    private Vector3 GetWorldPos(Vector2 tilePos) 
    {
        return new Vector3(transform.position.x - Board.Width / 2 + tilePos.x * TileWidth + TileWidth / 2.0f, 
                           transform.position.y + GetComponent<MeshRenderer>().bounds.size.y / 2 * 1.03f, 
                           transform.position.z - Board.Height / 2 + tilePos.y * TileHeight + TileHeight / 2.0f);
    }

    public BoardGameController GetGameController() { return gameController; } //Returns the gameController
    public BoardMarkManager GetMarkManager() { return markManager; }          //Returns the markManager
    public BoardCanvasManager GetCanvasManager() { return canvasManager; }             //Return the statsPanel
}
