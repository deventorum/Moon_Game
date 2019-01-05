using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PantherAi : MonoBehaviour
{

  public GameObject Player;
  public Animator animator;

  public float speed;

  private Collider playerCol;
  private Rigidbody rb;
  public int MinDist = 30;
  private Transform t;
  private Transform trans;

  private void Start()
  {
    playerCol = Player.GetComponent<BoxCollider>();
    rb = GetComponent<Rigidbody>();
    trans = new GameObject().transform;
    t = Player.GetComponent<Transform>();
    Debug.Log(t.position);
    animator = GetComponent<Animator>();
  }

  private void FixedUpdate()
  {
    var position = t.position;
    var ang = (float)Math.Atan2(position.z, position.x);
    var rot = Quaternion.Euler(0, ang, 0);
    trans.SetPositionAndRotation(position, rot);
    transform.LookAt(trans);
    // Debug.Log(Vector3.Distance(rb.position, playerCol.transform.position));

    var position1 = rb.position;
    var pos2 = t.position;
    var cur = new Vector2(position1.x, position1.z);
    var pla = new Vector2(pos2.x, pos2.z);
    
    
    if ((Vector2.Distance(cur, pla) - MinDist) >= 0)
    {
      animator.Play("Run");
      MoveTowards();
    }
    else if ((Vector2.Distance(cur, pla) - MinDist) < 0)
    {
      rb.velocity = Vector3.zero;
      // Do damage!! 
      //Dennis Do whatever you want!
    }
    else
    {
      rb.constraints = RigidbodyConstraints.FreezeAll;
      // Idk what the heck happened but ok
    }
  }

  private void MoveTowards()
  {
    transform.position = Vector3.Lerp(transform.position, t.position,speed * Time.deltaTime);
  }
  
  protected void LateUpdate()
  {
    var transform1 = transform;
    transform1.localEulerAngles = new Vector3(0, transform1.localEulerAngles.y, 0);
  }

}
