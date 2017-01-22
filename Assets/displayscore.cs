using UnityEngine;
using System.Collections;

public class displayscore : MonoBehaviour {

    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start () {
        text.text = "You survived for exaclty " + Scoring.ElaspedTime.ToString() + " seconds";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
