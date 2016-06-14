using UnityEngine;
using System.Collections;



// object general parameters
public struct PARAM
{
    public float x, y, alt;
    public float dir; // movement direction
    public float speed; // movement speed
    public float vv; // vertical velocity
    public float tgt_speed, tgt_dir; // target velocities for accelerations
    // rotation
    public float p, b, h; // pitch bank heading
    public float rp, rb, rh; // rotation speeds
    // events
    public Event object_event; // collision type or sound trigger
};



// player actions for legs and hands
public enum Event
{
    NULL,
    BOARD,
    BOARD_HIT,
    GLASS,
    PUCK_ICE,
    POLE,
    NET,
    PLAYER,
    STICK_ICE,
    SNAPSHOT,
    SLAPSHOT,
    SKATE_START
}







// arena parameters
public struct DATA_ARENA
{
    public string key; // database key

    public string name;
    public string name_rus;

    public string type; // standard = int or nhl
    public float width; 
    public float lenght; 
    public float round;
    public float board_firm; // how board collision affects puck speed
    public float glass_firm; // how glass collision affects puck speed
    public float net_pos; // net position from center

    // dynamic members
    public float lenmin, lenmax, widmin, widmax; // dynamic members for boards

};





// equipment parameters
public struct DATA_EQUIPMENT
{
    public string key; // database key
};













// player parameters
public struct DATA_PLAYER
{
    public string key; // database key

    public Role role; //player role in team 

    // player actions
    public Action legs, hands; // actions for legs and hands
    public float action_energy; // accumulated energy coefficient for hands action 0.0-1.0
    public float action_count; // internal counter of action; 
 
};


// player actions for legs and hands
public enum Action
{
    NULL,
    RUN,
    RUN_FAST,
    STOP,
    STOP_FAST,
    TURN_LEFT,
    TURN_LEFT_FAST,
    TURN_RIGHT,
    TURN_RIGHT_FAST,
    PLAY_PUCK,
    PLAY_PUCK_LEFT,
    PLAY_PUCK_RIGHT,
    PLAY_PUCK_FAST,
    PLAY_PUCK_TRICK,
    PASS,
    PASS_ACC,
    PASS_FAST,
    PASS_FAST_ACC,
    SHOT,
    SHOT_ACC,
    SHOT_FAST,
    SHOT_FAST_ACC,
    SLAP,
    SLAP_ACC,
    SLAP_FAST,
    SLAP_FAST_ACC
}

//players role
public enum Role {
    GOALKEEPER,
    LEFT_DEFENDER,
    RIGHT_DEFENDER,
    LEFT_FOWARD,
    CNTR_FORWARD,
    RIGHT_FORWARD,
    COACH,
    REFEREE
}






// AI parameters
public struct AI_PLAYER
{
    public int side; // my team side 1 / -1

    public PARAM puck;
    public PARAM net_my; // my net
    public PARAM net_ot; // opposing team net

    public PARAM my_c_fwd; // center forward
    public PARAM my_l_fwd; // left forward
    public PARAM my_r_fwd; // right forward
    public PARAM my_l_def; // center forward
    public PARAM my_r_def; // center forward
    public PARAM my_keeper; // goal keeper

    public PARAM ot_c_fwd; // center forward
    public PARAM ot_l_fwd; // left forward
    public PARAM ot_r_fwd; // right forward
    public PARAM ot_l_def; // center forward
    public PARAM ot_r_def; // center forward
    public PARAM ot_keeper; // goal keeper

};



 


