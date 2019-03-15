using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    public GameObject Shadow;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "disappear")
        {
             Shadow.SetActive(false);
            
           

        }
    }
}
