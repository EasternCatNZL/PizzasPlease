using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float RoundLength = 60.0f; //timer for round
    public int DrugCount = 0; //Number of drugs on pizza
    public Text OutputScreen; //reference to text in ui for messages
    public GameObject[] Pizzas; //array of possible pizzas

    private int PizzaIndex = 0; //index of current pizze
    private GameObject CurrentPizza; //reference to current pizze
    private GameObject Timer; //reference to timer
    public List<GameObject> Slices; //list of slices on pizza
    //Scoring
    public int TotalPizzas; //total pizzas checked
    public int WrongPizzas; //total pizzas incorrect
    public int WrongSlices; //total slices incorrect

    // Use this for initialization
    void Start()
    {
        Timer = GameObject.Find("Timer");
        if (Timer == null)
        {
            Debug.Log("No \"Timer\" GameObject was found.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //setup func for one round, till timer finishes
    public void StartRound()
    {
        Timer.GetComponent<gameTimer>().SetTimer(RoundLength);
        Timer.GetComponent<gameTimer>().StartTimer();
        NextPizza();
    }

    //checks the pizza once complete
    public void PizzaCheck(bool Legit)
    {

        //increment the number of pizzas checked so far
        TotalPizzas++;
        //check the slices individually
        WrongSlices = SliceCheck();
        //check if the pizza was checked correctly
        if (!DrugCheck(Legit) || WrongSlices > 0)
        {
            //if wrong, increment to incorrectly checked pizzas
            WrongPizzas++;
            WrongSlices = 0;
        }
             
        //if have not yet checked all pizzas in round, go to next pizza
        if (PizzaIndex < Pizzas.Length)
        {
            NextPizza();
        }
        //else end the game
        else
        {
            //End the game and show score.
        }
    }

    //takes player input and checks if the player is correct
    private bool DrugCheck(bool Legit)
    {
        GameObject[] Drugs = GameObject.FindGameObjectsWithTag("Drugs");
        DrugCount = 0;
        foreach (GameObject Drug in Drugs)
        {
            DrugCount++;
        }
        if (Legit && DrugCount > 0)//Passed a drugged pizza
        {
            return false;
        }
        else if (Legit && DrugCount <= 0)//Passed a drug-free pizza
        {
            return true;
        }
        else if (!Legit && DrugCount > 0)//Tossed a drugged pizza
        {
            return true;
        }
        else if (!Legit && DrugCount <= 0)//Tossed a perfectly fine pizza
        {
            return false;
        }
        else//Heck, something happened and you probably failed everything doing it
        {
            return false;
        }
    }

    //checks the slices on the pizza, and increments number by however many were present in the current pizza
    private int SliceCheck()
    {  
        int WrongSlices = 0;
        //check for each slice on pizza
        foreach(GameObject Slice in Slices)
        {
            //if slice has a drug, increment
            if(!Slice.GetComponent<Slice>().CheckTopping())
            {
                WrongSlices++;
            }
        }
        return WrongSlices;
    }

    //get the next pizza
    void NextPizza()
    {
        if (CurrentPizza != null)
        {
            Destroy(CurrentPizza);
        }
        Slices.Clear();
        
        Instantiate(Pizzas[PizzaIndex], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        CurrentPizza = GameObject.FindWithTag("Pizza");
        for(int i = 1; i < 13; ++i)
        {
            string index = i.ToString();
            Slices.Add(GameObject.Find("PizzaSlice" + index));
        }
        //foreach(GameObject Slice in Slices)
        //{
        //    Slice.GetComponent<Slice>().BakeSlice();
        //}
        StartCoroutine(BakeSlices());
        PizzaIndex++;
    }

    IEnumerator BakeSlices()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject Slice in Slices)
        {
            Slice.GetComponent<Slice>().BakeSlice();
        }
    }
}
