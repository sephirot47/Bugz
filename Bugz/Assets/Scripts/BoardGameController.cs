using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//This is the class responsible for managing the logic of the board game itself
public class BoardGameController : MonoBehaviour
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
    private BoardTile selectedTile = null; //Holds the currently selected Tile
    private Bug selectedBug = null;        //Holds the currently selected Bug
    private Bug actionBug = null;        //Holds the bug performing an action

    private BoardGameState currentGameState;

    public void Start()
    {
        currentGameState = BoardGameState.Pointing;
    }

    public void Update()
    {
    }


    //Called when the tile 'tile' is selected. This method spreads the event to all the ITileListeners
    public void OnTileSelected(BoardTile tile)
    {
        if (selectedTile == tile) //When clicking on a selected tile, unselect it
        {
            selectedTile.OnUnSelected();
            selectedTile = null;

            //Spread the event
            List<ITileListener> tileListeners = Utils.GetAll<ITileListener>();
            foreach (ITileListener tileListener in tileListeners) tileListener.OnTileUnselected(tile);
        }
        else //Tile selected for the first time
        {
            if (selectedTile != null) selectedTile.OnUnSelected(); //Tell the last selected tile that its not selected anymore

            selectedTile = tile;
            selectedTile.OnSelected(); //Tell the selected tile that its been selected
            selectedBug = selectedTile.GetBug();

            if (selectedBug != null) //If the tile isnt empty, do something with the bug
            {
                if(currentGameState == BoardGameState.Pointing) {  /*Just mark the selected tile, i.e., do nothing here*/ }
                else if(currentGameState == BoardGameState.MovingBug)
                {
                    //When a tile is selected and the player is moving
                }
            }

            //Spread the event
            List<ITileListener> tileListeners = Utils.GetAll<ITileListener>();
            foreach (ITileListener tileListener in tileListeners) tileListener.OnTileSelected(tile);
        }
    }



    //ACTION EVENT LISTENERS ////////////////////////////////////////////////////////////////////////////////
    public void OnActionMoveStarted() 
    {
        //Mark the board with the tiles the selected bug can move(travel) to
        currentGameState = BoardGameState.MovingBug;
        actionBug = selectedBug;
        int movementRange = actionBug.GetMovementRange();

        Vector2 selectedTilePos = selectedTile.GetTilePos();
        List<Vector2> bugPossibleMoves = actionBug.GetPossibleMoves(); //Get the possible moves of the bug
        board.GetMarkManager().MarkMovementRangeOnTheBoard(selectedTilePos, bugPossibleMoves); //Mark where the bug can go
    }

    public void OnActionMoveFinished()
    {
        //User has decided to confirm the movement. We must check if the tile he is selecting is valid for movement or not
        if(selectedTile.GetPermanentMarkType() == BoardMark.MarkType.Good) //We can check if the mark is Good or not :) 
        {
            currentGameState = BoardGameState.Pointing;

            //Move the bug!
            BoardTile fromTile = board.GetBugTile( actionBug ); //Get the tile where the bug is before moving
            fromTile.SetBug(null); //There isnt a bug anymore in you

            Vector3 worldPos = board.GetWorldPos( selectedTile.GetTilePos() );
            actionBug.MoveTo(worldPos); //Move the bug itself

            selectedTile.SetBug( actionBug ); //You have a new bug on you yay
            actionBug = null; //The action finished, so we don't need to track the action with the actionBug anymore

            //Unselect the tile and all stuff
            ResetEverything();
        }
        else //Trying to move to a tile where another bug is in
        {
            //Warn the user somehow, ftm a log
            Debug.Log("Moving to a bad tile, try to move to a different location");
        }
    }

    public void OnActionAttackStarted()
    {
        currentGameState = BoardGameState.AttackingWithBug;
    }

    public void OnActionAttackFinished()
    {
        currentGameState = BoardGameState.AttackingWithBug;
    }

    public void OnActionDefense()
    {
        currentGameState = BoardGameState.Pointing;
    }

    public void OnActionCancel()
    {
        ResetEverything();
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////

    // UTILS ////////////////////////////////////////////////////////////
    public void ResetEverything()
    {
        if (selectedTile != null) selectedTile.OnUnSelected();
        if (selectedBug != null) selectedBug.OnUnSelected();

        selectedBug = null;
        selectedTile = null;
        actionBug = null;

        board.GetMarkManager().ClearBoardMarks();

        currentGameState = BoardGameState.Pointing;
    }
    /////////////////////////////////////////////////////////////////////

    //GETTERS //////////////////////////////////////////////////////////
    public BoardTile GetSelectedTile() { return selectedTile; }
    public Bug GetSelectedBug() { return selectedBug; }
    public BoardGameState GetGameState() { return currentGameState; }
    /////////////////////////////////////////////////////////////////////
}
