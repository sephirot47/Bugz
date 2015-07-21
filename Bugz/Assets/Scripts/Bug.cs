using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    private int id;
    private int maxLife, life;
    private int attack;
    private int defense;
    private int movementRange;

	void Start () 
    {
        maxLife = life = UnityEngine.Random.Range(100, 1000);
        attack = UnityEngine.Random.Range(1, 5);
        defense = UnityEngine.Random.Range(1, 10);
        movementRange = UnityEngine.Random.Range(1, 3);
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

    public int GetId() { return id; }

    public int GetMaxLife() { return maxLife; }
    public int GetLife() { return life; }
    public int GetAttack() { return attack; }
    public int GetDefense() { return defense; }
    public int GetMovementRange() { return movementRange; }
}
