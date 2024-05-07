using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Way : MonoBehaviour
{
    public Arena topArena;
    public Arena bottomArena;
    public Vector3 topPosition;
    public Vector3 bottomPosition;

    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().enabled = false;
        topPosition = transform.Find("Top").transform.position;
        bottomPosition = transform.Find("Bottom").transform.position;
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Arena>() != null)
        {
            var colliderChild = collision.GetContact(0).otherCollider;

            if (colliderChild.gameObject.name == "Top")
                topArena = collision.collider.gameObject.GetComponent<Arena>();


            if (colliderChild.gameObject.name == "Bottom")
                bottomArena = collision.collider.gameObject.GetComponent<Arena>();

        }
    }
}
