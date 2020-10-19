using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1f)] public float volume = 0.7f;
    [Range(0.5f, 1.5f)] public float pitch = 1f;
    [Range(0, 0.5f)] public float volumeVariance = 0.1f;
    [Range(0, 0.5f)] public float pitchVariance = 0.1f;
    public bool loop = false;

    AudioSource audioSource;

    public void SetSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
        this.audioSource.clip = clip;
        this.audioSource.loop = loop;
    }
    
    public void Play()
    {
        audioSource.volume = volume * (1 + (Random.Range(-volumeVariance / 2f, volumeVariance / 2f)));
        audioSource.pitch = pitch * (1 + (Random.Range(-pitchVariance / 2f, pitchVariance / 2f)));
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundGo = new GameObject("Sound_" + i + "_" + sounds[i].name);
            soundGo.transform.SetParent(transform);
            sounds[i].SetSource(soundGo.AddComponent<AudioSource>());
        }

        PlaySound("Music");
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("Sound-" + name + " was not found in the array.");
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning("Sound-" + name + " was not found in the array.");
    }
}
