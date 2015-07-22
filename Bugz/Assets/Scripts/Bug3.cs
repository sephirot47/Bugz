using UnityEngine;
using System.Collections.Generic;

public class Bug3 : Bug
{
    public void Start()
    {
        base.Start();
        possibleMoves.Add(new Vector2(0, 1));
        possibleMoves.Add(new Vector2(0, 2));
        possibleMoves.Add(new Vector2(0, 3));
        possibleMoves.Add(new Vector2(1, 0));
        possibleMoves.Add(new Vector2(2, 0));
        possibleMoves.Add(new Vector2(-1, 0));
        possibleMoves.Add(new Vector2(-2, 0));

        canCrossBugs = false;
    }
}
