using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public List<Way> upWays = new List<Way>();
    public List<Way> downWays = new List<Way>();

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(ways.Count.ToString());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Way>() != null)
        {
            var component = collision.gameObject.GetComponent<Way>();

            if (collision.collider.transform.gameObject.name == "Bottom")
                if (upWays.IndexOf(component) < 0)
                    upWays.Add(component);

            if (collision.collider.transform.gameObject.name == "Top")
                if (downWays.IndexOf(component) < 0)
                    downWays.Add(component);
        }
    }
}
