using UnityEngine;
using System.Collections;

public class RandomClipPlayer : MonoBehaviour {

    public AudioClip[] audioclips;
    public float MinDistance = 100f;
    public float PitchVariation = 0.25f;
    public float Volume = 1.0f;
    public float BasePitch = 1f;    

    public int previousPick = 0;
    private AudioSource _audiosource;

    public void Play()
    {
        if (audioclips.Length <= 0)
        {
            Debug.Log("No clips found !");
            return;
        }
        else if (audioclips.Length == 1)
        {
            _audiosource.pitch = 1f + Random.Range(-PitchVariation * 0.5f, PitchVariation * 0.5f);
            _audiosource.PlayOneShot(audioclips[0]);
            return;
        }

        int pick = 0;
        do
        {
            pick = Random.Range(0, audioclips.Length);
        } while (previousPick == pick);

        previousPick = pick;
        _audiosource.pitch = 1f + Random.Range(-PitchVariation * 0.5f, PitchVariation * 0.5f);
        _audiosource.PlayOneShot(audioclips[pick]);
    }

    // Use this for initialization
    void Start () {
        _audiosource = gameObject.AddComponent<AudioSource>();
        _audiosource.volume = Volume;
        _audiosource.pitch = BasePitch;
        _audiosource.minDistance = MinDistance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
