﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipController))]
public class ShipAudioComponent : MonoBehaviour {

    private AudioSource _engineAudioSource;
    private ShipController _shipController;
    public float MinDistance = 100f;
    public float EnginePitchFactor = 0.25f;
    public float EngineVolume = 1.0f;
    public float EngineBasePitch = 0.5f;
    public AudioClip EngineAudioClip;

    public RandomClipPlayer splashRandomPlayer;
    public bool HasSplashed = false;

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
        _engineAudioSource.pitch = EngineBasePitch + _shipController.GetSpeedPercent() * EnginePitchFactor;

        if(!HasSplashed && transform.rotation.eulerAngles.x < 30f )
        {
            HasSplashed = true;
            if(splashRandomPlayer != null)
            {
                splashRandomPlayer.Play();
            }
        }
        else if(HasSplashed && transform.rotation.eulerAngles.x > 300.0f)
        {
            HasSplashed = false;
        }

    }
}

