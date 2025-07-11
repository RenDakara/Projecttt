using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string parameterName;

    private const float MinDb = -80f;
    private const float MaxDb = 0f;

    private void Start()
    {
        if (audioMixer.GetFloat(parameterName, out float currentVolume))
        {
            volumeSlider.value = DbToNormalized(currentVolume);
        }

        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        float dB;

        if (value <= 0.0001f)
        {
            dB = MinDb;
        }
        else
        {
            dB = Mathf.Log10(value) * 20f;
            dB = Mathf.Clamp(dB, MinDb, MaxDb);
        }

        audioMixer.SetFloat(parameterName, dB);
    }

    private float DbToNormalized(float dB)
    {
        if (dB <= MinDb + 0.01f)
            return 0f;

        return Mathf.Pow(10f, dB / 20f);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}