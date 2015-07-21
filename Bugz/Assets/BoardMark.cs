using UnityEngine;
using System.Collections;

public class BoardMark : MonoBehaviour
{
    public enum MarkType
    {
        Good, Bad, None
    }

    [SerializeField]
    private Material materialGood;
    [SerializeField]
    private Material materialBad;
    private MarkType type = MarkType.None;

	void Start () 
    {
        ChangeType(type);
	}
	
	void Update () {
	}

    public void ChangeType(MarkType type)
    {
        this.type = type;

        gameObject.SetActive(true);
        if (type == MarkType.Good) GetComponent<MeshRenderer>().material = materialGood;
        else if (type == MarkType.Bad) GetComponent<MeshRenderer>().material = materialBad;
        else if (type == MarkType.None) gameObject.SetActive(false);
    }

    public MarkType GetType() { return type; }
}
