using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioClip winClip;
    [SerializeField] AudioClip looseClip;
    [SerializeField] AudioClip miscClip;
    [SerializeField] AudioClip actionClip;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Awake()
    {
        instance = this;
    }

    public void PlayWinClip()
    {
        PlaySound(winClip);
    }
    public void PlayLoseClip()
    {
        PlaySound(looseClip);
    }

    public void PlayActionClip()
    {
        PlaySound(actionClip);
    }



    public void PlayMiscClip()
    {
        // Start a coroutine to wait for the current clip to finish
        StartCoroutine(PlayMiscAfterCurrentClip());
    }

    private IEnumerator PlayMiscAfterCurrentClip()
    {
        // Wait until the current clip has finished playing
        while (audioSource.isPlaying)
        {
            yield return null;  // Wait for the next frame
        }

        // Once the current clip has finished, play the misc clip
        audioSource.loop = true;  // Set the loop if needed
        PlaySound(miscClip);      // Play the miscClip
    }


    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip or AudioSource is missing!");
        }
    }
}
