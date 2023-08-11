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
    /// d�nme olay�n� kontrol eder ve uygular
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
    /// e�er zemine de�iyorsa ve jump tu�u tetiklenmi�se 
    /// z�plama ve d�nme metotlar�n� �al��t�r�r
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
    /// moveSpeed de�erine g�re ileri do�ru hareket eder
    /// </summary>
    private void MoveForward()
    {
        rb.velocity = new(moveSpeed, rb.velocity.y);
    }
    /// <summary>
    /// jump force de�erine g�re z�plar
    /// </summary>
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    /// <summary>
    /// d�n�lecek hedef a��ya g�re rotasyon belirler ve d�nme i�lemini ba�lat�r
    /// </summary>
    /// <param name="angle">d�n�lecek hedef a��</param>
    private void StartRotation(float angle)
    {
        targetRotation = Quaternion.Euler(0, 0, targetRotation.eulerAngles.z + angle);
        isRotating = true;
    }
    /// <summary>
    /// yere de�me durumunu d��ar�dan de�i�tirmemizi sa�lar
    /// </summary>
    /// <param name="state">ground de�i�keni true ise yere de�iyor false ise yere de�miyor</param>
    public void ChangeInGroundState(bool state)
    {
        grounded = state;
    }
    /// <summary>
    /// Coyote mekani�inin �al��mas�n� sa�lar
    /// coyote zeminden ayr�ld�ktan k�sa bir s�re sonra z�plama yapabilmemizi sa�lar
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
