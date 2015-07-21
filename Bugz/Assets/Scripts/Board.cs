using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour 
{
    public static readonly int WidthInTiles = 6, HeightInTiles = 6;
    public static float Width, Height, TileWidth, TileHeight;

    private List<List<BoardTile>> tiles;

    private BoardGameController gameController;

    [SerializeField]
    private BoardStatsPanel statsPanel;

    void Awake()
    {
        Width = GetComponent<MeshRenderer>().bounds.size.x;
        Height = GetComponent<MeshRenderer>().bounds.size.z;
        TileWidth = Width / WidthInTiles;
        TileHeight = Height / HeightInTiles;
    }

	void Start () 
    {
        FillTiles();
        gameController = new BoardGameController(this);
	}
	
	void Update () 
    {
	    if(Input.GetMouseButtonDown(0)) //Selection of a tile
        {
            Vector3 touchPoint = GetTouchWorldPoint();
            if (touchPoint != Vector3.zero) //Touched a tile
            {
                Vector2 tilePos = GetTilePos(touchPoint);
                BoardTile tile = GetTile(tilePos);
                gameController.OnTileSelected(tile);
            }
        }
	}

    void FillTiles() //Fill the tiles matrix with new BoardTiles()
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

    private BoardTile GetTile(Vector2 tilePos) //Gets the tile in tilePos tile position
    {
        return tiles[(int)tilePos.y][(int)tilePos.x];
    }

    private Vector2 GetTilePos(Vector3 worldPos) //Gets the tilePosition that corresponds to the world position worldPos
    {
        float x = Mathf.Floor( (worldPos.x -  (transform.position.x - Board.Width / 2.0f))   / Board.TileWidth);
        float y = Mathf.Floor( (worldPos.z - (transform.position.z - Board.Height / 2.0f))  / Board.TileHeight);
        return new Vector2(x, y);
    }

    private Vector3 GetWorldPos(Vector2 tilePos) //Gets the CENTER world position of the tile at tilePos
    {
        return new Vector3(transform.position.x - Board.Width / 2 + tilePos.x * TileWidth + TileWidth / 2.0f, 
                           transform.position.y + GetComponent<MeshRenderer>().bounds.size.y / 2 * 1.03f, 
                           transform.position.z - Board.Height / 2 + tilePos.y * TileHeight + TileHeight / 2.0f);
    }

    public BoardGameController GetGameController() { return gameController; }
    public BoardStatsPanel GetStatsPanel() { return statsPanel; }
}
