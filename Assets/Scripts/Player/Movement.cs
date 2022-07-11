using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed,movementForce,dampingForce;
    [Space]

    private Vector2 InputMovement;
    private Vector3 currentVelocity;
    private Rigidbody Rigidbody;

    [SerializeField] private float jumpForce, jumpTime;

    private bool isGrounded = false,isJumping = false,canJump = false;

    private float jumpTimer = 0f;

    #region Input

    public void xyInput(InputAction.CallbackContext ctx)
    {
        InputMovement = ctx.ReadValue<Vector2>();
    }

    public void jumpInput(InputAction.CallbackContext ctx)
    {
        Jump();
    }

    #endregion

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        jumpTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        currentVelocity = Rigidbody.velocity;
        if (isGrounded)
            Rigidbody.AddForce(-movementForce * InputMovement.x * Vector3.forward + movementForce * InputMovement.y * Vector3.right, ForceMode.Force);//Ana hareket

        if(currentVelocity.magnitude > MaxSpeed)//Hýz Limitini Geçtiyse
        {
            Rigidbody.AddForce(-currentVelocity * dampingForce, ForceMode.Force);
        }
    }

    private void Jump()
    {
        if (!isGrounded || jumpTimer < jumpTime)
            return;

        jumpTimer = 0f;

        Rigidbody.AddForce(jumpForce * Vector3.up, ForceMode.Force);//Zýplama
        isJumping = true;
    }

    #region collision check
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + currentVelocity);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (-1 * InputMovement.x * Vector3.forward + 1 * InputMovement.y * Vector3.right));

        if(isJumping)
            Gizmos.DrawLine(transform.position, transform.position + (2 * Vector3.up));

        if (currentVelocity.magnitude > MaxSpeed)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (-currentVelocity.normalized * 2 * currentVelocity.magnitude / MaxSpeed));
        }
    }
}
