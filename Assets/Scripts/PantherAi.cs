using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PantherAi : MonoBehaviour
{

  public GameObject Player;
  public Animator animator;
  int MoveSpeed = 4;
  int MaxDist = 10;
  int MinDist = 5;
  private Transform t;
  private Transform trans;

  void Start()
  {
    trans = new GameObject().transform;
    t = Player.GetComponent<Transform>();
    animator = GetComponent<Animator>();
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
      animator.Play("run");
      this.transform.position = Vector3.MoveTowards(transform.position, t.position, MoveSpeed * Time.deltaTime);
      //this.transform.position += Vector3.up * 4f;


      if (Vector3.Distance(this.transform.position, t.position) <= MaxDist)
      {
        //Here Call any function U want Like Shoot at here or something
      }

    }
  }
}
