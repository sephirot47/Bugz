using UnityEngine;
using System.Collections.Generic;

public class Bug0 : Bug
{
    public void Start()
    {
        base.Start();
        possibleMoves.Add(new Vector2(0, 1));
        possibleMoves.Add(new Vector2(0, 2));
        possibleMoves.Add(new Vector2(0, 3));
        possibleMoves.Add(new Vector2(1, 3));

        canCrossBugs = true;
    }
}
