using System;
using UnityEngine;


public class Space_Daddy : MonoBehaviour {
    private Animator anim;
    private Rigidbody rb;
    public float turnSpeed = 400.0f;
    private bool groundedState;
    public float jumpForce;
    public float forwardForce;
    //private Vector3 moveDirection = Vector3.zero;
    private Vector3 maxVelocity;

    public Collider[] colliderBoxes;
    private static readonly int AnimationPar = Animator.StringToHash("AnimationPar");

    private void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        var currentForward = transform.forward;
        var currentVelocity = new Vector3();
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);

        Accelerate(maxVelocity, groundedState);


        Jump(groundedState);









        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.constraints = RigidbodyConstraints.None;
            currentVelocity = Turn(Input.GetAxis("Horizontal"), rb.velocity);
            if (transform.forward != currentForward && groundedState)
            {
                rb.velocity -= currentVelocity / 10;
                Accelerate(maxVelocity, groundedState);

            }
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }









        if (Input.GetKeyDown("f") && !groundedState)
        {
            Flip();
        }

    }

    private void Accelerate(Vector3 maxVelocity, bool groundedState)
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            rb.isKinematic = false;
            anim.SetInteger(AnimationPar, 1);
            maxVelocity = transform.forward * forwardForce * 20;
            if (Vector3.Distance(new Vector3(0,0,0), rb.velocity) < Vector3.Distance(new Vector3(0,0,0),maxVelocity ))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    rb.velocity += transform.forward * forwardForce;
                }
                else
                {
                    rb.velocity += transform.forward * -forwardForce;
                }
            }
        }
        else
        {
            anim.SetInteger(AnimationPar, 0);
            if (!groundedState) return;
            rb.isKinematic = true;
            rb.velocity = new Vector3(0,rb.velocity.y,0);
        }

        
    }

    private Vector3 Turn(float turn, Vector3 v)
    {
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        return v;

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
                rb.AddForce(0,jumpForce * Time.deltaTime,0,ForceMode.VelocityChange);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var transform1 = transform;
        Gizmos.DrawWireCube(transform1.position, transform1.localScale / 2);
    }
   
    protected void LateUpdate()
    {
        var transform1 = transform;
        transform1.localEulerAngles = new Vector3(0, transform1.localEulerAngles.y, 0);
    }
}
