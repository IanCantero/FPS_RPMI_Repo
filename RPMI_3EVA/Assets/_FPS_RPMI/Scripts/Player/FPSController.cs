using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{

    #region General Variables
    [Header("Movement & Look")]
    [SerializeField] GameObject camHolder; //Ref al empty que tiene la camara como hijo
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float maxForce = 1f; //Fuerza máxima de aceleración
    [SerializeField] float sensitivity = 0.1f; //Sensibilidad para el input de look

    [Header("Player State Bools")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool isCrouching;
    #endregion


    //Variables de referencia privadas
    Rigidbody rb;
    Animator anim;

    //Variables para el input
    Vector2 moveInput;
    Vector2 lookInput;
    float lookRotation;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Lock del cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;  //Mueve el cursor al centro
        Cursor.visible = false; //Oculta el cursor de la vista
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Input Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        
    }
    #endregion
}
