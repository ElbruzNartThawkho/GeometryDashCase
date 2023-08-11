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
    public void Finish()
    {
        finishScreen.SetActive(true);
    }
    public void ReloadScene(bool landOrAir)
    {
        player.GetComponent<LandController>().enabled = landOrAir;
        player.GetComponent<AirController>().enabled = !landOrAir;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(reloadSceneTime);
        player.transform.position = startPosition.position;
        player.transform.rotation = startPosition.rotation;
        player.SetActive(true);
    }
}
