using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public List<Arena> arenas = new List<Arena>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            var component = child.GetComponent<Arena>();

            if (component != null)
                arenas.Add(component);
        }
    }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter2D(Collision2D collision) { }
}
