using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//This is the class responsible for managing the logic of the board game itself
public class BoardGameController : MonoBehaviour, IActionListener
{
    //Current state of the game
    public enum BoardGameState
    {
        MovingBug,        //Selecting the tile to move to
        AttackingWithBug, //Selecting the tile to attack to
        Pointing,         //When selecting the tile
        WaitingTurn
    }

    [SerializeField]
    private Board board;                   //A reference to the game board, for easy access
    private BoardTile selectedTile = null; //Holds the current selected Tile
    private Bug selectedBug = null;        //Holds the current selected Bug

    private BoardGameState currentGameState;

    public void Start()
    {
        currentGameState = BoardGameState.Pointing;
    }

    //Called when the tile 'tile' is selected
    //This method spreads the event to all the ITileListeners
    public void OnTileSelected(BoardTile tile)
    {
        if (selectedTile == tile) //When clicking on a selected tile, unselect it
        {
            board.GetMarkManager().ClearBoardMarks();
            selectedTile.OnUnSelected();
            selectedTile = null;

            //Spread the event
            List<ITileListener> tileListeners = Utils.GetAll<ITileListener>();
            foreach (ITileListener tileListener in tileListeners) tileListener.OnTileUnselected(tile);
        }
        else //Tile selected for the first time
        {
            if (selectedTile != null) selectedTile.OnUnSelected(); //Tell the last selected tile that its not selected anymore

            board.GetMarkManager().ClearBoardMarks(); //Clear the mark boards

            selectedTile = tile;
            selectedTile.OnSelected(); //Tell the selected tile that its been selected
            selectedBug = selectedTile.GetBug();

            //THERE'S A BUG IN THE SELECTED TILE!
            if (!tile.IsEmpty()) //If the tile isnt empty, do something with the bug
            {
                Bug bug = tile.GetBug();
                int movementRange = bug.GetMovementRange();

                //Mark the board with the tiles the selected bug can move(travel) to
                Vector2 selectedTilePos = tile.GetTilePos();
                List<Vector2> bugPossibleMoves = bug.GetPossibleMoves();
                board.GetMarkManager().MarkMovementRangeOnTheBoard(selectedTilePos, bugPossibleMoves);
            }

            //Spread the event
            List<ITileListener> tileListeners = Utils.GetAll<ITileListener>();
            foreach (ITileListener tileListener in tileListeners) tileListener.OnTileSelected(tile);
        }
    }


    public BoardTile GetSelectedTile() { return selectedTile; }
    public Bug GetSelectedBug() { return selectedBug; }

    //ActionListener functions////////
    public void OnActionMoveStarted(Bug movedBug, Vector2 initialTilePos) 
    {

    }

    public void OnActionMoveFinished(Bug movedBug, Vector2 finalTilePos) { }

    public void OnActionAttackStarted(Bug attackerBug) { }
    public void OnActionMoveFinished(Bug attackerBug, Bug victimBug) { }

    public void OnActionDefense(Bug defendedBug) { }
    public void OnActionCancel() { }
    /////////////////////////
}
