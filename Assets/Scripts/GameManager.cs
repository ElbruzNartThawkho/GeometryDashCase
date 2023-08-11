using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform startPosition;
    public GameObject player;
    public GameObject finishScreen;
    public float reloadSceneTime;

    private void Awake()
    {
        instance = this;
    }
    /// <summary>
    /// level bittiðinde bitiþ ekranýný açar
    /// </summary>
    public void Finish()
    {
        finishScreen.SetActive(true);
    }
    /// <summary>
    /// bölümün baþýna atar ve kontrol ayarlarýný yapar
    /// </summary>
    /// <param name="landOrAir">kara kontrolleri varsa true hava kontrolleri varsa false</param>
    public void RetryLvl(bool landOrAir)
    {
        player.GetComponent<LandController>().enabled = landOrAir;
        player.GetComponent<AirController>().enabled = !landOrAir;
        Invoke(nameof(ReloadCoroutine), reloadSceneTime);
    }

    private void ReloadCoroutine()
    {
        player.transform.position = startPosition.position;
        player.transform.rotation = startPosition.rotation;
        player.SetActive(true);
    }
}
