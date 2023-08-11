using UnityEngine;
using UnityEngine.InputSystem;

public class AirController : MonoBehaviour
{
    [Header("Fly Settings")]
    public float flySpeed = 5f;
    public float flyForce = 10f;
    public float rotationSpeed = 5f;
    public float rotationClamp = 30;

    [Header("Visual Settings")]
    public Transform flySprite;
    public GameObject flyParticle;

    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Fly();
        UpdateRotation();
    }
    private void FixedUpdate()
    {
        MoveForward();
    }
    /// <summary>
    /// y�kselip d��me durumuna g�re d�nme rotasyonunu ak�c� olarak ayarlar
    /// </summary>
    private void UpdateRotation()
    {
        float targetRotation = 0f;

        if (rb.velocity.y > 0)
        {
            targetRotation = Mathf.Lerp(0, rotationClamp, rb.velocity.y / 10);
        }
        else if (rb.velocity.y < 0)
        {
            targetRotation = Mathf.Lerp(0, -rotationClamp, -rb.velocity.y / 10);
        }

        Quaternion targetQuaternion = Quaternion.Euler(0, 0, targetRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSpeed);
    }
    /// <summary>
    /// fly tu�una bas�l� tutuldu�unda yukar� do�ru u�mas�n� sa�lar
    /// </summary>
    private void Fly()
    {
        if (playerInput.actions["Fly"].IsPressed())
        {
            rb.velocity = new Vector2(rb.velocity.x, flyForce);
        }
    }
    /// <summary>
    /// flySpeed de�erine g�re ileri do�ru hareket eder
    /// </summary>
    private void MoveForward()
    {
        rb.velocity = new(flySpeed, rb.velocity.y);
    }
    /// <summary>
    /// bile�en etkinle�tirildi�inde �al���r
    /// sprite ve particlelar�n� aktif yapar
    /// </summary>
    private void OnEnable()
    {
        flySprite.gameObject.SetActive(true);
        flyParticle.SetActive(true);
    }
    /// <summary>
    /// bile�en devre d��� b�rak�ld���nda �al���r
    /// sprite ve particlelar�n aktifli�ini kapat�r
    /// </summary>
    private void OnDisable()
    {
        flySprite.gameObject.SetActive(false);
        flyParticle.SetActive(false);
    }
}
