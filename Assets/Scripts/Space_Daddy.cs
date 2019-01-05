using System;
using UnityEngine;
using System.Collections;

public class Space_Daddy : MonoBehaviour {
    private Animator anim;
    private Rigidbody rb;
    public float turnSpeed = 400.0f;
    private bool groundedState;
    //private Vector3 moveDirection = Vector3.zero;


    public float gravity = 20.0f;
    public Vector3 jump;
    
    public Collider[] colliderBoxes;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");
    private 

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate (){
    
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);
        Debug.Log(groundedState);

        if (Input.GetKey ("w") || Input.GetKey("s"))
        {
            anim.SetInteger(AnimationPar, 1);
            
        }
        else
        {
            anim.SetInteger(AnimationPar, 0);
        }

        if (Input.GetKeyDown("f") && !groundedState)
        {
            Flip();
        }

        Jump(groundedState);
        var turn = Input.GetAxis("Horizontal");
        if (Math.Abs(turn) < 0.1f)
        {
            Turn(turn);
        }
       
    }

    private void Turn(float turn)
    {
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

    }
    private void Flip()
    {
        
            anim.Play("Flip");
    }

    private void Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown("space"))
            {
                //jumpVelocity.y = jumpForce;
                anim.Play("Jump_start");
            }
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Flip"))
            {
                anim.Play("Jump_loop");
            }
            
        }
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
   
    protected void LateUpdate()
    {
        var transform1 = transform;
        transform1.localEulerAngles = new Vector3(0, transform1.localEulerAngles.y, 0);
    }
}
