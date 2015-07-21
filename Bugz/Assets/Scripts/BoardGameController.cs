using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BoardGameController
{
    public enum State
    {
        MovingBug, WaitingTurn
    }

    [SerializeField]
    private Text lifeText, attackText, defenseText;

    private Board board;
    private BoardTile selectedTile = null;
    private Bug selectedBug = null;

    public BoardGameController(Board board)
	{
        this.board = board;
	}

    public void OnTileSelected(BoardTile tile)
    {
        if (selectedTile != null) selectedTile.OnUnSelected(); //Tell the last selected tile that its not selected anymore

        selectedTile = tile;
        selectedTile.OnSelected(); //Tell the selected tile that its been selected
        selectedBug = selectedTile.GetBug();
        board.GetStatsPanel().PopulateTexts(selectedBug);
    }

    public BoardTile GetSelectedTile() { return selectedTile; }
    public Bug GetSelectedBug() { return selectedBug; }
}
