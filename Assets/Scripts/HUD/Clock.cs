using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Clock : MonoBehaviour
{
    public float dayLength;
    public float currentTime;

    public GameObject needle;
    public Light2D globalLight;

    public UnityAction hoursListeners;
    private float _ticks;

    void Start()
    {
        currentTime = 0;
        UpdateClock();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        _ticks += Time.deltaTime;
        UpdateClock();
        if (_ticks > dayLength / 24f)
        {
            _ticks = _ticks % (dayLength / 24f);
            hoursListeners.Invoke();
        }
    }

    void UpdateClock()
    {
        currentTime = currentTime % dayLength;
        float rotation = 90 - 360*currentTime/dayLength;
        needle.transform.localRotation = Quaternion.Euler(0, 0, rotation);
        globalLight.intensity = LightIntensity();

    }

    float LightIntensity()
    {
        if (currentTime < 0.1 * dayLength)
        {
            float thresh2 = 0.1f * dayLength;
            float ratio =  currentTime / (thresh2);
            return 0.1f + 0.9f * ratio;
        }
        if (currentTime < 0.7 * dayLength) return 1.0f;
        if (currentTime < 0.8 * dayLength)
        {
            float thresh1 = 0.7f * dayLength;
            float thresh2 = 0.8f * dayLength;
            float ratio = (thresh2 - currentTime) / (thresh2 - thresh1);
            return 0.1f + 0.9f*ratio;
        }
        return 0.1f;
    }
}
