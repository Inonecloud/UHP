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

    // game controller
    CController Controller;


    // gameplay vars
    public static int goal = 0; // -1 0 1


    // Use this for initialization
    public void Init()
    {
        Core.mode++;


        //******************************
        // init camera 
        Cam = new CCamera();
        Cam.Select(CCamera.POV);


        //******************************
        // init controllers
        Controller = new CController();


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





        SetupShootout();
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
        PlayerHome[0].user_control = true;
        PlayerHome[0].player.puck = Puck.param;
        PlayerGuest[0].player.puck = Puck.param;
        PlayerHome[0].player.side = 1;
        PlayerGuest[0].player.side = -1;
        PlayerHome[0].player.net_ot = NetGuest.param;
        // temporary **********






        //=====================================
        // process all objects calculations
        //=====================================
        Puck.Process();
        NetHome.Process();
        NetGuest.Process();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Process( Controller, ref Puck.param, NetHome.param, NetGuest.param );
            //PlayerGuest[i].Process( ref Puck.param, NetHome.param, NetGuest.param );
            if (PlayerHome[i].user_control) Cam.user_head = PlayerHome[i].PlayerHead;
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
            Collision.CheckBoardCollision(ref Arena.data, ref PlayerGuest[i].param, 0.3f);
            Collision.CheckBoardCollision(ref Arena.data, ref PlayerGuest[i].param, 0.3f);
        }




        //=====================================
        // sound procedures
        //=====================================
		Arena.Sound();
        Puck.Sound();
        NetHome.Sound();
        NetGuest.Sound();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Sound();
            PlayerGuest[i].Sound();
        }



        //=====================================
        // position objects in the scene
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
        // gameplay 
        //=====================================
		if (goal != 0) goal = 0;
        if (Collision.CheckGoal(NetHome.param, Puck.param)) goal = 1;
        if (Collision.CheckGoal(NetGuest.param, Puck.param)) goal = -1;
        if (goal == 1) Arena.GoalHome();
        if (goal == -1) Arena.GoalGuest();
		if (goal != 0)
			SetupShootout ();






        //=====================================
        // camera
        //=====================================
        Controller.CameraControl(ref Cam);
        Cam.Show(ref Arena.data, ref Puck.param, ref NetHome.param, ref NetGuest.param);

    }










    //=====================================
    // shootout setup
    //=====================================
    public void SetupShootout()
    {
        PlayerHome[0].param.x = -5f;
        PlayerHome[0].param.y = 0;
        PlayerHome[0].param.h = 90;
        PlayerHome[0].param.dir = 90;
        PlayerHome[0].param.tgt_dir = 90;

        PlayerGuest[0].param.x = 24.5f;
        PlayerGuest[0].param.y = 0;
        PlayerGuest[0].param.h = -90;
        PlayerGuest[0].param.dir = -90;
        PlayerGuest[0].param.tgt_dir = -90;

        Puck.param.x = 0;
        Puck.param.y = 0;
		Puck.param.speed = 0;
    }













    public void OnGUI()
    {
        //float d = Vector2.Distance(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        //float r = Vector2.Angle(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "DEBUG " + (PlayerHome[0].data.action_count.ToString()) +" "+(PlayerHome[0].param.object_event.ToString()));
    }

}
