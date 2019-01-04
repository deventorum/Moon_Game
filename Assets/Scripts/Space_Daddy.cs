using System;
using UnityEngine;
using System.Collections;

public class Space_Daddy : MonoBehaviour {
    private Animator anim;
    private Animation animate;
    private Rigidbody rigidBody;
    private CharacterController controller;
    [SerializeField] float jumpForce = 20f;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public Vector3 jump;
    bool groundedState;
    public Collider[] colliderBoxes;

    void Start () {
        controller = GetComponent <CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        animate = gameObject.GetComponentInChildren<Animation>();
        groundedState = CheckGroundCollision(colliderBoxes[0]);
    }

    void Update (){

        if (Input.GetKey ("w") || Input.GetKey("s"))
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                // do something
                anim.SetInteger("AnimationPar", 0);
            }
            else
            {
                anim.SetInteger("AnimationPar", 1);
            }
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }
        if(groundedState)
        {
            moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
            Jumping();
        }
            Flip();

        float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection.y -= gravity * Time.deltaTime;
    
    }
    void FixedUpdate()
    {
        SetGroundedState();
    }

    private void SetGroundedState()
    {
        groundedState = CheckGroundCollision(colliderBoxes[0]);
        //CheckGroundCollision(colliderBoxes[0]);
        CheckAttackCollision(colliderBoxes[1]);
        Debug.Log(groundedState);
    }


    private bool CheckGroundCollision(Collider col)
    {
        Collider[] cols = Physics.OverlapBox(col.transform.position, col.transform.localScale / 2, Quaternion.identity, LayerMask.GetMask("Terrain", "Base"));
        {
            foreach (Collider c in cols)
            {
                //groundedState |= c is UnityEngine.MeshCollider;
                Debug.Log(c.name + gameObject);
                //groundedState = true;

            }

            if (cols.Length > 0)
            {
                return true;
            }

            return false;

        }
    }


    private void CheckAttackCollision(Collider col)
    {

    }

    //void OnTriggerEnter(Collider col)
    //{
    //    Debug.Log("Hit");
    //    if (col is BoxCollider && col.name != "Stylized Astronaut")
    //    {
    //        if (col.gameObject.tag == "GroundCollider")
    //        {
    //            Debug.Log(col.gameObject.name + " colliding with " + col.name + " when rigidbody is " + col.attachedRigidbody);
    //        }
    //        if (col.gameObject.tag == "HurtBox")
    //        {
    //            Debug.Log(col.gameObject.name + " colliding with " + col.name + " when rigidbody is " + col.attachedRigidbody);
    //        }
    //    }
    //    if (col.name != "Stylized Astronaut")
    //    {
    //        Debug.Log("Object " + name + tag + gameObject.gameObject +" has hit object "+ col.name);
    //    }
    //    if (null != col.gameObject.GetComponent<Sword>())
    //    {
    //        //do sword stuff
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale / 2);
    }

    private void Jumping()
    {
        if (Input.GetKeyDown("space"))
        {
            anim.Play("Jump_start");
            moveDirection.y += jumpForce;
        }
    }
    private void Flip()
    {
        if (Input.GetKeyDown("f") && !groundedState)
        {
            anim.Play("Flip");
        }
    }
}
