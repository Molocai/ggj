using UnityEngine;
using System.Collections;

public class SettingMonsterShader : MonoBehaviour {

    public Material mat;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        mat.SetVector("_LighthouseLocation", transform.position);
        mat.SetVector("_LighthouseVector", transform.forward);
    }
}
