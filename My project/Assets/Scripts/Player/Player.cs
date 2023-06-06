using UnityEngine;

public class Player : MonoBehaviour, IControllable
{
    private CharacterController characterController;
    private PlayerAnimatorController playerAnimator;

    [SerializeField]
    private Transform camera;

    [SerializeField]
    private float walkSpeed = 3f, runSpeed = 6f, sprintSpeed = 10f;

    [SerializeField]
    private float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    private Vector3 playerInput;
    private Vector3 gravityForce = new Vector3(0f, -9.81f, 0f);

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<PlayerAnimatorController>();
    }

    private void FixedUpdate()
    {
        if (!characterController.isGrounded)
        {
            characterController.Move(gravityForce * Time.deltaTime);
        }

        OnMove(playerInput);
    }

    public void Move(Vector2 direction)
    {
        playerInput = direction;
    }

    private void OnMove(Vector2 direction)
    {

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * runSpeed * Time.deltaTime);
            playerAnimator.IsRuning(true);
            playerAnimator.IsIdle(false);
        }
        else
        {
            playerAnimator.IsIdle(true);
            playerAnimator.IsRuning(false);
        }
    }

    public void Jump()
    {
        
    }
}
