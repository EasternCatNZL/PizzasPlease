using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour {

    private Vector3 startPos; //where click landed
    private Transform clickedObject = null; //reference to the clicked object
    private Vector3 offset; //offset held to prevent object from snapping
    private Plane plane; //plane aligned with camera to assist in dragging objects

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //align plane to camera
        plane.SetNormalAndPosition(Camera.main.transform.forward, transform.position); 
        //cast ray form camera based on mouse position on screen
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        plane.Raycast(ray, out distance);
        //get offset to prevent object from snapping
        offset = transform.position - ray.GetPoint(distance);
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        plane.Raycast(ray, out distance);
        Vector3 pos = ray.GetPoint(distance);
        transform.position = pos + offset;
    }
}
