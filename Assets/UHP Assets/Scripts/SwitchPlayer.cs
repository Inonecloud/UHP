using UnityEngine;
using System.Collections;

public class SwitchPlayer : MonoBehaviour
{
    private bool select = true; //players switcher
    public GameObject[] player;
    int counter = 0; //player number in Array

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(counter);
        if (Input.GetKeyDown(KeyCode.S))
        {
            select = !select;
            if (select)
            {
                SwitchControl();  
            }
            else
            {
                SwitchControl(); 
            }
            counter++;
        }

    }

    //This method call methods Activate() and Disactivate()  from hpcontol class
    void SwitchControl()
    {
        if (counter == 0) // First Player
        {
            Debug.Log("KEK");
            player[player.Length - 1].GetComponent<hpcontol>().Disactivate();
            Debug.Log("Деативировали 4");
            counter = 0;
            player[counter].GetComponent<hpcontol>().Activate();
            Debug.Log("Ативировали " + (counter));
        }
        else if (counter == player.Length - 1) //Last player
        {
            Debug.Log("LOL");
            player[counter - 1].GetComponent<hpcontol>().Disactivate();
            Debug.Log("Деативировали " + (counter - 1));
            player[counter].GetComponent<hpcontol>().Activate();
            Debug.Log("Ативировали " + (counter));
            
            counter = -1; //Ad-hoc
           // counter = 0;
            Debug.Log(counter);
        }
        else {
            player[counter - 1].GetComponent<hpcontol>().Disactivate();
            Debug.Log("Деативировали " + (counter-1));
            player[counter].GetComponent<hpcontol>().Activate();
            Debug.Log("Ативировали " + (counter));
            //counter++;
        }
        //player[counter].GetComponent<hpcontol>().Activate();
        //Debug.Log("Ативировали " + (counter));
        //Debug.Log("Length " + player.Length);
    }
}

