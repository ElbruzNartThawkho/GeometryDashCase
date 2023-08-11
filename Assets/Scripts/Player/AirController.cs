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

    private void OnEnable()
    {
        flySprite.gameObject.SetActive(true);
        flyParticle.SetActive(true);
    }
    private void OnDisable()
    {
        flySprite.gameObject.SetActive(false);
        flyParticle.SetActive(false);
    }
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
    private void Fly()
    {
        if (playerInput.actions["Fly"].IsPressed())
        {
            rb.velocity = new Vector2(rb.velocity.x, flyForce);
        }
    }
    private void MoveForward()
    {
        Vector2 movement = new(flySpeed, rb.velocity.y);
        rb.velocity = movement;
    }
}
