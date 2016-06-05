using UnityEngine;
using System.Collections;



public class AIPlayer
{




    // ===================================================
    // AI player thinking procedures
    // ===================================================
    public static PARAM ProcessAI(PARAM param, AI_PLAYER player)
    {
        // just go
        float r = Physics.Angle(param.x, param.y, player.puck.x, player.puck.y);
        float d = Physics.Distance(param.x, param.y, player.puck.x, player.puck.y);
        //if (pos.dir != r) pos.dir += (r-pos.dir)*0.4f ;
        param.dir = r;
        param.h = param.dir;

        param.speed += (0.5f * Random.value) - (0.2f * Random.value);
        if (param.speed > 5.0f) param.speed = 5.0f;
        if (player.side == -1 && param.x < -5 || player.side == 1 && param.x > 5) param.speed *= 0.96f;
        if (param.speed < 0.1f) param.speed = -0.1f;




        return param;
    }







    // ===================================================
    // AI puck control
    // ===================================================
    public static PARAM PuckControl(PARAM param, AI_PLAYER player, PARAM puck)
    {

        float r = Physics.Angle(param.x, param.y, puck.x, puck.y);
        float d = Physics.Distance(param.x, param.y, puck.x, puck.y);
        if (d < 0.9f && puck.alt<2F )
        {
            puck.speed = 5f + 1f * Random.value;
            puck.vv = 2f * Random.value;
            puck.dir = param.h + 3f * Random.value;
            // hit
            if (player.side == -1 && param.x > 12 || player.side == 1 && param.x < -12)
            {
                puck.speed = 1f + 20f * Random.value;
                puck.vv = 10f * Random.value;
                puck.dir = 10f * Random.value - 5 + Physics.Angle(puck.x, puck.y, player.net_ot.x, player.net_ot.y);
            }
        }


        return puck;
    }






}