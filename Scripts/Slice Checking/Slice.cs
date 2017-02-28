using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour {

    public int InZone = 0; //number of toppings within the slice
    public bool ToppingsFound; //check for whether if toppings have been found
    public GameObject Plane; //debugging
    public Material[] Materials; //debugging

    //[HideInInspector]
    public List<GameObject> CorrectToppings; //list of correct toppings on this slice
    //[HideInInspector]
    public List<GameObject> ToppingsOnSlice; //list of toppings on the slice currently

    public bool Setup = false; //checks if the setup is complete
    private GameObject FindGameObject; //reference find game objects

	// Use this for initialization
	void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        if (Plane != null)
        {
            if (CheckTopping())
            {
                Plane.GetComponent<MeshRenderer>().material = Materials[2];
            }
            else if (!CheckTopping())
            {
                Plane.GetComponent<MeshRenderer>().material = Materials[0];
            }
        }
	}

    public void BakeSlice()//Confirms the toppings on the pizza
    {
        Setup = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //before setup complete
        if (!Setup)
        {
            //add toppings to the list on slice
            CorrectToppings.Add(other.gameObject);
            ToppingsOnSlice.Add(other.gameObject);
        }
        else
        {
            ToppingsOnSlice.Add(other.gameObject);
        }
    }

    //
    public bool CheckTopping()
    {
        if (CorrectToppings.Count == 0 && ToppingsOnSlice.Count == 0)
            return true;
        if (CorrectToppings.Count != ToppingsOnSlice.Count)
            return false;
        else
        {
            ToppingsFound = false;
            for (int i = 0; i < CorrectToppings.Count; i++)
            {
                FindGameObject = CorrectToppings[i];
                if (!ToppingsOnSlice.Find(isGameObject))
                {
                    return false;
                }
                else
                {
                    ToppingsFound = true;
                }
            }
            
            return ToppingsFound;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindGameObject = other.gameObject;
        if (!Setup)
        {
            CorrectToppings.Remove(CorrectToppings.Find(isGameObject));
            ToppingsOnSlice.Remove(ToppingsOnSlice.Find(isGameObject));
        }
        else
        {
            ToppingsOnSlice.Remove(ToppingsOnSlice.Find(isGameObject));
        }
    }

    private bool isGameObject(GameObject gameObject)
    {

        return (gameObject == FindGameObject);
    }
}
