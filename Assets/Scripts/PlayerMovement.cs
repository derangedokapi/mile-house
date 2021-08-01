using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    Rigidbody2D myRigidBody;
    //Animator myAnimator;
    bool isMoving = false;

    void Start() {
        myRigidBody = GetComponent<Rigidbody2D>();
        //myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        
        Run();
        //FlipSprite();
    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // from -1 and +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void FlipSprite() {
        bool playerHasHorizontalSpeed = IsPlayerMoving(); //Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // abs is the value positive or negative, e.g. turns -5 into 5. basically saying if it has eitehr pos or neg velocity = true
        // Epsilon instead of 0 I believe to allow for "close enough"

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
           //myAnimator.SetBool("isRunning",true);
        } else {
           // myAnimator.SetBool("isRunning",false);
        }
    }

    public bool IsPlayerMoving() {
        Debug.Log(myRigidBody.velocity.x);
        return(Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon);
    }

    public float GetPlayerVelocitySign() {
        if (myRigidBody.velocity.x >= 0) { return 1; } else { return -1; };
    }


    public float GetPlayerXPos() {
        return(transform.position.x);
    }
}
