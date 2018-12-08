using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void playButtonPressSound()
    {
        audioSource.Play(0);
    }
}
