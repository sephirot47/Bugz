using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoardCanvasManager : MonoBehaviour, ITileListener
{
    private static readonly string None = "-";

    [SerializeField]
    private Text lifeText, attackText, defenseText;

    [SerializeField]
    private Button buttonMove, buttonAttack, buttonDefense, buttonConfirm, buttonCancel;

	void Start ()
    {
        GoToMainActionMenu();
        HideAllButtons();
        PopulateStatsTexts(null);
	}
	
	void Update ()
    {
	}

    public void OnTileSelected(BoardTile tile)
    {
        if(!tile.IsEmpty())
        {
            GoToMainActionMenu(); //Change the menu to Move, Attack, Defense, etc

            Bug selectedBug = tile.GetBug();
            PopulateStatsTexts(selectedBug); //Populate the texts on the StatsPanel
        }
        else
        {
            HideAllButtons();
        }
    }

    public void OnTileUnselected(BoardTile tile)
    {
        HideAllButtons();
    }
    
    public void GoToMainActionMenu()
    {
        HideAllButtons();
        buttonMove.gameObject.SetActive(true);
        buttonAttack.gameObject.SetActive(true);
        buttonDefense.gameObject.SetActive(true);
    }

    public void OnButtonMovePressed()
    {

    }

    public void OnButtonAttackPressed()
    {

    }

    public void OnButtonDefensePressed()
    {

    }

    public void OnButtonCancelPressed()
    {
        GoToMainActionMenu();
    }

    public void OnButtonConfirmPressed()
    {
        GoToMainActionMenu();
    }


    public void HideAllButtons()
    {
        buttonMove.gameObject.SetActive(false);
        buttonAttack.gameObject.SetActive(false);
        buttonDefense.gameObject.SetActive(false);
        buttonConfirm.gameObject.SetActive(false);
        buttonCancel.gameObject.SetActive(false);
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
