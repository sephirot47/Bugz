using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoardStatsPanel : MonoBehaviour
{
    private static readonly string None = "-";

    [SerializeField]
    private Text lifeText, attackText, defenseText;

	void Start () 
    {
        PopulateTexts(null);
	}
	
	void Update () {
	
	}

    public void PopulateTexts(Bug bug)
    {
        if(bug)
        {
            lifeText.text = bug.GetLife() + " / " + bug.GetMaxLife();
            attackText.text = bug.GetAttack().ToString();
            defenseText.text = bug.GetDefense().ToString();
        }
        else
        {
            lifeText.text = None;
            attackText.text = None;
            defenseText.text = None;
        }
    }
}
