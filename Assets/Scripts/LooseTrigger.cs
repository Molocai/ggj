using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class LooseTrigger : MonoBehaviour {

    private Rigidbody _rigidbody;
    public float elapsedTime = 0.0f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("LoseScene");
            Scoring.ElaspedTime = elapsedTime;
        }
    }

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {
        elapsedTime += Time.deltaTime;
    }
}
