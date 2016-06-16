using UnityEngine;
using System.Collections;



public class CCamera
{
    // camera types
    public static int TV = 9;
    public static int NET_HOME = 10;
    public static int NET_GUEST = 11;
    public static int GOAL_HOME = 12;
    public static int GOAL_GUEST = 13;
    public static int PLAY = 14;
    public static int PLAY_FOLLOW = 15;
    public static int POV = 16;

    // current active camera
    public static int type;


    // camera parameters
    public float dir; // direction change
    public float pitch; // pitch change


    // look at player for play action cameras
    public Transform user_head; // player head


    public void Select(int seltype)
    {
        type = seltype;
    }



    public void Show( ref DATA_ARENA arena, ref PARAM puck, ref PARAM net_home, ref PARAM net_guest )
    {

        //======================
        // tv cameras
        //======================
        if (type == TV)
        {
            Camera.main.transform.position = new Vector3(0.0f, 12.0f, -25.0f);
            Camera.main.transform.LookAt(new Vector3(puck.x, 0, -5.0f + puck.y*(Mathf.Abs(puck.x) / 30.0f)));
            Camera.main.fov = 35.0f - Mathf.Abs(puck.x) * 0.5f;
        }



        //======================
        // net cameras
        //======================
        if (type == NET_HOME)
        {
            Camera.main.transform.position = new Vector3(arena.net_pos, 20.0f, 0.0f);
            Camera.main.transform.eulerAngles = new Vector3(90.0f, -90.0f, 0.0f);
            Camera.main.fov = 15.0f;
        }
        if (type == NET_GUEST)
        {
            Camera.main.transform.position = new Vector3(-arena.net_pos, 20.0f, 0.0f);
            Camera.main.transform.eulerAngles = new Vector3(90.0f, 90.0f, 0.0f);
            Camera.main.fov = 15.0f;
        }
        if (type == GOAL_HOME)
        {
            float x = net_home.x - 0.6f * Mathf.Sin(Mathf.Deg2Rad * net_home.h);
            float y = net_home.y - 0.6f * Mathf.Cos(Mathf.Deg2Rad * net_home.h);
            Camera.main.transform.position = new Vector3( x, 0.9f, y );
            Camera.main.transform.eulerAngles = new Vector3(20.0f, net_home.h, 0.0f);
            Camera.main.fov = 90.0f;
        }
        if (type == GOAL_HOME)
        {
            // place the camera inside the net
            float x = net_home.x - 0.6f * Mathf.Sin(Mathf.Deg2Rad * net_home.h);
            float y = net_home.y - 0.6f * Mathf.Cos(Mathf.Deg2Rad * net_home.h);
            // find where is the puck and turn our camera in that direction
            if (net_home.object_event == Event.NULL)
            {
                float d = Physics.dd(net_home.h, Physics.Angle(x, y, puck.x, puck.y));
                float tgtd = 0f;
                if (d > 50.0f) tgtd = 50f;
                if (d > 90.0f) tgtd = 80f;
                if (d < -50.0f) tgtd = -50f;
                if (d < -90.0f) tgtd = -80f;
                if (dir < tgtd) dir += 2.0f;
                if (dir > tgtd) dir -= 2.0f;
                Physics.protect(ref dir, -120f, 120f);
            }
            if (net_home.object_event == 0) { dir += 30.0f * Random.value - 60 * Random.value; pitch = 20.0f * Random.value - 40 * Random.value; }
            Physics.protect( ref pitch, -40f, 40f);
            Camera.main.transform.position = new Vector3(x, 0.9f, y);
            Camera.main.transform.eulerAngles = new Vector3(20.0f + pitch, net_home.h + dir, 0.0f);
            Camera.main.fov = 90.0f;
        }
        if (type == GOAL_GUEST)
        {
            // place the camera inside the net
            float x = net_guest.x - 0.6f * Mathf.Sin(Mathf.Deg2Rad * net_guest.h);
            float y = net_guest.y - 0.6f * Mathf.Cos(Mathf.Deg2Rad * net_guest.h);
            // find where is the puck and turn our camera in that direction
            if (net_guest.object_event == 0)
            {
                float d = Physics.dd(net_guest.h, Physics.Angle(x, y, puck.x, puck.y));
                float tgtd = 0f;
                if (d > 50.0f) tgtd = 50f;
                if (d > 90.0f) tgtd = 80f;
                if (d < -50.0f) tgtd = -50f;
                if (d < -90.0f) tgtd = -80f;
                if (dir < tgtd) dir += 2.0f;
                if (dir > tgtd) dir -= 2.0f;
                Physics.protect(ref dir, -120f, 120f);
            }
            if (net_guest.object_event == Event.PLAYER) { dir += 30.0f * Random.value - 60 * Random.value; pitch = 20.0f * Random.value - 40 * Random.value; }
            Physics.protect(ref pitch, -40f, 40f);
            Camera.main.transform.position = new Vector3(x, 0.9f, y);
            Camera.main.transform.eulerAngles = new Vector3(20.0f + pitch, net_guest.h + dir, 0.0f);
            Camera.main.fov = 90.0f;
        }


        //======================
        // user play cameras
        //======================
        if (type == PLAY)
        {
            Camera.main.transform.position = new Vector3(user_head.position.x-5.0f,user_head.position.y+5.0f,user_head.position.z);
            Camera.main.transform.eulerAngles = new Vector3(40.0f, 90.0f, 0.0f);
            Camera.main.fov = 60.0f;
        }

        if (type == PLAY_FOLLOW)
        {
            float d = Physics.Angle(user_head.position.x, user_head.position.z, puck.x, puck.y);
            float cx = user_head.position.x - 5.0f * Mathf.Sin(Mathf.Deg2Rad * (user_head.eulerAngles.y+90.0f));
            float cz = user_head.position.z - 5.0f * Mathf.Cos(Mathf.Deg2Rad * (user_head.eulerAngles.y+90.0f));
            Physics.smooth(ref dir, d, 0.01f*Time.deltaTime);
            Camera.main.transform.position = new Vector3(cx, user_head.position.y + 5.0f, cz);
            Camera.main.transform.eulerAngles = new Vector3(40.0f, dir, 0.0f);
            Camera.main.fov = 60.0f;
        }

        if (type == POV)
        {
            float d = Physics.Angle(user_head.position.x, user_head.position.z, puck.x, puck.y);
            d = Physics.dd(d, user_head.eulerAngles.y + 90.0f);
            Physics.protect(ref d, -100f, 100f);
            Physics.smooth(ref dir, d, 1.0f * Time.deltaTime);
            Camera.main.transform.position = new Vector3(user_head.position.x, user_head.position.y + 0.03f, user_head.position.z);
            Camera.main.transform.eulerAngles = new Vector3(30f, user_head.eulerAngles.y+90.0f+dir, 0f);
            Camera.main.fov = 80.0f;
        }




    }


}
