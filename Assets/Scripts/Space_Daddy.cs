using UnityEngine;
using System.Collections;

public class Space_Daddy : MonoBehaviour {
    private Animator anim;
    private Rigidbody rigidBody;
    private CharacterController controller;
    [SerializeField] float jumpForce = 20f;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
    public Vector3 jump;

    void Start () {
        controller = GetComponent <CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update (){
        if (Input.GetKey ("w"))
        {
            anim.SetInteger ("AnimationPar", 1);
        }
        else
        {
            anim.SetInteger("AnimationPar", 0);
        }
        if(controller.isGrounded)
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
        if (Input.GetKeyDown("f") && !controller.isGrounded)
        {
            anim.Play("Flip");
        }
    }
}
