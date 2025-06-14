using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isSlowedByZone = false;

    CharacterController controller;
    AudioSource source;

    public GroundCheck groundChecker; //rigidbody checkera ekleme olayýmsý

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    public Transform ground;
    public float distance = 0.3f;

    public float Speed;
    public float Jumpheight;
    public float gravity;
    public float speedMultiplier = 2;
    public float speedMore = 7;
    public float speedSlow = 4;

    public bool canMove = true;

    public bool canRun = true;

    public float originalHeight;
    public float crouchHeight;

    public LayerMask mask;

    public float timeBetweenSteps;
    float timer;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        #region Movoment
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = (horizontal * transform.right) + (vertical * transform.forward);    
        
        if(canMove)
            controller.Move(move * Speed * Time.deltaTime);
        #endregion

        #region Foot Steps
        if(horizontal != 0 || vertical !=0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                timer = timeBetweenSteps;
                source.Play();
            }
        }
        else
        {
            timer = timeBetweenSteps;
        }

        #endregion

        #region Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y += Mathf.Sqrt(Jumpheight * -3.0f * gravity);
        }
        #endregion

        #region Gravity
        isGrounded = groundChecker.isGrounded;

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = 0f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        #endregion

        #region Basic Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.height = crouchHeight;
            Speed = 2.0f;
            Jumpheight = 0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = originalHeight;
            Speed = 5.0f;
            Jumpheight = 1f;
        }
        #endregion

        #region Basic Running
        if (canRun && !isSlowedByZone)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && StaminaBar.instance.CanRun)
            {
                Speed = speedMore * speedMultiplier;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || !StaminaBar.instance.CanRun)
            {
                Speed = speedMore;
            }
        }
        #endregion


    }

}
