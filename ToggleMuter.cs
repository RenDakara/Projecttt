using UnityEngine;
using UnityEngine.Audio;

public class ToggleMuter : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    public delegate void MuteToggleHandler(bool isMuted);
    public event MuteToggleHandler OnMuteToggle;

    public bool isMuted { get; private set; } = false;

    public void ToggleSound()
    {
        const float MinDb = -80f;
        const float MaxDb = 0f;
        string mixName = "MasterVolume";

        isMuted = !isMuted;

        float targetDb;

        if (isMuted)
            targetDb = MinDb; 
        else
            targetDb = MaxDb; 

        _audioMixer.SetFloat(mixName, targetDb);
        OnMuteToggle?.Invoke(isMuted);
    }
}
