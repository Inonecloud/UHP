using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

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
