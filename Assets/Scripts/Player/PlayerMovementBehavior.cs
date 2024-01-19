using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovementBehavior : MonoBehaviour
{
    //Important scripts
    public PlayerControls playercontrols;
    [SerializeField]private Rigidbody2D _rb;


    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _isMoving;
    private Vector2 _moveInput;


    [SerializeField] private InputActionMap _currentActionMap { get; set; }

    [Header("Action Map bools")]
    public bool playerCanMove = true;
    public bool playerCanPause = true;
    public bool playerCanInteract = true;


    private void OnEnable()
    {
        playercontrols = new PlayerControls();
        playercontrols.Overworld.Enable();

        //Move controls performed
        playercontrols.Overworld.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playercontrols.Overworld.Movement.performed += ctx => _isMoving = true;


        //Move controls canceled
        playercontrols.Overworld.Movement.canceled += ctx => _moveInput = Vector2.zero;
        playercontrols.Overworld.Movement.canceled += ctx => _isMoving = false;

    }


    private void OnDisable()
    {
        //Removes on disable


        playercontrols.Disable();
      

        //Move controls performed
        playercontrols.Overworld.Movement.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        playercontrols.Overworld.Movement.performed -= ctx => _isMoving = true;


        //Move controls canceled
        playercontrols.Overworld.Movement.canceled -= ctx => _moveInput = Vector2.zero;
        playercontrols.Overworld.Movement.canceled -= ctx => _isMoving = false;
    }


    private void Awake()
    {
        
    }

    public void Update()
    {
        //Fixes diagnal input
        if (_moveInput.x != 0)
        {
            _moveInput.y = 0;
        }


        if (_moveInput != Vector2.zero)
        {
            //Use for animations when sprites are received
            //animator.SetFloat("moveX", _input.x);
            //animator.SetFloat("moveY", _input.y);
        }
        _rb.velocity = new Vector2(_moveInput.x * _speed, _moveInput.y * _speed);

        //animator.SetBool("isMoving", _isMoving);

        CheckForEncounters();
    }


    public void CheckForEncounters()
    {
        //While player is moving
        if(_isMoving)
        {
            if (Random.Range(1, 5000) <= 10) //random value can be changed
            {
                Debug.Log("Start battle!");
            }
        }
    }
}
