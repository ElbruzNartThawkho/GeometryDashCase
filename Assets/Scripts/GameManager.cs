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
    /// level bitti�inde biti� ekran�n� a�ar
    /// </summary>
    public void Finish()
    {
        finishScreen.SetActive(true);
    }
    /// <summary>
    /// b�l�m�n ba��na atar ve kontrol ayarlar�n� yapar
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
