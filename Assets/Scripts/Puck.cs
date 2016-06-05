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



    public void Init(string dbkey)
    {
        data = Database.LoadEquipment(dbkey);
    }


    public void Load( string dbkey )
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



    public void Process()
    {
        if (param.alt < 0.01)
        {
            param.speed = param.speed * 0.995f;
            param.p *= 0.45f;
            param.b *= 0.45f;
            param.h *= 0.8f;
        }

        param = Physics.Move(param);
    }




    public void Post()
    {
        // set up audio environment
        AudioHit.spatialBlend = 0.5f;
        AudioIce.spatialBlend = 0.5f;
        AudioHit.pitch = 0.8f + 0.4f * Random.value;
        AudioIce.pitch = 0.8f + 0.4f * Random.value;
        // play sounds
        if (param.collision == 1) AudioHit.PlayOneShot(snd_board_0, param.speed / 30.0f);
        if (param.collision == 2 && param.speed <= 20) AudioHit.PlayOneShot(snd_board_1, param.speed / 30.0f);
        if (param.collision == 2 && param.speed > 20) AudioHit.PlayOneShot(snd_board_2, param.speed / 30.0f);
        if (param.collision == 10) AudioHit.PlayOneShot(snd_glass, param.speed / 30.0f);
        if (param.collision == 99) AudioIce.PlayOneShot(snd_ice, param.vv / 30.0f);
        if (param.collision == 98) AudioIce.PlayOneShot(snd_pole, param.speed / 30.0f);

        PuckObj.transform.position = new Vector3(param.x, param.alt + 0.01f, param.y);
        PuckObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);

        // flush the collision flag
        param.collision = 0;
    }
 
}