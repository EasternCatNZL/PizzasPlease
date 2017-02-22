using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour {

    public int InZone = 0;
    public bool ToppingsFound;
    public GameObject Plane;
    public Material[] Materials;

    public List<GameObject> CorrectToppings;
    public List<GameObject> ToppingsOnSlice;

    private bool Setup = false;
    private GameObject FindGameObject;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if(CheckTopping())
        {
            Plane.GetComponent<MeshRenderer>().material = Materials[2];
        }
        else if(!CheckTopping())
        {
            Plane.GetComponent<MeshRenderer>().material = Materials[0];
        }
	}

    public void SetupComplete()
    {
        Setup = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Setup)
        {
            CorrectToppings.Add(other.gameObject);
            ToppingsOnSlice.Add(other.gameObject);
        }
        else
        {
            ToppingsOnSlice.Add(other.gameObject);
        }
    }

    private bool CheckTopping()
    {
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
