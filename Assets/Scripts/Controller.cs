using UnityEngine;
using System.Collections;


public class CController
{
    // modifiers
    bool shift; // acceleration and shot modifier





    //=============================================
    // load controller setup
    //=============================================
    public void Load()
    {

    }

    



    //=============================================
    // handle player controls
    //=============================================
    public void PlayerControl(ref DATA_PLAYER data)
    {
        // modifiers
        if (Input.GetKey(KeyCode.LeftShift)) shift = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) shift = false;

        // main keys
        if (Input.GetKey(KeyCode.UpArrow) && shift) data.legs = Action.RUN_FAST;
        if (Input.GetKey(KeyCode.UpArrow) && !shift) data.legs = Action.RUN;
        if (Input.GetKeyUp(KeyCode.UpArrow)) data.legs = 0;

        if (Input.GetKey(KeyCode.DownArrow) && shift) data.legs = Action.STOP_FAST;
        if (Input.GetKey(KeyCode.DownArrow) && !shift) data.legs = Action.STOP;
        if (Input.GetKeyUp(KeyCode.DownArrow)) data.legs = 0;

        if (Input.GetKey(KeyCode.LeftArrow) && shift) data.legs = Action.TURN_LEFT_FAST;
        if (Input.GetKey(KeyCode.LeftArrow) && !shift) data.legs = Action.TURN_LEFT;
        if (Input.GetKeyUp(KeyCode.LeftArrow)) data.legs = 0;

        if (Input.GetKey(KeyCode.RightArrow) && shift) data.legs = Action.TURN_RIGHT_FAST;
        if (Input.GetKey(KeyCode.RightArrow) && !shift) data.legs = Action.TURN_RIGHT;
        if (Input.GetKeyUp(KeyCode.RightArrow)) data.legs = 0;

        // play with puck
        //if (Input.GetKey(KeyCode.C) && shift) player.hands = Action.PLAY_PUCK_FAST;
        //if (Input.GetKey(KeyCode.C) && !shift ) player.hands = Action.PLAY_PUCK; 
        //if (Input.GetKeyUp(KeyCode.C)) player.legs = 0;

        // normal pass 
        if (Input.GetKey(KeyCode.X) && shift) data.hands = Action.PASS_ACC;
        if (Input.GetKeyUp(KeyCode.X) && shift) data.hands = Action.PASS_FAST;
        if (Input.GetKey(KeyCode.X) && !shift) data.hands = Action.PASS_ACC;
        if (Input.GetKeyUp(KeyCode.X) && !shift) data.hands = Action.PASS;

        // normal shot 
        if (Input.GetKey(KeyCode.C) && shift) data.hands = Action.SHOT_ACC;
        if (Input.GetKeyUp(KeyCode.C) && shift) data.hands = Action.SHOT_FAST;
        if (Input.GetKey(KeyCode.C) && !shift) data.hands = Action.SHOT_ACC;
        if (Input.GetKeyUp(KeyCode.C) && !shift) data.hands = Action.SHOT; 

        // slapshot or onetimer 
        if (Input.GetKey(KeyCode.Space) && shift) data.hands = Action.SLAP_ACC;
        if (Input.GetKeyUp(KeyCode.Space) && shift) data.hands = Action.SLAP_FAST;
        if (Input.GetKey(KeyCode.Space) && !shift) data.hands = Action.SLAP_ACC;
        if (Input.GetKeyUp(KeyCode.Space) && !shift) data.hands = Action.SLAP;
    }

















    //=============================================
    // handle camera controls
    //=============================================
    public void CameraControl(ref CCamera m_camera)
    {
        //Switch cam;
        // T - TV
        // N - NET_HOME
        // M - NET_GUEST
        // H - GOAL_HOME
        // G - GOAL_GUEST
        // L - PLAY
        // P - POV
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_camera.Select(CCamera.TV);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            m_camera.Select(CCamera.GOAL_GUEST);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_camera.Select(CCamera.GOAL_HOME);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            m_camera.Select(CCamera.POV);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            m_camera.Select(CCamera.NET_HOME);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_camera.Select(CCamera.NET_GUEST);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_camera.Select(CCamera.PLAY);
        }

    }


}