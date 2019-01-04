using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PantherAi : MonoBehaviour
{

  public GameObject Player;
  //public Animator animator;

  private Rigidbody rb;
  int MoveSpeed = 2;
  int MaxDist = 10;
  int MinDist = 5;
  private Transform t;
  private Transform trans;

  void Start()
  {
    rb = GetComponent<Rigidbody>();
    trans = new GameObject().transform;
    t = Player.GetComponent<Transform>();
    // animator = GetComponent<Animator>();
  }

  void Update()
  {
    float ang = (float)Math.Atan2(t.position.z, t.position.x);
    //ang = ang - 90.0f;
    var rot = Quaternion.Euler(0, ang, 0);
    trans.SetPositionAndRotation(t.position, rot);
    this.transform.LookAt(trans);

    if (Vector3.Distance(this.transform.position, t.position) >= MinDist)
    {
      // animator.Play("walk");
      rb.MovePosition(t.position);
      //this.transform.position += Vector3.up * 4f;


      if (Vector3.Distance(this.transform.position, t.position) <= MaxDist)
      {
        //Here Call any function U want Like Shoot at here or something
      }

    }
  }
}
