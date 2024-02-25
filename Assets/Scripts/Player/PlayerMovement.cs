using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Onjects")]
    [SerializeField] private JoystickController joystickController;
    private CharacterController characterController;

    [SerializeField] private PlayerAnimator playerAnimator;
    Vector3 moveVector;

    [Header("Settings")]
    [SerializeField] private int speed;
    // Start is called before the first frame update

    private float gravity = -9.81f;
    private float gravityMultiplayer = 3f;
    private float gravityVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MovePlayer();

    }

    private void MovePlayer()
    {
        moveVector = joystickController.GetMovePosition() * speed * Time.deltaTime / Screen.width;
        
        moveVector.z = moveVector.y;
        moveVector.y = 0;

        playerAnimator.ManageAnimator(moveVector);

        ApplyGravity();
        characterController.Move(moveVector);
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && gravityVelocity < 0f)
        {
            gravityVelocity = -1f;
        }
        else
        {
             gravityVelocity = gravity * gravityMultiplayer * Time.deltaTime; 
        }

        moveVector.y = gravityVelocity;
    }
}
