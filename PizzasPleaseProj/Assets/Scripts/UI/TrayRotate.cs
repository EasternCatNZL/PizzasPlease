using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayRotate : MonoBehaviour
{

    public float AnimLength = 3.0f;

    private bool Opened = false;
    private bool Moving = false;

    public float start = 0.0F;
    public float end = 90F;

    // starting value for the Lerp    
    static float t = 0.0f;

    void Update()
    {
        if (Moving)
        {
            // animate the position of the game object...
            if (Opened)
            {
                gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90 * t);
            }
            else if (!Opened)
            {
                gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90 + 90 * t);
            }

            // .. and increate the t interpolater
            t += AnimLength * Time.deltaTime;

            // now check if the interpolator has reached 1.0
            // and swap end and start so game object moves
            // in the opposite direction.
            if (t > 1.0f)
            {
                if (Opened)
                    gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -90);
                else if (!Opened)
                    gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);

                Moving = false;
            }
        }
    }

    public void OnClick()
    {
        if (!Moving)
        {
            if (!Opened)
            {
                t = 0.0f;
                Moving = true;
                Opened = true;
            }
            else if (Opened)
            {
                t = 0.0f;

                Moving = true;
                Opened = false;
            }
        }
    }
}
