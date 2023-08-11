using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject music, musicTMP;
    private string off = "X", on = "0";

    public void Music()
    {
        if (music.GetComponent<AudioSource>().isPlaying)
            music.GetComponent<AudioSource>().Pause();
        else
            music.GetComponent<AudioSource>().Play();

        musicTMP.GetComponent<TextMeshProUGUI>().text = musicTMP.GetComponent<TextMeshProUGUI>().text == off ? on : off;
    }

    public void Play(string sceneName) => SceneManager.LoadScene(sceneName);
}