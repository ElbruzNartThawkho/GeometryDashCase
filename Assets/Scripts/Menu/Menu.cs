using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject music, musicTMP;
    private string off = "X", on = "0";
    
    /// <summary>
    /// ses tuþuna basýldýðýnda ses açma kapama ve metnini ayarlar
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
    /// play tuþuna basýldýðýnda oyun sahnesine gider
    /// </summary>
    /// <param name="sceneName">gidilecek sahne adý</param>
    public void Play(string sceneName) => SceneManager.LoadScene(sceneName);
}