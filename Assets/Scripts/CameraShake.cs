using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.3f;

    private Vector3 originalPosition;
    private float timeRemaining;


    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake()
    {
        originalPosition = transform.localPosition;
        timeRemaining = shakeDuration;
    }
}