using UnityEngine;

public class PlayerPreference : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create}

    public static int hiScoreAmount = 0;

    public void Start()
    {

        // Before reading the key, check to see if a value has been stored in it.
        if (PlayerPrefs.HasKey("hiScore") == true)
        {
            // the key musicVol holds a value, therefore we can
            //retrieve it and store it in a variable
            hiScoreAmount = PlayerPrefs.GetInt("hiScore");
        }
        else
        {
            // the key musicVol is null so give it a default value of 0.5f
            PlayerPrefs.SetInt("hiScore", 1000);
        }

        /*
        if (PlayerPrefs.HasKey("musicVolume") == true)
        {
            AudioManager.instance.musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 0.5f);
        }
        */
    }
}
