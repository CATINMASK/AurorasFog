using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    public bool canControl = true;

    [SerializeField] private float walkSpeed = 3f, runSpeed = 6f;
    [SerializeField] private float runBuildUpSpeed = 1f;            // Множитель перехода от шага к бегу.
    [SerializeField] private KeyCode runKey = KeyCode.LeftShift;

    private float movementSpeed;    // Speed of the player in a current moment;

    [SerializeField] private float slopeForce = 6;
    [SerializeField] private float slopeForceRayLength = 1;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier = 5;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;


    private bool isJumping;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        if (PlayerPrefs.GetInt("sprint") != 0)
            runKey = (KeyCode)PlayerPrefs.GetInt("sprint");
        if (PlayerPrefs.GetInt("jump") != 0)
            jumpKey = (KeyCode)PlayerPrefs.GetInt("jump");
    }

    private void Update()
    {
        if (canControl)
            PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;


        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);

        SetMovementSpeed();
        JumpInput();
    }

    private void SetMovementSpeed()
    {
        if (Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
    }


    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
            {
                print("OnSlope");
                return true;
            }

        return false;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }


    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }

}