using UnityEngine;
using UnityEngine.InputSystem;

public class LandController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 7f;
    public float coyoteTime = 0.2f;

    [Header("Visual Settings")]
    public Transform sprite;
    public GameObject particle;
    public GameObject dyingParticle;

    private Rigidbody2D rb;
    private PlayerInput playerInput;

    private Quaternion targetRotation;
    private bool isRotating = false;

    private bool grounded = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        MoveForward();
        RotateControl();
    }
    private void RotateControl()
    {
        if (isRotating)
        {
            sprite.rotation = Quaternion.Slerp(sprite.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            if (Quaternion.Angle(sprite.rotation, targetRotation) < 1)
            {
                isRotating = false;
            }
        }
    }
    private void HandleJump()
    {
        if (grounded && playerInput.actions["Jump"].triggered)
        {
            Jump();
            StartRotation(-90f);
        }
    }
    private void MoveForward()
    {
        Vector2 movement = new(moveSpeed, rb.velocity.y);
        rb.velocity = movement;
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WaitForCoyote()
    {
        grounded = false;
        particle.SetActive(false);
    }
    private void StartRotation(float angle)
    {
        targetRotation = Quaternion.Euler(0, 0, targetRotation.eulerAngles.z + angle);
        isRotating = true;
    }
    public void ChangeInGroundState(bool state)
    {
        grounded = state;
    }
    public void Coyote()
    {
        Invoke(nameof(WaitForCoyote), coyoteTime);
        StartRotation(-90f);
    }
}
