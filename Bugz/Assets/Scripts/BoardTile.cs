using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardTile
{
    private Bug bug;
    private BoardMark mark;
    private Vector2 tilePos;
    private Vector3 worldPos;

    private BoardMark.MarkType permanentMarkType = BoardMark.MarkType.None;

    public BoardTile(Vector2 tilePos, Vector3 worldPos)
	{
        this.tilePos = tilePos;
        this.worldPos = worldPos;
        CreateMark();
	}

    //Called when you want to create a bug in this tile
    public void CreateBug() 
    {
        int rand = Random.Range(1, 4);
        string resPath = "Bugs/Bug" + rand.ToString(); //Create a random bug (Bug0||Bug1||Bug2||Bug3)
        GameObject go = GameObject.Instantiate(Resources.Load(resPath), worldPos, Quaternion.identity) as GameObject;
        bug = go.GetComponent<Bug>();
    }

    //Called at when creating the tile, it instantiates the mark
    private void CreateMark()
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Board/BoardMark/BoardMark"), worldPos, Quaternion.identity) as GameObject;
        mark = go.GetComponent<BoardMark>();
    }

    //Called when this tile gains the user focus
    public void OnSelected()
    {
        SetTemporaryMarkType(BoardMark.MarkType.Selection);
        if (bug) bug.OnSelected();
    }

    //Called when the focus of the tile has gone
    public void OnUnSelected()
    {
        mark.SetType(permanentMarkType);
        if (bug) bug.OnUnSelected();
    }

    //Set the mark type, that will be kept in the tile until the tile receives the OnUnSelected event
    public void SetTemporaryMarkType(BoardMark.MarkType type)
    {
        mark.SetType(type);
    }

    //Set the mark type, and it will be set as the permanent mark type, that is, the one to change to when the tile receives the OnUnSelected event
    public void SetPermanentMarkType(BoardMark.MarkType type)
    {
        permanentMarkType = type;
        mark.SetType(type);
    }

    public Vector2 GetTilePos() { return tilePos; }
    public Bug GetBug() { return bug; }
    public bool IsEmpty() { return bug == null; }
}
