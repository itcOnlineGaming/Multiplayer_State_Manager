using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayerScript : MonoBehaviour
{
    private Renderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<Renderer>();
    }

    public void SetColour(Color colour)
    {
        if (spriteRenderer != null)
        {
            Debug.Log($"Changing color of {gameObject.name} to {colour}");
            spriteRenderer.material.color = colour;
        }
    }
}
