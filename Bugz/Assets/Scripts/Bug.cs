using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bug : MonoBehaviour
{
    private int id;
    private int maxLife, life;
    private int attack;
    private int defense;
    private int movementRange;

    [SerializeField]
    //Represents the normalized directions the bug can move to. For example: { (0,1), (1,0) } means he can only move upwards and right
    private List<Vector2> possibleDirections; 

	void Start () 
    {
        maxLife = life = UnityEngine.Random.Range(100, 1000);
        attack = UnityEngine.Random.Range(1, 5);
        defense = UnityEngine.Random.Range(1, 10);
        movementRange = UnityEngine.Random.Range(2, 4);
	}
	
	void Update () 
    {
	    
	}

    public void OnSelected()
    {

    }

    public void OnUnSelected()
    {

    }

    // Returns an array of Vector2, representing the relative displacement from the bug's tile to 
    // all the tiles the bug can possibly move to (without taking into account any collision with another bugs)
    // For example: a list like this: { (0,1), (0,2), (0,-1) },  means the bug can move upwards one or two positions [(0,1), (0,2)] and downwards just one position(0,-1)
    public List<Vector2> GetPossibleMoves()
    {
        List<Vector2> possibleMoves = new List<Vector2>();
        for(int i = 1; i <= movementRange; ++i)
        {
            foreach (Vector2 dir in possibleDirections) possibleMoves.Add(dir * i);
        }
        return possibleMoves;
    }

    public int GetId() { return id; }

    public int GetMaxLife() { return maxLife; }
    public int GetLife() { return life; }
    public int GetAttack() { return attack; }
    public int GetDefense() { return defense; }
    public int GetMovementRange() { return movementRange; }
}
