using UnityEngine;
using System.Collections;

public class SettingMonsterShader : MonoBehaviour {

    public Material mat;
    public MonsterController monsterController;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetVector("_LighthouseLocation", transform.position);
        mat.SetVector("_LighthouseVector", transform.forward);

        if(monsterController != null)
        {
            monsterController.LighthouseLocation = transform.position;
            monsterController.LighthouseVector = transform.forward;
        }
        else
        {
            Debug.Log("Link monster controller for jump scare sounds pliz.");
        }
    }
}
