using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.PlayClip("MainMenu");
        AudioManager.instance.StopClip("MusicGame");
    }
}
