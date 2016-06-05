using UnityEngine;
using System.Collections;



public class Core : MonoBehaviour {


	// global objects
    public GUISkin skin;

    public static int mode;
    
    CCoreMenu Menu;
    CCoreGame Game;
    


	// Use this for initialization
	void Start () {
        mode = 1;
        Menu = new CCoreMenu();
        Game = new CCoreGame();
    }


    
    
    
    
    
    // Update is called once per frame
	void Update () {

        // stop executing if we have experienced an error somewhere
        if (CoreError.Error != "") return; 

        if (mode == 0) Menu.Process(); 
 
        // load and process normal gameplay
        if (mode == 1) Game.Init(); 
        if (mode == 2) Game.Process();

    }







    void OnGUI()
    {
        GUI.skin = skin;
        if (CoreError.Error != "") CoreError.Show();
        if (mode == 0) Menu.OnGUI();
        if (mode == 2) Game.OnGUI();
    }

}
