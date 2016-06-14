using UnityEngine;
using System.Collections;




public class CPlayer
{


    // global objects
    public GameObject PlayerObj;
    public GameObject StickObj;
    public GameObject HelmetObj;
    public GameObject GloveLeftObj;
    public GameObject GloveRightObj;


    // joints and bones
    public Transform PlayerHead;

    // animation 
    public Animation PlayerAnim;


    // parameters
    public PARAM param;
    public DATA_PLAYER data;
    public AI_PLAYER player;
    public AI_PLAYER goalkeeper;


    // player control
    public bool user_control; // this player is under the user control

    //goalkeeper flag


    // sounds
    private AudioSource AudioStick;
    private AudioSource AudioSkates;
    // board hit
    public AudioClip snd_stick_ice;
    public AudioClip snd_shot_snap;
    public AudioClip snd_shot_slap;



    //=================================================================================================
    // we use this method if we need to load only player data from database without meshes and textures
    // suitable for main menu and skills editor etc
    //=================================================================================================
    public void Init(string dbkey)
    {
        data = Database.LoadPlayer(dbkey);
    }




    //=================================================================================================
    // normal loading procedure that loads everything for gameplay
    //=================================================================================================
    public void Load(string dbkey)
    {
        data = Database.LoadPlayer(dbkey);

        PlayerObj = Database.LoadGameObject(dbkey, Database.path_player + dbkey + "/player");


        StickObj = Database.LoadGameObject(dbkey, Database.path_equipment + "stick/stick");
        BoneAttach( ref StickObj, PlayerObj, "Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Right/Player_Shoulder_Right/Player_Forearm_Right/Player_Wrist_Right" );
        StickObj.transform.localEulerAngles = new Vector3(90.0f, 0.0f, -90.0f);
        StickObj.transform.localPosition = new Vector3(0.0f, 0.8f, 0.0f);
        
        GloveLeftObj = Database.LoadGameObject(dbkey, Database.path_equipment + "gloveleft/gloveleft");
        BoneAttach(ref GloveLeftObj, PlayerObj, "Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Left/Player_Shoulder_Left/Player_Forearm_Left/Player_Wrist_Left");
        
        GloveRightObj = Database.LoadGameObject(dbkey, Database.path_equipment + "gloveright/gloveright");
        BoneAttach(ref GloveRightObj, PlayerObj, "Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Right/Player_Shoulder_Right/Player_Forearm_Right/Player_Wrist_Right");

        PlayerAnim = PlayerObj.AddComponent<Animation>();
        //PlayerAnim.clip = Resources.Load("players/animation/humanoididle", typeof(AnimationClip)) as AnimationClip;
        //PlayerAnim.AddClip(Resources.Load("players/animation/humanoidrun", typeof(AnimationClip)) as AnimationClip, "run");
        //PlayerAnim.AddClip(Resources.Load("players/animation/Humanoidwalk", typeof(AnimationClip)) as AnimationClip, "walk");
        //PlayerAnim.enabled = true;


        PlayerObj.transform.Find("Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Left/Player_Shoulder_Left").localEulerAngles = new Vector3(0.0f, -60.0f, 00.0f);
        PlayerObj.transform.Find("Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Right/Player_Shoulder_Right").localEulerAngles = new Vector3(0.0f, -70.0f, 20.0f);
        PlayerObj.transform.Find("Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Clav_Right/Player_Shoulder_Right/Player_Forearm_Right/Player_Wrist_Right").localEulerAngles = new Vector3(10.0f, 0.0f, -40.0f);

        PlayerHead = PlayerObj.transform.Find("Player_Pelvis/Player_Spine_01/Player_Spine02/Player_Spine03/Player_Neck/Player_Head");

        // init audio sources
        AudioStick = PlayerObj.AddComponent<AudioSource>();
        AudioSkates = PlayerObj.AddComponent<AudioSource>();
        // load sounds
        snd_stick_ice = Resources.Load("sounds/stick_ice", typeof(AudioClip)) as AudioClip;
        snd_shot_snap = Resources.Load("sounds/shot_snap", typeof(AudioClip)) as AudioClip;
        snd_shot_slap = Resources.Load("sounds/shot_slap", typeof(AudioClip)) as AudioClip;
    }






    //=================================================================================================
    // either we process keyboard or mous or gamepad inputs and set actions for player
    // or let ai do this for us
    //=================================================================================================
    public void Process(CController Controller, ref PARAM puck, PARAM net_home, PARAM net_guest)
    {
        param.object_event = 0; // erase the flag

        if (user_control) Controller.PlayerControl( ref data ); // process user control inputs
        if (!user_control) AIPlayer.ProcessAI( ref param, ref player); // ai predictions and desicion making
        AIPlayer.PuckControl( ref param, ref data, ref player, ref puck); // actions with puck
        Physics.PlayerPhysics(ref param, ref data); // moooove

    }




    //=================================================================================================
    // play the sounds
    //=================================================================================================
    public void Sound()
    {
        // set up audio environment
        AudioStick.spatialBlend = 0.5f;
        AudioSkates.spatialBlend = 0.5f;
       // AudioStick.pitch = 0.8f + 0.4f * Random.value;
        AudioSkates.pitch = 0.8f + 0.4f * Random.value;
        // play sounds
        if (param.object_event == Event.STICK_ICE) AudioStick.PlayOneShot(snd_stick_ice, 0.01f + 0.3f * Random.value); 
        if (param.object_event == Event.SNAPSHOT) AudioStick.PlayOneShot(snd_shot_snap, 0.6f + 0.4f * Random.value);
        if (param.object_event == Event.SLAPSHOT) AudioStick.PlayOneShot(snd_shot_slap, 0.6f + 0.4f * Random.value);
    }



    //=================================================================================================
    // put player object in the scene with reqwuired coordinates and rotations
    //=================================================================================================
    public void Post()
    {
        PlayerObj.transform.position = new Vector3(param.x, param.alt, param.y);
        PlayerObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }







    public void BoneAttach(ref GameObject obj, GameObject parent, string bone)
    {
        obj.transform.parent = parent.transform.Find( bone );
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }
 

}