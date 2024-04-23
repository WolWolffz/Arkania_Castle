using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public List<Way> upWays = new List<Way>();
    public List<Way> downWays = new List<Way>();
    public bool isSelected = false;

    private SpriteRenderer spriteRenderer;
    private Color unselectedColor,
        selectedColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        unselectedColor = spriteRenderer.color;
        selectedColor = new Color(
            unselectedColor.r / 2,
            unselectedColor.g / 2,
            unselectedColor.b / 2
        );
    }

    void Update()
    {
        // Debug.Log(ways.Count.ToString());
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) {
             Select(!isSelected);
        }
    }

    void Select(bool value)
    {
        if (value)
        {
            isSelected = true;
            spriteRenderer.color = selectedColor;
        }
        else
        {
            isSelected = false;
            spriteRenderer.color = unselectedColor;
        }
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
