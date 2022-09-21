using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Codigo de video de CodeMonkey en youtube
//link:https://www.youtube.com/watch?v=ACf1I27I6Tk&t=21s&ab_channel=CodeMonkey

namespace Plataformer2d 
{ 
    public class CameraShake : MonoBehaviour
    {   
        public static CameraShake Instance { get; private set; }
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private float shakeTimer;
        private float shakeTimerTotal;
        private float startingIntensity;
        private void Awake()
        {
            Instance = this;
            cinemachineVirtualCamera=GetComponent<CinemachineVirtualCamera>();
            CameraShake.Instance.ShakeCamera(0f, 0f);//evito que la camara empieze vibrando. con esto le meto la vibracion y duracion en 0
        }
        public void ShakeCamera(float intensity,float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin= cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            startingIntensity = intensity;
            shakeTimerTotal = time;
            shakeTimer = time;

        }
        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                    //timer over
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
                
            }
        }
    }
}