using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLayering : MonoBehaviour
{

    private SpriteRenderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
        UpdateForegrounds(0);
    }

    public void UpdateForegrounds(int value)
    {
        foreach (SpriteRenderer item in renderers)
        {
            item.sortingOrder = value;
        }
    }
}
