using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;

    public AudioClip hologramClip;
    public AudioClip letterClip;
    public AudioClip chestClip;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayHologram()
    {
        audioSource.PlayOneShot(hologramClip);
    }

    public void PlayLetter()
    {
        audioSource.PlayOneShot(letterClip);
    }

    public void PlayChest()
    {
        audioSource.PlayOneShot(chestClip);
    }
}