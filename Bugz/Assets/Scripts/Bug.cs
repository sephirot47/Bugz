using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour
{
    private int id;
    private float maxLife, life;
    private float attack;
    private float defense;

	void Start () 
    {
	    
	}
	
	void Update () 
    {
	
	}

    public int GetId() { return id; }

    public float GetMaxLife() { return maxLife; }
    public float GetLife() { return life; }
    public float GetAttack() { return attack; }
    public float GetDefense() { return defense; }
}
