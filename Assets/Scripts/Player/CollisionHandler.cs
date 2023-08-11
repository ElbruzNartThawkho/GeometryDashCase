using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [Header("Tag Settings")]
    public string groundTag;
    public string obstacleTag;
    public string stateChangeTag;
    public string finishTag;

    [Header("Visual Settings")]
    public GameObject dyingParticle;

    private AirController airPlayer;
    private LandController landPlayer;

    private void Awake()
    {
        airPlayer = GetComponent<AirController>();
        landPlayer = GetComponent<LandController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            Instantiate(dyingParticle, transform.position, dyingParticle.transform.rotation);
            gameObject.SetActive(false);
            GameManager.instance.ReloadScene(true);
        }
        if (collision.gameObject.CompareTag(groundTag))
        {
            landPlayer.ChangeInGroundState(true);
            landPlayer.particle.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            if (landPlayer.enabled == false)
            {
                landPlayer.particle.SetActive(false);
            }
            else
            {
                landPlayer.Coyote();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(finishTag))
        {
            GameManager.instance.Finish();
            Time.timeScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stateChangeTag))
        {
            if (landPlayer.enabled == false)
            {
                landPlayer.enabled = true;
                airPlayer.enabled = false;
            }
            else
            {
                landPlayer.enabled = false;
                airPlayer.enabled = true;
                landPlayer.sprite.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
