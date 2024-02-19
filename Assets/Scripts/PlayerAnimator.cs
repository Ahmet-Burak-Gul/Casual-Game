using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAC;
    // Start is called before the first frame update
    public void ManageAnimator(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            PlayRunAnimator();

            playerAC.transform.forward = move.normalized;
        }
        else
        {
            PlayIdleAnimator();
        }
;
    }

    void PlayRunAnimator()
    {
        playerAC.Play("Run");
    }

    void PlayIdleAnimator()
    {
        playerAC.Play("Idle");
    }
}
