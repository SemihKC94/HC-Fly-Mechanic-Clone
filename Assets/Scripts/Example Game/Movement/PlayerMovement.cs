/*//////////////////////////////////////////////////////////////////////////////////////////
//      █─▄▄▄▄█▄─█─▄█─▄▄▄─█                                                               //
//      █▄▄▄▄─██─▄▀██─███▀█             Scripts created by Semih Kubilay Çetin            //
//      ▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▄▄▀                                                               //
//////////////////////////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Config SO")]
    [SerializeField] private SKC_RunnerMechanic movementSO = null;
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundCheckRadius = 2f;
    [SerializeField] private LayerMask groundLayer;

    [Space, Header("Animator")]
    [SerializeField] private Animator myAnimator = null;

    // Privates
    private float forwardSpeed;
    private float forwardSpeedHolder;
    private float sideSpeed;
    private float jumpForce;
    private Vector2 xClamp;
    private Vector2 dir;
    private float xStart;
    private bool onGround = true;
    private bool firstJump = false;
    private float gravity;

    // Components
    private CamController myCam;
    private Rigidbody rb;
    private PickupContainer pickupContainer;

    #region Unity Methods
    private void Start()
    {
        xStart = transform.position.x;

        myCam = Camera.main.GetComponent<CamController>();
        rb = GetComponent<Rigidbody>();
        pickupContainer = GetComponent<PickupContainer>();

        myAnimator.SetTrigger(movementSO.IdleAnim);
    }
    private void FixedUpdate()
    {
        if (GameManager.GameState != GameState.Play) return;

        GroundCheck();
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (pickupContainer.GetListCount() <= 0)
            {
                FailMechanic();
                rb.velocity = Vector3.zero;
                myAnimator.SetTrigger(movementSO.LossAnim);
            }
            else
            {
                pickupContainer.LostWings(5);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            if (pickupContainer.GetListCount() <= 0)
            {
                FailMechanic();
                rb.velocity = Vector3.zero;
                myAnimator.SetTrigger(movementSO.LossAnim);
            }
            else
            {
                pickupContainer.LostWings(5);
                other.gameObject.GetComponent<Collider>().enabled = false;
            }
        }

        if(other.gameObject.CompareTag("Finish"))
        {
            SuccessLevel();
            rb.velocity = Vector3.zero;
            myAnimator.SetTrigger(movementSO.VictoryAnim);
        }
    }

    #endregion

    #region Unique Methods
    public void Init()
    {
        forwardSpeed = movementSO.ForwardSpeed;
        sideSpeed = movementSO.SideSpeed;
        jumpForce = movementSO.JumpForce;
        xClamp = movementSO.XClamp;

        forwardSpeedHolder = forwardSpeed;
    }

    public bool OnGround()
    {
        return onGround;
    }

    private void GroundCheck()
    {
        onGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if(onGround)
        {
            gravity = rb.velocity.y;
            firstJump = false;
            myAnimator.SetBool(movementSO.FlyAnim, false);
            forwardSpeed = forwardSpeedHolder;
        }
        else
        {
            if (!firstJump)
            {
                firstJump = true;
                myAnimator.SetTrigger(movementSO.JumpAnim);
                myAnimator.SetBool(movementSO.FlyAnim, true);

                Jump();
            }

            if(pickupContainer.GetListCount() > 0 )
            {
                gravity = rb.velocity.y / 2f;
                forwardSpeed += .1f;
            }
            else
            {
                myAnimator.SetTrigger(movementSO.FallAnim);
                gravity = rb.velocity.y;
                forwardSpeed = forwardSpeedHolder;
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
    private void Move()
    {
        myCam.Assign(transform);

        dir = GUIController.Instance.GetJoystickDirection();

        float clamped = xStart + (dir.x * sideSpeed * Time.deltaTime); // TODO : Clamp
        clamped = Mathf.Clamp(clamped, xClamp.x, xClamp.y);
        Vector3 forwardDir = transform.forward * forwardSpeed * Time.deltaTime;

        // transform.Translate(new Vector3(clamped, 0f, forwardDir.z));
        rb.velocity = new Vector3(clamped, gravity, forwardDir.z);
        myAnimator.SetTrigger(movementSO.RunAnim);
    }

    private void FailMechanic()
    {
        EventBroker.InvokeFail();
        rb.useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    private void SuccessLevel()
    {
        EventBroker.InvokeLevelSuccess();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }


    #endregion

}
/* Tip    #if UNITY_EDITOR
          Debug.Log("Unity Editor");
          #endif                          Tip End */