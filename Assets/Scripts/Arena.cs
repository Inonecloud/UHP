using UnityEngine;
using System.Collections;




public class CArena  
{

    // objects
    public GameObject ArenaObj;
    public GameObject IceObj;

    // lights
    public Light LightHome;
    public Light LightGuest;
    int light_timer;


    // parameters
    public DATA_ARENA data;



    public void Init(string dbkey)
    {
        data = Database.LoadArena(dbkey);
     }


    //=================================================================================================
    // normal loading procedure that loads everything for gameplay
    //=================================================================================================
    public void Load(string dbkey)
    {
        // load params from db first
        data = Database.LoadArena(dbkey);

        // load arena object
        ArenaObj = Database.LoadGameObject( dbkey, Database.path_arena+dbkey+"/arena");
 
        //IceObj = Object.Instantiate(Resources.Load("meshes/ice", typeof(GameObject))) as GameObject;

        LightHome = GameObject.Find("Light Home Proj").GetComponent<Light>();
        LightGuest = GameObject.Find("Light Guest Proj").GetComponent<Light>();

    }





    //=================================================================================================
    // calculate physics for objects
    //=================================================================================================
    public void Process()
    {
    }




    //=================================================================================================
    // play the sounds
    //=================================================================================================
    public void Sound()
    {
		if (CCoreGame.goal == 1 || CCoreGame.goal == -1)
			PlayHorn ();
    }




    //=================================================================================================
    // put player object in the scene with reqwuired coordinates and rotations
    //=================================================================================================
    public void Post()
    {
    }





    public void GoalHome()
    {
        light_timer++;
        if (light_timer > 15 && light_timer<100)
        {
            if (Time.time % 0.2 < 0.1) LightHome.enabled = true; else LightHome.enabled = false;
        }
        else
        {
            LightHome.enabled = false;
        }
    }

    public void GoalGuest()
    {
       light_timer++;
        if (light_timer > 15 && light_timer<100)
        {
            if (Time.time%0.2 < 0.1) LightGuest.enabled = true; else LightGuest.enabled = false;
        }
        else
        {
            LightGuest.enabled = false;
        }
    }
		
	private void PlayHorn() {
			AudioClip arenaHorn = Resources.Load ("sounds/goal_easy_horn", typeof(AudioClip)) as AudioClip;
			AudioSource arenaHornSrc = ArenaObj.AddComponent<AudioSource> ();
			arenaHornSrc.spatialBlend = 0.7f;
			arenaHornSrc.pitch = 0.9f;
			arenaHornSrc.PlayOneShot (arenaHorn, 0.1f);
	}

    




}