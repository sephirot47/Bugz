using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//This is the class responsible for managing the logic of the board game itself
public class BoardGameController
{
    //Current state of the game
    public enum State
    {
        MovingBug, WaitingTurn
    }

    [SerializeField]
    private Text lifeText, attackText, defenseText; //References set on the editor, of the different stats texts

    private Board board;                   //A reference to the game board, for easy access
    private BoardTile selectedTile = null; //Holds the current selected Tile
    private Bug selectedBug = null;        //Holds the current selected Bug

    public BoardGameController(Board board)
	{
        this.board = board;
	}

    //Called when the tile 'tile' is selected
    public void OnTileSelected(BoardTile tile)
    {
        if (selectedTile == tile) //When clicking on a selected tile, unselect it
        {
            board.GetMarkManager().ClearBoardMarks();
            selectedTile.OnUnSelected();
            selectedTile = null;
        }
        else
        {
            if (selectedTile != null) selectedTile.OnUnSelected(); //Tell the last selected tile that its not selected anymore

            board.GetMarkManager().ClearBoardMarks(); //Clear the mark boards

            selectedTile = tile;
            selectedTile.OnSelected(); //Tell the selected tile that its been selected
            selectedBug = selectedTile.GetBug();
            board.GetCanvasManager().PopulateStatsTexts(selectedBug); //Populate the texts on the StatsPanel

            if (!tile.IsEmpty()) //If the tile isnt empty, do something with the bug
            {
                Bug bug = tile.GetBug();
                int movementRange = bug.GetMovementRange();

                //Mark the board with the tiles the selected bug can move(travel) to
                Vector2 selectedTilePos = tile.GetTilePos();
                List<Vector2> bugPossibleMoves = bug.GetPossibleMoves();
                board.GetMarkManager().MarkMovementRangeOnTheBoard(selectedTilePos, bugPossibleMoves);
            }
        }
    }


    public BoardTile GetSelectedTile() { return selectedTile; }
    public Bug GetSelectedBug() { return selectedBug; }
}
