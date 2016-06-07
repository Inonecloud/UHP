using UnityEngine;
using System.Collections;





public class CCoreGame
{


    // global objects
    public CArena Arena;
    public CPuck Puck;
    public CNet NetHome;
    public CNet NetGuest;
    public CPlayer[] PlayerHome = new CPlayer[20];
    public CPlayer[] PlayerGuest = new CPlayer[20];

    // camera object
    CCamera Cam;



    // Use this for initialization
    public void Init()
    {
        Core.mode++;


        //******************************
        // init camera and lights
        Cam = new CCamera();
        Cam.Select(CCamera.GOAL_GUEST);



        //******************************
        // init base components 
        Arena = new CArena();
        Puck = new CPuck();
        NetHome = new CNet();
        NetGuest = new CNet();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i] = new CPlayer();
            PlayerGuest[i] = new CPlayer();
        }



        //******************************
        // load objects
        Arena.Load("arena_omsk");
        Puck.Load("puck");
        NetHome.Load("net");
        NetGuest.Load("net");

        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Load("test");
            PlayerGuest[i].Load("test");
        }



        //******************************
        // set initial positions -home +guests
        //******************************
        NetHome.param.x = -Arena.data.net_pos;
        NetGuest.param.x = Arena.data.net_pos;
        NetHome.param.h = 90;
        NetGuest.param.h = -90;


    }







    // Update is called once per frame
    public void Process()
    {

        // temporary **********
        // random shot
        if (Time.frameCount % 200 == 1)
        {
            //Puck.param.speed = 1f + 5f;
            //Puck.param.dir = 10f * Random.value - 5 + Physics.Angle(Puck.param.x, Puck.param.y, NetHome.param.x, NetHome.param.y);
            //Puck.param.vv = 10f * Random.value;
            //Puck.pos.dir = 45f;
        }
        PlayerHome[0].player.puck = Puck.param;
        PlayerGuest[0].player.puck = Puck.param;
        PlayerHome[0].player.side = 1;
        PlayerGuest[0].player.side = -1;
        PlayerHome[0].player.net_ot = NetGuest.param;






        //=====================================
        // process all objects calculations
        //=====================================
        Puck.Process();
        NetHome.Process();
        NetGuest.Process();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Process( ref Puck.param, NetHome.param, NetGuest.param );
            //PlayerGuest[i].Process( ref Puck.param, NetHome.param, NetGuest.param );
        }






        //=====================================
        // check collisions 
        //=====================================
        Collision.CheckBoardCollision( ref Arena.data, ref Puck.param, 0.1f);
        Collision.CheckNetCollision( ref NetHome.param,ref Puck.param, 0.1f);
        Collision.CheckNetCollision( ref NetGuest.param, ref Puck.param, 0.1f);
        for (int i = 0; i < 1; i++)
        {
            Collision.CheckNetCollisionHeavy(ref NetHome.param, ref PlayerHome[i].param, 0.3f );
            Collision.CheckNetCollisionHeavy(ref NetGuest.param, ref PlayerHome[i].param, 0.3f);
            Collision.CheckNetCollisionHeavy(ref NetHome.param, ref PlayerGuest[i].param, 0.3f);
            Collision.CheckNetCollisionHeavy(ref NetGuest.param, ref PlayerGuest[i].param, 0.3f);
        }


 



        //=====================================
        // process reaction and position objects in the scene
        //=====================================
        Puck.Post();
        NetHome.Post();
        NetGuest.Post();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Post();
            PlayerGuest[i].Post();
        }




        //=====================================
        // camera
        //=====================================
        Cam.Show( ref Arena.data, ref Puck.param, ref NetHome.param, ref NetGuest.param);

    }







    public void OnGUI()
    {
        //float d = Vector2.Distance(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        //float r = Vector2.Angle(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "DEBUG "+(Cam.dir.ToString()) );
    }

}
