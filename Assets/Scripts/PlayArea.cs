using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {        
        MonsterController controller = other.GetComponent<MonsterController>();
        if ( controller != null)
        {
            controller.LeftPlayArea();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
