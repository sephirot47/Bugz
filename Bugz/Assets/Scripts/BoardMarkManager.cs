using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//This class has useful functions of marking the Board.
public class BoardMarkManager
{
    private Board board; //A reference to the game board, for easy access

    public BoardMarkManager(Board board)
    {
        this.board = board;
    }

    //Given an origin tilePos and a movementRange, marks the board(Good/Bad) with the tiles you can travel to from the origin with the given movementRange
    public void MarkMovementRangeOnTheBoard(Vector2 originTilePos, List<Vector2> possibleMoves)
    {
        foreach(Vector2 relativeDisplacement in possibleMoves)
        {
            BoardTile tile = board.GetTile(originTilePos + relativeDisplacement); //Extend on the horizontal axis
            MarkMovementRangeOnTile(tile); //Mark green if there's no obstacle, red otherwise
        }
    }

    //Updates a mark on the tile, marking it as Good if it's empty, as Bad otherwise 
    public void MarkMovementRangeOnTile(BoardTile tile)
    {
        if (tile != null) //If pos is not outside of the board, hence theres a tile
        {
            if (tile.IsEmpty()) tile.SetPermanentMarkType(BoardMark.MarkType.Good);
            else tile.SetPermanentMarkType(BoardMark.MarkType.Bad);
        }
    }

    //Clear all the board marks, i.e., set all the tileMarks to type None 
    public void ClearBoardMarks()
    {
        for(int x = 0; x < Board.WidthInTiles; ++x)
        {
            for(int y = 0; y < Board.HeightInTiles; ++y)
            {
                BoardTile tile = board.GetTile(new Vector2(x, y));
                tile.SetPermanentMarkType(BoardMark.MarkType.None);
            }
        }
    }

}
