using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AlternatingAudioButton : MonoBehaviour
{
    public AudioClip firstClip;
    public AudioClip secondClip;

    private Button button;
    private AudioSource audioSource;
    private int clickCount = 0;

    void Start()
    {
        button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();

        button.onClick.AddListener(PlayAudio);
    }

    void PlayAudio()
    {
        if (clickCount % 2 == 0)
        {
            audioSource.clip = firstClip;
        }
        else
        {
            audioSource.clip = secondClip;
        }

        audioSource.Play();
        clickCount++;
    }
}
