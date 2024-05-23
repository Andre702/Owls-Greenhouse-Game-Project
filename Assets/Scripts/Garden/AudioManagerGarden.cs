using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerGarden : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip water_barrel;
    public AudioClip shovel;
    public AudioClip planting;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
