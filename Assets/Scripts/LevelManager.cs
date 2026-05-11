using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayClip("MusicGame");
        AudioManager.instance.StopClip("MenuMusic");
    }
}
