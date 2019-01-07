using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
   void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Platform")
        {
            transform.parent.parent.parent = col.transform;

        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Platform")
        {
            transform.parent.parent.parent = null;

        }
    }
}
