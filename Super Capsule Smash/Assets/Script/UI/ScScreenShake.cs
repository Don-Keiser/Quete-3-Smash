using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScScreenShake : MonoBehaviour
{
    public static ScScreenShake Instance;
    private bool isShaking = false;
    private float elapsed;
    private float shakeSpeed = 45f;

    private float durationTime;
    private float magnitudeShake;

    private Vector3 originalPos = new Vector3(0, 0, -10);

    private void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (isShaking)
        {
            elapsed += Time.deltaTime;

            if (elapsed > durationTime)
            {
                isShaking = false;
                transform.localPosition = originalPos;
                elapsed = 0.0f;
            }
            else
            {
                float x = Mathf.PerlinNoise(Time.time * shakeSpeed, 0f);
                float y = Mathf.PerlinNoise(Time.time * shakeSpeed, 0.5f);
                Vector3 offset = new Vector3(x - magnitudeShake, y - magnitudeShake, originalPos.z);

                transform.position = offset /* amount*/;
            }
        }
    }
    public void Shake (float duration, float magnitude)
    {
        isShaking = true;
        durationTime = duration;
        magnitudeShake = magnitude;
        elapsed = 0.0f;

    }
}
