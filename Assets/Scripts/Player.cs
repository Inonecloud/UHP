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

    public Animation PlayerAnim;


    // parameters
    public PARAM param;
    public DATA_PLAYER data;
    public AI_PLAYER player;



    public void Init( string dbkey )
    {
        data = Database.LoadPlayer(dbkey);
    }




    public void Load( string dbkey )
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
   
    }





    public void Process( ref PARAM puck, PARAM net_home, PARAM net_guest)
    {
        AIPlayer.ProcessAI(ref param, ref player); // predictions and desicion making
        AIPlayer.PuckControl(ref param, ref player, ref puck); // actions with puck
        Physics.PlayerPhysics(ref param); // moooove


    }



 



    public void Post()
    {
        PlayerObj.transform.position = new Vector3(param.x, param.alt, param.y);
        PlayerObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);

        // flush the collision flag
        param.collision = 0;
    }







    public void BoneAttach(ref GameObject obj, GameObject parent, string bone)
    {
        obj.transform.parent = parent.transform.Find( bone );
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;
    }
 

}