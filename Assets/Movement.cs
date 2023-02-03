using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float speed = 10;

    // Update is called once per frame
    void Update()
    {
        float speedMultiplier = speed * Time.deltaTime;

        transform.Translate(Input.GetAxis("Horizontal") * speedMultiplier, Input.GetAxis("Vertical") * speedMultiplier,0);
    }
}
