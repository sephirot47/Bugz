using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoardCanvasManager : MonoBehaviour
{
    private static readonly string None = "-";

    [SerializeField]
    private Text lifeText, attackText, defenseText;

    [SerializeField]
    private Button buttonMove, buttonAttack, buttonDefense, buttonConfirm, buttonCancel;

	void Start () 
    {
        PopulateStatsTexts(null);
	}
	
	void Update () {
	
	}

    //Populate the stats texts with the stats of the bug passed as parameter
    public void PopulateStatsTexts(Bug bug)
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

    private void GoToActionsMain()
    {

    }
}
