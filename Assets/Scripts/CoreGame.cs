using UnityEngine;
using System.Collections;





public class CCoreGame
{


    // global objects
    CArena Arena;
    CPuck Puck;
    CNet NetHome;
    CNet NetGuest;
    CPlayer[] PlayerHome = new CPlayer[20];
    CPlayer[] PlayerGuest = new CPlayer[20];






    // Use this for initialization
    public string Init()
    {
        Core.mode++;


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
        // set initial positions
        //******************************
        NetHome.param.x = Arena.data.net_pos;
        NetGuest.param.x = -Arena.data.net_pos;
        NetHome.param.h = -90;
        NetGuest.param.h = 90;


        return "";
    }







    // Update is called once per frame
    public void Process()
    {


        // random shot
        if (Time.frameCount % 200 == 1)
        {

            //Puck.param.speed = 1f + 5f;
            //Puck.param.dir = 10f * Random.value - 5 + Physics.Angle(Puck.param.x, Puck.param.y, NetHome.param.x, NetHome.param.y);
            //Puck.param.vv = 10f * Random.value;

            //Puck.pos.dir = 45f;
        }








        //******************************
        // process all object calculations
        Puck.Process();
        for (int i = 0; i < 1; i++)
        {
            Puck.param = PlayerHome[i].Process( Puck.param, NetHome.param, NetGuest.param );
            Puck.param = PlayerGuest[i].Process( Puck.param, NetHome.param, NetGuest.param );
        }






        //******************************
        // check collisions with sides
        Puck.param = Arena.CheckBoardCollision(Puck.param,0.5f);
        Puck.param = NetHome.CheckNetCollision(Puck.param, 0.2f);
        Puck.param = NetGuest.CheckNetCollision(Puck.param, 0.2f);


        // check collisions with people
        //float d = Vector2.Distance(new Vector2(Puck.param.x, Puck.param.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        //if (d < 0.9)
        //{
        //    Puck.pos.speed = 5f + 1f * Random.value;
        //    Puck.pos.dir = Me.transform.eulerAngles.y;
        //    // hit
        //    if (Puck.pos.x > 12 || Puck.pos.x < -12)
        //    {
        //        Puck.pos.speed = 1f + 20f * Random.value;
        //        Puck.pos.vv = 10f * Random.value;
        //        Puck.pos.dir = Me.transform.eulerAngles.y;
        //    }
        //}






        //******************************
        // position objects in the scene
        Puck.Post();
        NetHome.Post();
        NetGuest.Post();
        for (int i = 0; i < 1; i++)
        {
            PlayerHome[i].Post();
            PlayerGuest[i].Post();
        }




        //******************************
        // camera
        Camera.main.transform.LookAt(Puck.PuckObj.transform);


    }







    public void OnGUI()
    {
        //float d = Vector2.Distance(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        //float r = Vector2.Angle(new Vector2(Puck.pos.x, Puck.pos.y), new Vector2(Me.transform.position.x, Me.transform.position.z));
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), (Puck.param.speed.ToString()) );
    }

}
