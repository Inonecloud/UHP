using UnityEngine;
using System.Collections;



public class CCamera
{
    // camera types
    public static int TV = 0;
    public static int NET_HOME = 10;
    public static int NET_GUEST = 11;
    public static int GOAL_HOME = 12;
    public static int GOAL_GUEST = 13;
    public static int PLAY = 20;
    public static int POV = 30;

    // current active camera
    public int type;


    // camera parameters
    public float dir; // direction change
    public float pitch; // pitch change



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
        if (type == GOAL_GUEST)
        {
            // place the camera inside the net
            float x = net_guest.x - 0.6f * Mathf.Sin(Mathf.Deg2Rad * net_guest.h);
            float y = net_guest.y - 0.6f * Mathf.Cos(Mathf.Deg2Rad * net_guest.h);
            // find where is the puck and turn our camera in that direction
            if (net_guest.collision == 0)
            {
                float d = Physics.dd(net_guest.h, Physics.Angle(x, y, puck.x, puck.y));
                float tgtd = 0f;
                if (d > 50.0f) tgtd = 50f;
                if (d > 90.0f) tgtd = 80f;
                if (d < -50.0f) tgtd = -50f;
                if (d < -90.0f) tgtd = -80f;
                if (dir < tgtd) dir += 2.0f;
                if (dir > tgtd) dir -= 2.0f;
            }
            if (net_guest.collision == Collision.PLAYER) { dir += 40.0f * Random.value - 80 * Random.value; pitch = 20.0f * Random.value - 40 * Random.value; }
            Physics.protect( ref dir,-120f,120f );
            Physics.protect( ref pitch, -40f, 40f);
            Camera.main.transform.position = new Vector3(x, 0.9f, y);
            Camera.main.transform.eulerAngles = new Vector3(20.0f+pitch, net_guest.h+dir, 0.0f);
            Camera.main.fov = 90.0f;
        }


    }


}
