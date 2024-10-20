using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioClipType {grabClip, shopClip }
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    public AudioClip grabClip, shopClip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void PlayAudio(AudioClipType clipType)
    {
        if (audioSource != null)
        {
            AudioClip audioClip = null;
            if (clipType == AudioClipType.grabClip)
            {
                audioClip = grabClip;
            }
            if (clipType == AudioClipType.shopClip)
            {
                audioClip = shopClip;
            }

            audioSource.PlayOneShot(audioClip, 0.3f);
        }
    }

    public void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }

    public void PlayBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Play();
    }
}
