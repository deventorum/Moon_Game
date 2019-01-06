using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
   void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Platform")
        {
            Debug.Log(col.gameObject.tag);
            transform.parent.parent.parent = col.transform;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            Debug.Log("Exit");
            transform.parent.parent.parent = null;

        }
    }
}
