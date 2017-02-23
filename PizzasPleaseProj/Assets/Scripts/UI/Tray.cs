using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour {

    // animate the game object from -1 to +1 and back
    public  bool opened = false;
    public bool moving = false;

    public float start = 0.0F;
    public float end = 90F;
    public float speed = 0.5f;

    // starting value for the Lerp    
    static float t = 0.0f;

    void Update()
    {
        if (moving)
        {
            // animate the position of the game object...
            transform.position = new Vector3(Mathf.Lerp(start, end, t), transform.position.y, transform.position.z);

            // .. and increate the t interpolater
            t += speed * Time.deltaTime;

            // now check if the interpolator has reached 1.0
            // and swap end and start so game object moves
            // in the opposite direction.
            if (t > 1.0f)
            {
                moving = false;
            }
        }
    }

    public void OnClick()
    {
        if (!moving)
        {
            if (!opened)
            {
                if (start != 0.0f)
                {
                    float temp = end;
                    end = start;
                    start = temp;
                }
                t = 0.0f;
                moving = true;
                opened = true;
            }
            else if(opened)
            {
                float temp = start;
                start = end;
                end = temp;
                t = 0.0f;

                moving = true;
                opened = false;
            }
        }
    }
}
