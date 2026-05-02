using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class FPSController : MonoBehaviour
{
    #region General Variables
    [Header("Movement & Look")]
    [SerializeField] GameObject camHolder;
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float maxForce = 1f;
    [SerializeField] float sensitivity = 0.1f;

    [Header("Jump & GroundCheck")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.3f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    [Header("Player State Bools")]
    [SerializeField] bool isSprinting;
    public bool isCrouching { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] float footstepRate = 0.5f;
    float footstepTimer;
    #endregion

    Rigidbody rb;
    Animator anim;
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.DrawRay(camHolder.transform.position, camHolder.transform.forward * 100f, Color.yellow);
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void CameraLook()
    {
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);
        lookRotation += (-lookInput.y * sensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -90, 90);
        camHolder.transform.localEulerAngles = new Vector3(lookRotation, 0f, 0f);
    }

    void Movement()
    {
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, moveInput.y);
        targetVelocity *= isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : speed;
        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = (targetVelocity - currentVelocity);
        velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);

        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (isGrounded && targetVelocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0)
            {
                if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(2);
                float speedMultiplier = isSprinting ? 0.7f : 1f;
                footstepTimer = footstepRate * speedMultiplier;
            }
        }
        else
        {
            if (footstepTimer > 0)
            {
                if (AudioManager.Instance != null) AudioManager.Instance.FadeOutPasos(0.4f);
            }
            footstepTimer = 0;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    #region Input Methods
    public void OnMove(InputAction.CallbackContext context) => moveInput = context.ReadValue<Vector2>();
    public void OnLook(InputAction.CallbackContext context) => lookInput = context.ReadValue<Vector2>();
    public void OnJump(InputAction.CallbackContext context) { if (context.performed) Jump(); }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = !isCrouching;
            anim.SetBool("isCrouching", isCrouching);
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed && !isCrouching) isSprinting = true;
        if (context.canceled) isSprinting = false;
    }
    #endregion
}



