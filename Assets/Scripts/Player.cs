using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : Humanoid
{
  [SerializeField] private int moveSpeed;
  private Animator anim;

  private Rigidbody rigidBody;

  void Awake()
  {
    //Assert.IsNotNull(moveSpeed);
  }
  // Start is called before the first frame update
  void Start()
  {
    baseMoveSpeed = moveSpeed;
    rigidBody = GetComponent<Rigidbody>();
    anim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown("w"))
    {
      isMoving = true;
      MoveForward(rigidBody);
    }
    if (Input.GetKeyDown("a"))
    {
      isMoving = true;
      MoveLeft(rigidBody);
    }
    if (Input.GetKeyDown("d"))
    {
      isMoving = true;
      MoveRight(rigidBody);
    }
    if (Input.GetKeyDown("s"))
    {
      isMoving = true;
      MoveBackward(rigidBody);
    }

  }

  void FixedUpdate()
  {


  }

}
