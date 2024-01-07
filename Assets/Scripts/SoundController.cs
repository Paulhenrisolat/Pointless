using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> sounds = new();
    [SerializeField]
    private List<AudioClip> musics = new();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string soundName)
    {
        foreach(AudioClip sound in sounds)
        {
            if(sound.name == soundName)
            {
                audioSource.PlayOneShot(sound);
            }
        }
    }
    public void PlayMusic(string musicName)
    {
        foreach (AudioClip music in musics)
        {
            if (music.name == musicName)
            {
                audioSource.Play();
            }
        }
    }
    public void ChangeVolume()
    {

    }
}
