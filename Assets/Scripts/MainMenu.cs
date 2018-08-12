using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public string buttonText;

	public void PlayNow()
    {
        Application.LoadLevel(1);
    }

    public void Settings()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
}
