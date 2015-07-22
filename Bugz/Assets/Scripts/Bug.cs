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

    //Represents the possible movements the bug can do. For example: { (0,1), (1,0) } means he can only move upwards one tile away, and right one tile away
    protected List<Vector2> possibleMoves;
    protected bool canCrossBugs = false; //Can the bug go through another bug's tile when moving ????

	public void Start () 
    {
        maxLife = life = UnityEngine.Random.Range(100, 1000);
        attack = UnityEngine.Random.Range(1, 5);
        defense = UnityEngine.Random.Range(1, 10);
        movementRange = UnityEngine.Random.Range(2, 4);

        possibleMoves = new List<Vector2>();
	}

    public void Update() 
    {
	    
	}

    public void OnSelected()
    {

    }

    public void OnUnSelected()
    {

    }

    public void MoveTo(Vector3 worldPos)
    {
        transform.position = worldPos;
    }

    // Returns an array of Vector2, representing the relative displacement from the bug's tile to 
    // all the tiles the bug can possibly move to (without taking into account any collision with another bugs)
    // For example: a list like this: { (0,1), (0,2), (0,-1) },  means the bug can move upwards one or two positions [(0,1), (0,2)] and downwards just one position(0,-1)
    public List<Vector2> GetPossibleMoves()
    {
        return possibleMoves;
    }

    public int GetId() { return id; }

    public int GetMaxLife() { return maxLife; }
    public int GetLife() { return life; }
    public int GetAttack() { return attack; }
    public int GetDefense() { return defense; }
    public int GetMovementRange() { return movementRange; }
}
