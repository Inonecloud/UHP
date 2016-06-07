using UnityEngine;
using System.Collections;




public class CPlayer
{


    // global objects
    public GameObject PlayerObj;
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

        PlayerAnim = PlayerObj.AddComponent<Animation>();
        //PlayerAnim.clip = Resources.Load("players/animation/humanoididle", typeof(AnimationClip)) as AnimationClip;
        //PlayerAnim.AddClip(Resources.Load("players/animation/humanoidrun", typeof(AnimationClip)) as AnimationClip, "run");
        //PlayerAnim.AddClip(Resources.Load("players/animation/Humanoidwalk", typeof(AnimationClip)) as AnimationClip, "walk");
        //PlayerAnim.enabled = true;

    }





    public void Process( ref PARAM puck, PARAM net_home, PARAM net_guest)
    {
        AIPlayer.ProcessAI(ref param, ref player); // predictions and desicion making
        AIPlayer.PuckControl(ref param, ref player, ref puck); // actions with puck
        Physics.PlayerPhysics(ref param); // moooove

        // temporary **********
        player.puck = puck;

    }



 



    public void Post()
    {
        PlayerObj.transform.position = new Vector3(param.x, param.alt, param.y);
        PlayerObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }







 

}