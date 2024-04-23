using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Way : MonoBehaviour
{
    public Arena topArena;
    public Arena bottomArena;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {              
        if (collision.GetContact(0).otherCollider.transform.gameObject.name == "Top"){
            topArena = collision.collider.gameObject.GetComponent<Arena>();
            //Debug.Log("Way 1 Top: " + topArena.name);
        }

        if (collision.GetContact(0).otherCollider.transform.gameObject.name == "Bottom"){
            bottomArena = collision.collider.gameObject.GetComponent<Arena>();
            //Debug.Log("Way 1 Bottom: " + bottomArena.name);
        }
     
    }

}
