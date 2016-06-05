using UnityEngine;
using System.Collections;




public class CoreError
{
    public static string Error="";

    public static void Add( string msg )
    {
        if( Error=="" ) Error = msg;
    }


    public static void Show()
    {
        Rect windowRect = new Rect ((Screen.width - 300)/2, (Screen.height - 100)/2, 300, 100);
        GUI.Box(windowRect, "\n"+Error );
        if (GUI.Button(new Rect(windowRect.x+100, windowRect.y+50, 100, 30), "Close"))  Application.Quit();
    }

}