using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipController))]
public class ShipAudioComponent : MonoBehaviour {

    private AudioSource _engineAudioSource;
    private ShipController _shipController;
    public float MinDistance = 100f;
    public float EnginePitchFactor = 0.5f;
    public float EngineVolume = 1.0f;
    public AudioClip EngineAudioClip;

	// Use this for initialization
	void Start () {
        _engineAudioSource = gameObject.AddComponent<AudioSource>();
        _engineAudioSource.volume = EngineVolume;
        _engineAudioSource.clip = EngineAudioClip;
        _engineAudioSource.minDistance = MinDistance;
        _engineAudioSource.loop = true;
        _engineAudioSource.Play();
        

        _shipController = GetComponent<ShipController>();
    }
	
	// Update is called once per frame
	void Update () {
        _engineAudioSource.pitch = 1 + _shipController.GetSpeedPercent() * EnginePitchFactor;
	}
}

