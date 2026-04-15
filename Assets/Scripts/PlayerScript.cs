using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;


public enum States // used by all logic
{
    None,
    Idle,
    Walk,
    Jump,
};

public class PlayerScript : MonoBehaviour
{


    States state;

    public Animator anim;
    public Rigidbody rb;
    public bool grounded;

    public float waiting = 3f;
    public bool deathCooldown = true;
    public bool falling;
    CharacterStats characterStats;
    InputAction prepareAction;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction stompAction;
    InputAction tauntAction;
    InputAction menuAction;
    InputAction dodgeAction;

    #region Start

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        stompAction = InputSystem.actions.FindAction("Stomp");
        tauntAction = InputSystem.actions.FindAction("Taunt");
        menuAction = InputSystem.actions.FindAction("Menu");
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        prepareAction = InputSystem.actions.FindAction("Prepare");

    }

    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
       

        if (stompAction.IsPressed())
        {
            rb.linearVelocity = new Vector3(0, -3f, 0);
        }
    }

    #endregion

    #region Late Update
    private void LateUpdate()
    {
        DoLogic();


        grounded = false;
    }

    #endregion

    #region Logic

    void DoLogic()
    {
        if (state == States.Idle)
        {
            PlayerIdle();
        }

        if (state == States.Jump)
        {
            PlayerJumping();
        }

        if (state == States.Walk)
        {
            PlayerWalk();
        }

    }

    #endregion

    #region Player Idle
    void PlayerIdle()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalk", false);
        anim.SetBool("isJump", false);
        anim.SetBool("movingJump", false);

        if (jumpAction.IsPressed())
        {
            // simulate jump
            anim.SetBool("isJump", true);
            state = States.Jump;
            rb.linearVelocity = new Vector3(0, 4f, 0);
        }

      

        if (moveAction.IsPressed())
        {
            state = States.Walk;
        }

    }

    #endregion

    #region Player Jump
    void PlayerJumping()
    {
        anim.SetBool("isIdle", false);
        anim.SetBool("isWalk", false);
        Vector3 vel;

        // player is jumping, check for hitting the ground
        if (grounded == true)
        {
            //player has landed on floor
            state = States.Idle;
        }

        float magnitude = rb.linearVelocity.magnitude;

        if (moveAction.IsPressed())
        {
            vel = transform.forward * 5f;
        }
        else
        {
            vel = transform.forward * 0f;
        }

       



        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

        if (magnitude <= 0.5f)
        {
            state = States.Idle;
        }
    }

    #endregion

    #region Player Walk
    void PlayerWalk()
    {
        Vector3 vel;

        //magnitude = the player's speed
        float magnitude = rb.linearVelocity.magnitude;

        //move forward and preserve original y velocity

        if (moveAction.IsPressed())
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);

            vel = transform.forward * 5f;
        }
        else
        {
            vel = transform.forward * 0f;
        }

        if (jumpAction.IsPressed())
        {
            anim.SetBool("movingJump", true);
            state = States.Jump;
            rb.linearVelocity = new Vector3(0, 4.5f, 0);
        }

      

        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

        if (magnitude <= 0.5f)
        {
            state = States.Idle;
        }

    }

    #endregion

   

    #region Collision

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            grounded = true;
            print("landed!");
        }    
    }



    #endregion

    //Output debug info to canvas
    private void OnGUI()
    {
        float mag = rb.linearVelocity.magnitude;

        mag = Mathf.Round(mag * 100) / 100;

        //debug text
        string text = "Left/Right arrows = Rotate\nSpace = Jump\nUp Arrow = Forward\nCurrent state=" + state;
        text += "\nmag=" + mag;

        // define debug text area
        GUILayout.BeginArea(new Rect(10f, 450f, 1600f, 1600f));
        GUILayout.Label($"<size=16>{text}</size>");
        GUILayout.EndArea();
    }
}