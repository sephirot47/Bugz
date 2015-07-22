using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BoardCanvasManager : MonoBehaviour
{
    private static readonly string None = "-";

    [SerializeField]
    private Text lifeText, attackText, defenseText;

    [SerializeField]
    private Button buttonMove, buttonAttack, buttonDefense, buttonConfirm, buttonCancel;

    [SerializeField]
    private BoardGameController gameController;

	void Start ()
    {
        GoToMainActionMenu();
        HideAllButtons();
        PopulateStatsTexts(null);
	}
	
	void Update ()
    {
        BoardGameController.BoardGameState gameState = gameController.GetGameState();
        if(gameState == BoardGameController.BoardGameState.Pointing)
        {
            Bug selectedBug = gameController.GetSelectedBug();
            if (selectedBug == null) HideAllButtons();
            else GoToMainActionMenu(); //There's a selectedBug, show the actions it can perform
        }
        else if (gameState == BoardGameController.BoardGameState.MovingBug)
        {
            GoToMoveActionMenu();
        }
        else if (gameState == BoardGameController.BoardGameState.MovingBug)
        {
            GoToAttackActionMenu();
        }
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

    public void OnTileUnSelected(BoardTile tile)
    {
        HideAllButtons();
    }

    public void OnButtonMovePressed()
    {
        gameController.OnActionMoveStarted();
    }

    public void OnButtonAttackPressed()
    {
        gameController.OnActionAttackStarted();
    }

    public void OnButtonDefensePressed()
    {
        gameController.OnActionDefense();
    }

    public void OnButtonCancelPressed()
    {
        gameController.OnActionCancel();
    }

    public void OnButtonConfirmPressed()
    {
        BoardGameController.BoardGameState gameState = gameController.GetGameState();
        if (gameState == BoardGameController.BoardGameState.MovingBug)
        {
            gameController.OnActionMoveFinished();
        }
        else if (gameState == BoardGameController.BoardGameState.AttackingWithBug)
        {
            gameController.OnActionAttackFinished();
        }
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

    //NAVIGATION FUNCTIONS /////////////////////////////
    public void GoToMainActionMenu()
    {
        HideAllButtons();
        buttonMove.gameObject.SetActive(true);
        buttonAttack.gameObject.SetActive(true);
        buttonDefense.gameObject.SetActive(true);
    }

    public void GoToMoveActionMenu()
    {
        HideAllButtons();
        buttonConfirm.gameObject.SetActive(true);
        buttonCancel.gameObject.SetActive(true);
    }

    public void GoToAttackActionMenu()
    {
        HideAllButtons();
        buttonConfirm.gameObject.SetActive(true);
        buttonCancel.gameObject.SetActive(true);
    }


    public void HideAllButtons()
    {
        buttonMove.gameObject.SetActive(false);
        buttonAttack.gameObject.SetActive(false);
        buttonDefense.gameObject.SetActive(false);
        buttonConfirm.gameObject.SetActive(false);
        buttonCancel.gameObject.SetActive(false);
    }
    ////////////////////////////////////////////////////
}
