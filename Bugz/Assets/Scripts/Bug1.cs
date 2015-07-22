using UnityEngine;
using System.Collections.Generic;

public class Bug1 : Bug
{
    public void Start()
    {
        base.Start();
        possibleMoves.Add(new Vector2(1, 1));
        possibleMoves.Add(new Vector2(1, -1));
        possibleMoves.Add(new Vector2(-1, 1));
        possibleMoves.Add(new Vector2(-1, -1));
        possibleMoves.Add(new Vector2(2, 2));
        possibleMoves.Add(new Vector2(2, -2));
        possibleMoves.Add(new Vector2(-2, 2));
        possibleMoves.Add(new Vector2(-2, -2));

        canCrossBugs = false;
    }
}
