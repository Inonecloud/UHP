using UnityEngine;
using System.Collections;

public class SwitchPlayer : MonoBehaviour
{
    private bool select = true;
    public GameObject[] player;
    int counter = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(counter);
        if (Input.GetKeyDown(KeyCode.S))
        {
            select = !select;

            if (select)
            {
                SwitchControl();
                player[++counter].GetComponent<hpcontol>().Activate();

                //++counter;   
            }
            else
            {
                SwitchControl();
                player[++counter].GetComponent<hpcontol>().Activate();
            }
        }

    }

    void SwitchControl()
    {
        if (counter == 0)
            player[player.Length - 1].GetComponent<hpcontol>().Disactivate();
        else if (counter == player.Length - 1)
        {
            counter = 0;
            player[player.Length - 1].GetComponent<hpcontol>().Disactivate();
        }
        else
            player[counter].GetComponent<hpcontol>().Disactivate();
    }
}

