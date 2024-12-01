using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicClips; 
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); 
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.buildIndex >= 0 && scene.buildIndex <= 2)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = musicClips[0]; 
                audioSource.Play();
            }
        }
        else if(scene.buildIndex >= 3 && scene.buildIndex <= 7)
        {
            audioSource.Stop();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
