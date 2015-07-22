using UnityEngine;
using System.Collections.Generic;

public class Bug2 : Bug
{
    public void Start()
    {
        base.Start();
        possibleMoves.Add(new Vector2(0, 1));
        possibleMoves.Add(new Vector2(1, 0));
        possibleMoves.Add(new Vector2(-1, 0));
        possibleMoves.Add(new Vector2(0, -1));

        canCrossBugs = false;
    }
}
