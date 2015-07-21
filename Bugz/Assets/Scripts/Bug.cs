using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    private int id;
    private int maxLife, life;
    private int attack;
    private int defense;

	void Start () 
    {
        maxLife = life = UnityEngine.Random.Range(100, 1000);
        attack = UnityEngine.Random.Range(1, 5);
        defense = UnityEngine.Random.Range(1, 10);
	}
	
	void Update () 
    {
	
	}

    public int GetId() { return id; }

    public int GetMaxLife() { return maxLife; }
    public int GetLife() { return life; }
    public int GetAttack() { return attack; }
    public int GetDefense() { return defense; }
}
