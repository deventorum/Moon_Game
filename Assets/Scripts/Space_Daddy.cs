using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Space_Daddy : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigidBody;
    private CharacterController controller;
    [SerializeField] float jumpForce = 20f;

    private Vector3 jumpVelocity = Vector3.zero;
    private Vector3 sideManeuring = Vector3.zero;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    public float sidewayControl = 20.0f;

    //private Vector3 moveDirection = Vector3.zero;

    private const int TOTAL_LIVES = 3;
    public int livesRemaining;


    public float gravity = 20.0f;
    public Vector3 jump;
    private bool groundedState;
    public Collider[] colliderBoxes;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");


    void Start()
    {
        livesRemaining = 3;
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        groundedState = CheckGroundCollision(colliderBoxes[0]);
    }

    private void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            anim.SetInteger(AnimationPar, 1);
        }
        else
        {
            anim.SetInteger(AnimationPar, 0);
        }


        Vector3 moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        if (groundedState)
        {
            moveDirection *= speed;
            if (Input.GetKeyDown("space"))
            {
                jumpVelocity = moveDirection;
                jumpVelocity.y = jumpForce;
                anim.Play("Jump_start");
            }
            else
            {
                //sideManeuring = moveDirection;
                //sideManeuring.x = sidewayControl;
                jumpVelocity = Vector3.zero;

            }
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flip"))
            {
                anim.Play("Jump_loop");
            }
            moveDirection *= speed / 2;
            jumpVelocity.y -= gravity * Time.deltaTime;
        }
        Flip();
        var turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move((moveDirection + jumpVelocity) * Time.deltaTime);

        //if (groundedState)
        //{

        //    moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
        //    Jumping();
        //}
        //    Flip();
        //if (!groundedState)
        //{
        //    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flip"))
        //    {
        //        anim.Play("Jump_loop");
        //    }
        //}
        //var turn = Input.GetAxis("Horizontal");
        //transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        //controller.Move(moveDirection * Time.deltaTime);
        //moveDirection.y -= gravity * Time.deltaTime;

        // TODO - Add life handling method here

        if (livesRemaining == 0)
        {
            GameController.Instance.GameOver();
        }

    }

    private void FixedUpdate()
    {
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);
        Debug.Log(groundedState);
    }

    private void SetGroundedState()
    {
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);
        Debug.Log(groundedState);
    }


    private bool CheckGroundCollision(Collider col)
    {
        var transform1 = col.transform;
        Collider[] cols = Physics.OverlapBox(transform1.position, transform1.localScale / 2, Quaternion.identity, LayerMask.GetMask("Terrain", "Base"));
        foreach (Collider c in cols)
        {
            Debug.Log(c.name + gameObject);
        }

        if (cols.Length > 0 && !groundedState)
        {
            anim.Play("Jump_end");
        }
        return cols.Length > 0;
    }


    private void CheckAttackCollision(Collider col)
    {
        var transform1 = col.transform;
        Collider[] cols = Physics.OverlapBox(transform1.position, transform1.localScale / 2, Quaternion.identity, LayerMask.GetMask("Enemy"));
        foreach (Collider c in cols)
        {
            Debug.Log("Attacked by fox");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var transform1 = transform;
        Gizmos.DrawWireCube(transform1.position, transform1.localScale / 2);
    }

    //private void Jumping()
    //{
    //    if (!Input.GetKeyDown("space")) return;
    //    anim.Play("Jump_start");
    //    moveDirection.y += jumpForce;
    //}
    private void Flip()
    {
        if (Input.GetKeyDown("f") && !groundedState)
        {
            anim.Play("Flip");
        }
    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    /** 
     * Scene management is closely tied to the life of space daddy, so scene
     * management is included here
    */

    private void SceneController()
    {
        SceneManager.LoadScene("GameOver");
    }

}


