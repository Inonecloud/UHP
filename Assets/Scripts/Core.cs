using UnityEngine;
using System.Collections;



public class Core : MonoBehaviour {


	// this is our butiful skin 
    public GUISkin skin;

    // currently active mode
    public static int mode;
    // mode values
    public const int MODE_MAIN_MENU = 0;
    public const int MODE_GAME_INIT = 1;
    public const int MODE_GAME_PLAY = 2;

    
    CCoreMenu Menu; // main menu class
    CCoreGame Game; // gameplay class
    


	// Use this for initialization
	void Start () {
        mode = MODE_GAME_INIT; // ******** temporary for testing ************
        Menu = new CCoreMenu();
        Game = new CCoreGame();
    }


    
    
    
    
    
    // Update is called once per frame
	void Update () {

        // stop executing if we have experienced an error somewhere
        if (CoreError.Error != "") return; 

        if (mode == MODE_MAIN_MENU) Menu.Process(); 
 
        // load and process normal gameplay
        if (mode == MODE_GAME_INIT) Game.Init(); 
        if (mode == MODE_GAME_PLAY) Game.Process();

    }







    void OnGUI()
    {
        GUI.skin = skin;
        if (CoreError.Error != "") CoreError.Show(); // show error dialog
        if (mode == MODE_MAIN_MENU) Menu.OnGUI();
        if (mode == MODE_GAME_PLAY) Game.OnGUI();
    }

}
