using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineController : Singleton<CineController>
{
    public float shakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float shakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float shakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float m_shakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin m_virtualCameraNoise;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        base.Start();
        // Get Virtual Camera Noise Profile
        if (virtualCamera != null)
            m_virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        ShakeListener();
    }

    void ShakeListener()
    {
        // If the Cinemachine componet is not set, avoid update
        if (virtualCamera != null && m_virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (m_shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                m_virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                m_virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                // Update Shake Timer
                m_shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                m_virtualCameraNoise.m_AmplitudeGain = 0f;
                m_shakeElapsedTime = 0f;
            }
        }
    }

    public void ShakeTrigger()
    {
        m_shakeElapsedTime = shakeDuration;
    }
}
