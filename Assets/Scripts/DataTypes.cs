using UnityEngine;
using System.Collections;



// object general parameters
public struct PARAM
{
    public float x, y, alt;
    public float dir; // movement direction
    public float speed; // movement speed
    public float vv; // vertical velocity
    // rotation
    public float p, b, h; // pitch bank heading
    public float rp, rb, rh; // rotation speeds
    // events
    public int collision; // collision type
};






// arena parameters
public struct DATA_ARENA
{
    public string key; // database key

    public string name;
    public string name_rus;

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



 
};





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
