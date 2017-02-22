using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour {

    public int InZone = 0;
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
        print("New Item Entered Zone");
        InZone++;
        if (!Setup)
        {
            CorrectToppings.Add(other.gameObject);
            print(CorrectToppings.Count);
            ToppingsOnSlice.Add(other.gameObject);
            print(ToppingsOnSlice.Count);
        }
        else
        {
            ToppingsOnSlice.Add(other.gameObject);
            print(ToppingsOnSlice.Count);
        }
    }

    private bool CheckTopping()
    {
        if (CorrectToppings.Count != ToppingsOnSlice.Count)
            return false;
        else
        {
            return true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FindGameObject = other.gameObject;
        ToppingsOnSlice.Remove(ToppingsOnSlice.Find(isGameObject));

        print("New Item Exited Zone");
        InZone--;
    }

    private bool isGameObject(GameObject gameObject)
    {

        return (gameObject == FindGameObject);
    }
}
