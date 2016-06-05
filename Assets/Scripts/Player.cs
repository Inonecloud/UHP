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



    public PARAM Process( PARAM puck, PARAM net_home, PARAM net_guest)
    {
        param = AIPlayer.ProcessAI(param, player);
        puck = AIPlayer.PuckControl(param, player, puck);

 
        param.x += param.speed * Mathf.Sin(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        param.y += param.speed * Mathf.Cos(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        param.vv -= 9.81f * Time.deltaTime;
        //pos.alt += pos.vv * Time.deltaTime;
        //if (pos.alt < 0.01) pos.alt = 0f;

        //pos.p += pos.rp * Time.deltaTime;
        //pos.b += pos.rb * Time.deltaTime;
        //pos.h += pos.rh * Time.deltaTime;


        return puck;
    }



 



    public void Post()
    {


        PlayerObj.transform.position = new Vector3(param.x, param.alt, param.y);
        PlayerObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }







 

}