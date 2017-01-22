using UnityEngine;
using System.Collections;

public class Scoring2 : MonoBehaviour {

    public float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
        elapsedTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
    }
}
