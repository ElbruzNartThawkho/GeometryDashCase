using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject music, musicTMP;
    private string off = "X", on = "0";
    
    /// <summary>
    /// ses tu�una bas�ld���nda ses a�ma kapama ve metnini ayarlar
    /// </summary>
    public void Music()
    {
        if (music.GetComponent<AudioSource>().isPlaying)
            music.GetComponent<AudioSource>().Pause();
        else
            music.GetComponent<AudioSource>().Play();

        musicTMP.GetComponent<TextMeshProUGUI>().text = musicTMP.GetComponent<TextMeshProUGUI>().text == off ? on : off;
    }
    /// <summary>
    /// play tu�una bas�ld���nda oyun sahnesine gider
    /// </summary>
    /// <param name="sceneName">gidilecek sahne ad�</param>
    public void Play(string sceneName) => SceneManager.LoadScene(sceneName);
}