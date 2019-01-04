using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionScript : MonoBehaviour
{
    // Start is called before the first frame update


    public Collider[] groundCol;

    private void Awake()
    {
        // add isTrigger
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }
    void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
