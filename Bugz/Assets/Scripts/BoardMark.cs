using UnityEngine;
using System.Collections;

public class BoardMark : MonoBehaviour
{
    public enum MarkType //An enum for the different mark types
    {
        Good,       //Able to do somewhat in this tile (Green)
        Bad,        //Unable to do somewhat in this tile (?) (Red)
        Selection,  //When selected (Yellow)
        None        //Invisible mark. Board tile itself
    }


    //Different materials set in the editor, for the different mark types
    [SerializeField]
    private Material materialGood, materialBad, materialSelection;
    //


    //Current mark type. By default, marks are of type None (invisible)
    private MarkType type = MarkType.None;

	void Start () 
    {
        SetType(type); //Start with the None type
	}
	
	void Update () {
	}

    //Sets the mark type
    public void SetType(MarkType type)
    {
        this.type = type;

        gameObject.SetActive(true);
        if (type == MarkType.Good) GetComponent<MeshRenderer>().material = materialGood;
        else if (type == MarkType.Bad) GetComponent<MeshRenderer>().material = materialBad;
        else if (type == MarkType.Selection) GetComponent<MeshRenderer>().material = materialSelection;
        else if (type == MarkType.None) gameObject.SetActive(false);
    }

    public MarkType GetType() { return type; } //Returns the mark type
}
