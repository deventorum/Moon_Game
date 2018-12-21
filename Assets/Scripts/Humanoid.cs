using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class Humanoid : MonoBehaviour
{
  [SerializeField] private int _baseMoveSpeed;
  [SerializeField] private float _baseHealth;
  [SerializeField] private bool _isMoving = false;

  public bool isMoving
  {
    get { return _isMoving; }
    set { _isMoving = value; }
  }

  public float baseHealth
  {
    get { return baseHealth; }
    set { _baseHealth = value; }
  }

  public int baseMoveSpeed
  {
    get { return _baseMoveSpeed; }
    set { _baseMoveSpeed = value; }
  }
  // Start is called before the first frame update
  void Awake()
  {

  }


  public virtual void MoveForward(Rigidbody rb)
  {
    //Implement Control Scheme
    if (isMoving)
    {
      isMoving = false;
    }

  }
  public virtual void MoveBackward(Rigidbody rb)
  {
    if (isMoving)
    {
      isMoving = false;
    }

  }
  public virtual void MoveLeft(Rigidbody rb)
  {
    if (isMoving)
    {
      isMoving = false;
    }

  }
  public virtual void MoveRight(Rigidbody rb)
  {
    if (isMoving)
    {
      isMoving = false;
    }

  }


}
