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
    /// <summary>
    /// dönme olayýný kontrol eder ve uygular
    /// </summary>
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
    /// <summary>
    /// eðer zemine deðiyorsa ve jump tuþu tetiklenmiþse 
    /// zýplama ve dönme metotlarýný çalýþtýrýr
    /// </summary>
    private void HandleJump()
    {
        if (grounded && playerInput.actions["Jump"].triggered)
        {
            Jump();
            StartRotation(-90f);
        }
    }
    /// <summary>
    /// moveSpeed deðerine göre ileri doðru hareket eder
    /// </summary>
    private void MoveForward()
    {
        rb.velocity = new(moveSpeed, rb.velocity.y);
    }
    /// <summary>
    /// jump force deðerine göre zýplar
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    /// <summary>
    /// dönülecek hedef açýya göre rotasyon belirler ve dönme iþlemini baþlatýr
    /// </summary>
    /// <param name="angle">dönülecek hedef açý</param>
    private void StartRotation(float angle)
    {
        targetRotation = Quaternion.Euler(0, 0, targetRotation.eulerAngles.z + angle);
        isRotating = true;
    }
    /// <summary>
    /// yere deðme durumunu dýþarýdan deðiþtirmemizi saðlar
    /// </summary>
    /// <param name="state">ground deðiþkeni true ise yere deðiyor false ise yere deðmiyor</param>
    public void ChangeInGroundState(bool state)
    {
        grounded = state;
    }
    /// <summary>
    /// Coyote mekaniðinin çalýþmasýný saðlar
    /// coyote zeminden ayrýldýktan kýsa bir süre sonra zýplama yapabilmemizi saðlar
    /// </summary>
    public void Coyote()
    {
        Invoke(nameof(WaitForCoyote), coyoteTime);
        StartRotation(-90f);
    }
    private void WaitForCoyote()
    {
        grounded = false;
        particle.SetActive(false);
    }
}
