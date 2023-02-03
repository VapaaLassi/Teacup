using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public AnimationCurve movementCurve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float speed = 5;

    bool moving = false;
    float ongoingTime;

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        float curveMultiplier = 1;

        //print("x: " + x + " y: " + y);

        if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
        {
            if (!moving)
            {
                moving = true;
                ongoingTime = 0;
            }
            else
            {
                ongoingTime += Time.deltaTime;
                curveMultiplier = movementCurve.Evaluate((ongoingTime % 6f)/3);
                //print("curveMultiplier " + curveMultiplier);
            }
        } else
        {
            if (moving)
            {
                moving = false;
            }
        }

        //print(moving);

        float speedMultiplier = speed * Time.deltaTime * curveMultiplier;

        //print(speedMultiplier);

        transform.Translate(x * speedMultiplier, y * speedMultiplier,0);
    }
}
