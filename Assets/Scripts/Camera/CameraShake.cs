using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour {

    private CinemachineImpulseSource impulseSource;

    private void Awake() {
        impulseSource = GetComponentInChildren<CinemachineImpulseSource>();
    }

    public void ShakeCameraCinemachine(float duration, float amplitude, float frequency) {

        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = amplitude;
        impulseSource.m_ImpulseDefinition.m_FrequencyGain = frequency;
        impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = duration;

        impulseSource.GenerateImpulse();

    }

}
