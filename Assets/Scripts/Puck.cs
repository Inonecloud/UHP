using UnityEngine;
using System.Collections;




public class CPuck  
{

    // global objects
    public GameObject PuckObj;

    // parameters
    public PARAM param;
    public DATA_EQUIPMENT data;

    // sounds
    private AudioSource AudioHit;
    private AudioSource AudioIce;
    // board hit
    public AudioClip snd_board_0;
    public AudioClip snd_board_1;
    public AudioClip snd_board_2;
    public AudioClip snd_glass;
    public AudioClip snd_ice;
    public AudioClip snd_pole;



 

    //=================================================================================================
    // normal loading procedure that loads everything for gameplay
    //=================================================================================================
    public void Load(string dbkey)
    {
        data = Database.LoadEquipment(dbkey);

        PuckObj = Database.LoadGameObject(dbkey, Database.path_equipment + dbkey + "/"+dbkey);
        
        // init audio sources
        AudioHit = PuckObj.AddComponent<AudioSource>();
        AudioIce = PuckObj.AddComponent<AudioSource>();
        // load sounds
        snd_board_0 = Resources.Load("sounds/puck_board_0", typeof(AudioClip)) as AudioClip;
        snd_board_1 = Resources.Load("sounds/puck_board_1", typeof(AudioClip)) as AudioClip;
        snd_board_2 = Resources.Load("sounds/puck_board_2", typeof(AudioClip)) as AudioClip;
        snd_glass = Resources.Load("sounds/puck_glass", typeof(AudioClip)) as AudioClip;
        snd_ice = Resources.Load("sounds/puck_ice", typeof(AudioClip)) as AudioClip;
        snd_pole = Resources.Load("sounds/puck_pole", typeof(AudioClip)) as AudioClip;
    }



    //=================================================================================================
    // calculate physics for object
    //=================================================================================================
    public void Process()
    {
        Physics.PuckPhysics(ref param);
    }




    //=================================================================================================
    // play the sounds
    //=================================================================================================
    public void Sound()
    {
        // set up audio environment
        AudioHit.spatialBlend = 0.5f;
        AudioIce.spatialBlend = 0.5f;
        AudioHit.pitch = 0.8f + 0.4f * Random.value;
        AudioIce.pitch = 0.8f + 0.4f * Random.value;
        // play sounds
        if (param.object_event == Event.BOARD) AudioHit.PlayOneShot(snd_board_0, param.speed / 30.0f);
        if (param.object_event == Event.BOARD_HIT && param.speed <= 20) AudioHit.PlayOneShot(snd_board_1, param.speed / 30.0f);
        if (param.object_event == Event.BOARD_HIT && param.speed > 20) AudioHit.PlayOneShot(snd_board_2, param.speed / 30.0f);
        if (param.object_event == Event.GLASS) AudioHit.PlayOneShot(snd_glass, param.speed / 30.0f);
        if (param.object_event == Event.PUCK_ICE) AudioIce.PlayOneShot(snd_ice, param.vv / 30.0f);
        if (param.object_event == Event.POLE) AudioIce.PlayOneShot(snd_pole, param.speed / 30.0f);
    }



    //=================================================================================================
    // put player object in the scene with reqwuired coordinates and rotations
    //=================================================================================================
    public void Post()
    {
        PuckObj.transform.position = new Vector3(param.x, param.alt + 0.01f, param.y);
        PuckObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }
 
}