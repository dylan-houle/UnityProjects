using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoD_AnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maxWalkVelocity = 0.5f;
    public float maxRunVelocity = 2.0f;

    // Increase performance
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        // Search the GameObject this script is attached to and the get the animator component
        animator = GetComponent<Animator>();

        // Increase performance
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    // Handles acceleration and deceleration
    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // If player presses forward, increase velocity in Z direction
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        // Increase velocity in left direction
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        // Increase velocity in right direction
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // Decrease velocity in Z direction
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // Increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        // Decrease velocityX if right is not pressed and velocity > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    // Handles reset and locking of velocity
    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
    {
        // Reset velocity in Z direction
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // Reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > 0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        // Lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // Decelerate to maximum walk velocity
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        // Lock left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // Decelerate to maximum walk velocity
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

         // Lock right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        // Decelerate to maximum walk velocity
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            // Round to the currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get key input from player
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Set current maxVelocity
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;

        // Handle changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        // Set the parameters to our local variable values
        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

    }
}
