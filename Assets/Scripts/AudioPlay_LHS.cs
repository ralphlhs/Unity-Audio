using UnityEngine;

public class AudioPlay_LHS : MonoBehaviour
{
    private AudioSource audioSourceBGM; 
    public AudioClip clip;
    void Start()
    {
        audioSourceBGM = GetComponent<AudioSource>();
        audioSourceBGM.clip = clip;
    }

    public void AudioPlay()
    {
        audioSourceBGM.Play();
    }

    public void AudioPause() 
    {
        audioSourceBGM.Pause();
    }
}
