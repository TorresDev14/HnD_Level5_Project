using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    public AudioSource soundEffect;

    private void Awake()
    {
        soundEffect.Play();
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Endgame ()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
