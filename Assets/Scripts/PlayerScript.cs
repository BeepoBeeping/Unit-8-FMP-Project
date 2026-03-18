using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;


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
    Rigidbody rb;
    public bool grounded;

    public float waiting = 3f;
    public bool deathCooldown = true;
    public bool falling;
    InputAction moveAction;
    InputAction jumpAction;

    #region Start

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        DoLogic();
    }

    #endregion

    #region Late Update
    private void LateUpdate()
    {
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

        if (jumpAction.IsPressed())
        {
            // simulate jump
            state = States.Jump;
            rb.linearVelocity = new Vector3(0, 4.5f, 0);
        }

        if (moveAction.IsPressed())
        {
            transform.Rotate(0, 0.5f, 0, Space.Self);

        }
        if (moveAction.IsPressed())
        {
            transform.Rotate(0, -0.5f, 0, Space.Self);
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
            vel = transform.forward * 3.65f;
        }
        else
        {
            vel = transform.forward * 0.5f;
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
            vel = transform.forward * 3.65f;
        }
        else
        {
            vel = transform.forward * 0.5f;
        }

        if (jumpAction.IsPressed())
        {
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