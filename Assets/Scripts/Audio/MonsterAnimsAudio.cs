using UnityEngine;
using System.Collections;

public class MonsterAnimsAudio : MonoBehaviour {

    public RandomClipPlayer SwimSounds;
    public RandomClipPlayer AttackSounds;

    public void PlaySwimSound()
    {
        if (SwimSounds != null)
            SwimSounds.Play();
    }

    public void PlayAttackSound()
    {
        if (AttackSounds != null)
            AttackSounds.Play();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
