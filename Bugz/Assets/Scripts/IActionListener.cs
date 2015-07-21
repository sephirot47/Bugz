using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

interface IActionListener
{
    void OnActionMoveStarted(Bug movedBug, Vector2 initialTilePos); 
    void OnActionMoveFinished(Bug movedBug, Vector2 finalTilePos);

    void OnActionAttackStarted(Bug attackerBug);                   
    void OnActionMoveFinished(Bug attackerBug, Bug victimBug);

    void OnActionDefense(Bug defendedBug);
    void OnActionCancel();
}