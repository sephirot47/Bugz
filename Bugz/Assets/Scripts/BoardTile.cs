using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardTile
{
    private Bug bug;
    private BoardMark mark;
    private Vector2 tilePos;
    private Vector3 worldPos;

    public BoardTile(Vector2 tilePos, Vector3 worldPos)
	{
        this.tilePos = tilePos;
        this.worldPos = worldPos;
        CreateMark();
	}

    public void CreateBug() 
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Bugs/Bug"), worldPos, Quaternion.identity) as GameObject;
        bug = go.GetComponent<Bug>();
    }

    private void CreateMark()
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Board/BoardMark/BoardMark"), worldPos, Quaternion.identity) as GameObject;
        mark = go.GetComponent<BoardMark>();
    }

    public void OnUnSelected()
    {
        mark.ChangeType(BoardMark.MarkType.None);
    }

    public void OnSelected()
    {
        mark.ChangeType(BoardMark.MarkType.Selection);
    }

    public Vector2 GetTilePos() { return tilePos; }
    public BoardMark GetMark() { return mark; }
    public Bug GetBug() { return bug; }
    public bool IsEmpty() { return bug == null; }
}
