using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Frontend");
    }

    public void PlayClip(string name)
    {
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == name);
        s.source.Play();
    }
}
