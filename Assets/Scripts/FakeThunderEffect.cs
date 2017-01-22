using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeThunderEffect : MonoBehaviour
{
    public AnimationCurve LightIntensity;

    private bool Boom = false;
    private float Progress = 0f;
    private float NextLighting = 3.5f;

    public RandomClipPlayer _randomClipPlayer;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Boom)
        {
            Progress += Time.deltaTime;
            GetComponent<Light>().intensity = LightIntensity.Evaluate(Progress);

            if (Progress >= 1)
            {
                Progress = 0;
                Boom = false;
            }
        }

        else
        {
            if (Time.time >= NextLighting)
            {
                Boom = true;
                if(_randomClipPlayer != null)
                    _randomClipPlayer.Play();

                transform.localPosition = new Vector3(Random.Range(-6, 6), transform.localPosition.y, transform.localPosition.z);
                NextLighting = Time.time + Random.Range(10, 25);
            }
        }
    }
}
