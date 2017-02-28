using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour {

    public int DrugCount = 0;

    // Use this for initialization
    void Start()
    {
        GameObject[] Drugs = GameObject.FindGameObjectsWithTag("Drugs");
        foreach (GameObject Drug in Drugs)
        {
            DrugCount++;
        }
    }
}
