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
        characterController.Move(moveVector);
    }
}
