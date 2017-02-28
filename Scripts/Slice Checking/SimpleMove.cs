using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour {

    public int CubeID;
    private bool CanMove = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("1"))
        {
            if(CubeID == 1)
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
            }
        }
        else if(Input.GetKey("2"))
        {
            if(CubeID == 2)
            {
                CanMove = true;
            }
            else
            {
                CanMove = false;
            }
        }
        if (CanMove)
        {
            Vector3 Movement = new Vector3();
            if (Input.GetKey("up"))
            {
                Movement += new Vector3(0.0f, 0.1f, 0.0f);
            }
            if (Input.GetKey("down"))
            {
                Movement += new Vector3(0.0f, -0.1f, 0.0f);
            }
            if (Input.GetKey("left"))
            {
                Movement += new Vector3(-0.1f, 0.0f, 0.0f);
            }
            if (Input.GetKey("right"))
            {
                Movement += new Vector3(0.1f, 0.0f, 0.0f);
            }
            this.transform.position += Movement;
        }
    }
}
