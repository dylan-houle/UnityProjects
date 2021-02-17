using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        //If player presses "W" key
        if (!isWalking && forwardPressed)
        {
            //Then set the isWalking boolean to true
            animator.SetBool(isWalkingHash, true);
        }
        //If player is not pressing "W" key
        if (isWalking && !forwardPressed)
        {
            //Then set the isWalking boolean to false
            animator.SetBool(isWalkingHash, false);
        }

        //If player is walking and not running and presses left shift
        if (!isRunning && (forwardPressed && runPressed)) 
        {
            //Then set the isRunning boolean to true
            animator.SetBool(isRunningHash, true);
        }
        //If the player is running and stops running or stops walking
        if (isRunning && (!forwardPressed || !runPressed))
        {
            //Then set the isRunning boolean to false
            animator.SetBool(isRunningHash, false);
        }
    }
}
