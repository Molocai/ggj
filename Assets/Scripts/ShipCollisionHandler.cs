using UnityEngine;
using System.Collections;

public class ShipCollisionHandler : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacles")
        {
            Debug.Log("All hands on deck ! We hit an obstacle !");
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
