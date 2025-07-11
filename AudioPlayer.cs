using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] soundEffects; // Массив звуков
    [SerializeField] private Button[] playButtons; // Массив кнопок

    private ToggleMuter toggleMuter;

    private void Start()
    {
        toggleMuter = GetComponent<ToggleMuter>();

        for (int i = 0; i < playButtons.Length; i++)
        {
            int index = i; 
            if (playButtons[i] != null)
            {
                playButtons[i].onClick.AddListener(() => PlaySound(index));
            }
        }

        if (toggleMuter != null)
            toggleMuter.OnMuteToggle += HandleMuteToggle;
    }

    private void PlaySound(int index)
    {
        if (soundEffects == null || toggleMuter == null)
            return;

        if (index < 0 || index >= soundEffects.Length)
            return;

        if (!toggleMuter.isMuted)
        {
            foreach (var sound in soundEffects)
            {
                sound.Stop();
            }

            soundEffects[index].Stop();
            soundEffects[index].Play();
        }
    }

    private void HandleMuteToggle(bool isMuted)
    {
        if (soundEffects != null)
        {
            foreach (var sound in soundEffects)
            {
                if (isMuted)
                    sound.Pause();
                else if (!sound.isPlaying)
                    sound.UnPause();
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < playButtons.Length; i++)
        {
            int index = i;

            if (playButtons[i] != null)
                playButtons[i].onClick.RemoveListener(() => PlaySound(index));
        }

        if (toggleMuter != null)
            toggleMuter.OnMuteToggle -= HandleMuteToggle;
    }
}
