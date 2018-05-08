using UnityEngine;
using System.Collections;



public class AIPlayer
{




    // ===================================================
    // AI player thinking procedures
    // ===================================================
    public static void ProcessAI(ref PARAM param, ref AI_PLAYER player)
    {
        // just go
        float r = Physics.Angle(param.x, param.y, player.puck.x, player.puck.y);
        float d = Physics.Distance(param.x, param.y, player.puck.x, player.puck.y);
        param.dir = r;
        param.h = param.dir;

        param.speed += (0.3f * Random.value) - (0.1f * Random.value);
        if (param.speed > 4.0f) param.speed = 4.0f;
        //if (player.side == -1 && param.x < -5 || player.side == 1 && param.x > 5) param.speed *= 0.96f;
        if (param.speed < 0.1f) param.speed = 0.1f;

    }







    // ===================================================
    // AI puck control
    // ===================================================
    public static void PuckControl( ref PARAM param, ref DATA_PLAYER data, ref AI_PLAYER player, ref PARAM puck)
    {

        float r = Physics.dd( Physics.Angle(param.x, param.y, puck.x, puck.y), param.h );
        float d = Physics.Distance(param.x, param.y, puck.x, puck.y);


        //if ((d > 2.0f || puck.alt > 2.0f) && data.hands != 0) data.hands = 0;
 
 
        // accumulate energy
        if (data.hands == Action.SHOT_ACC || data.hands == Action.SLAP_ACC || data.hands == Action.PASS_ACC) data.action_energy += 0.05f;
        Physics.protect(ref data.action_energy, 0.0f, 1.0f);
        
        // shot
        if (data.hands == Action.SHOT_ACC && data.action_count>-10.0f ) data.action_count -= 1.0f;
        if (data.hands == Action.SHOT) data.action_count += 1.0f;
        if (data.hands == Action.SHOT && data.action_count > 50.0f) { data.action_count = 0; data.hands = 0; }
        if (d < 2.0f && puck.alt < 0.1f && data.hands == Action.SHOT && data.action_count==1.0f )
        {
            puck.speed = 8f + 8f * data.action_energy + 2f * Random.value;
            puck.vv = 3f * Random.value;
            puck.dir = (1.5f-data.action_energy) * 10f * Random.value - 1 + Physics.Angle(puck.x, puck.y, player.net_ot.x, player.net_ot.y);
            param.object_event = Event.SNAPSHOT;
            data.action_energy = 0;
        }
        // shot fast upper
        if (data.hands == Action.SHOT_FAST) data.action_count += 1.0f;
        if (data.hands == Action.SHOT_FAST && data.action_count > 50.0f) { data.action_count = 0; data.hands = 0; }
        if (d < 2.0f && puck.alt < 0.1f && data.hands == Action.SHOT_FAST && data.action_count == 1.0f)
        {
            puck.speed = 8f + 8f * data.action_energy + 2f * Random.value;
            puck.vv = 2f + 4f*Random.value;
            puck.dir = (1.5f - data.action_energy) * 10f * Random.value + Physics.Angle(puck.x, puck.y, player.net_ot.x, player.net_ot.y);
            param.object_event = Event.SNAPSHOT;
            data.action_energy = 0;
        }


        
        // onetimer
        if (data.hands == Action.SLAP_ACC && data.action_count > -10.0f) data.action_count -= 1.0f;
        if (data.hands == Action.SLAP) data.action_count += 1.0f;
        if (data.hands == Action.SLAP && data.action_count > 50.0f) { data.action_count = 0; data.hands = 0; }
        if (d < 2.0f && puck.alt < 0.1f && data.hands == Action.SLAP && data.action_count == 1.0f)
        {
            Debug.Log("SLAP");
            puck.speed = 10f + 15f * data.action_energy + 5f * Random.value;
            puck.vv = 3f * Random.value;
            puck.dir = (data.action_energy) * 20f * Random.value - 1 + Physics.Angle(puck.x, puck.y, player.net_ot.x, player.net_ot.y);
            param.object_event = Event.SLAPSHOT;
            data.action_energy = 0;
        }
        // sonetimer fast upper
        if (data.hands == Action.SLAP_FAST) data.action_count += 1.0f;
        if (data.hands == Action.SLAP_FAST && data.action_count > 50.0f) { data.action_count = 0; data.hands = 0; }
        if (d < 2.0f && puck.alt < 0.1f && data.hands == Action.SLAP_FAST && data.action_count == 1.0f)
        {
            puck.speed = 10f + 15f * data.action_energy + 5f * Random.value;
            puck.vv = 1f + 5f * Random.value;
            puck.dir = (data.action_energy) * 20f * Random.value + Physics.Angle(puck.x, puck.y, player.net_ot.x, player.net_ot.y);
            param.object_event = Event.SLAPSHOT;
            data.action_energy = 0;
        }





        //============================================================================================
        // keep the puck playing around
        if (d < 2.0f && puck.alt < 0.1f)
        {
            // where the puck should go )))
            float dr = (20.0f - param.speed); // set side margin
            if (data.hands == 0) data.hands = Action.PLAY_PUCK_LEFT;
            if (data.hands == Action.PLAY_PUCK_LEFT && r >= dr) { data.hands = Action.PLAY_PUCK_RIGHT; param.object_event = Event.STICK_ICE; }
            if (data.hands == Action.PLAY_PUCK_RIGHT && r <= -dr) { data.hands = Action.PLAY_PUCK_LEFT; param.object_event = Event.STICK_ICE; }
            if (data.hands == Action.PLAY_PUCK_RIGHT) dr = -dr;
            if (data.hands == Action.PLAY_PUCK_RIGHT || data.hands == Action.PLAY_PUCK_LEFT)
            {
                float tx = param.x + (1.2f + param.speed * 0.3f) * Mathf.Sin(Mathf.Deg2Rad * (param.h - dr));
                float ty = param.y + (1.2f + param.speed * 0.3f) * Mathf.Cos(Mathf.Deg2Rad * (param.h - dr));
                float tr = Physics.Angle(puck.x, puck.y, tx, ty);
                float td = Physics.Distance(puck.x, puck.y, tx, ty);
                puck.speed = td + param.speed * 0.2f + 2.0f + 1.0f * Random.value;
                puck.vv = 0.5f * Random.value;
                puck.dir = tr + 3f * Random.value;
            }
            if (data.hands == Action.SHOT_ACC && data.action_count<-5 )
            {
                dr = 90f - param.speed * 20.0f;
                float tx = param.x + (0.9f + param.speed * 0.3f) * Mathf.Sin(Mathf.Deg2Rad * (param.h - dr));
                float ty = param.y + (0.9f + param.speed * 0.3f) * Mathf.Cos(Mathf.Deg2Rad * (param.h - dr));
                float tr = Physics.Angle(puck.x, puck.y, tx, ty);
                float td = Physics.Distance(puck.x, puck.y, tx, ty);
                puck.speed = td * (param.speed * 0.2f + 0.2f + 1.5f + 1.0f * Random.value);
                puck.vv = 0.5f * Random.value;
                puck.dir = tr + 3f * Random.value;
                if (td < 0.05) { puck.speed = param.speed; puck.dir = param.dir; }
            }

            if (data.hands == Action.SLAP_ACC && data.action_count ==-1 )
            {
                dr = 70f - param.speed * 20.0f;
                Debug.Log(dr);
                float tx = param.x + (0.9f + param.speed * 0.3f) * Mathf.Sin(Mathf.Deg2Rad * (param.h - dr));
                float ty = param.y + (0.9f + param.speed * 0.3f) * Mathf.Cos(Mathf.Deg2Rad * (param.h - dr));
                float tr = Physics.Angle(puck.x, puck.y, tx, ty);
                float td = Physics.Distance(puck.x, puck.y, tx, ty);
                puck.speed = ( param.speed * 1.0f + 1.0f + 1.0f * Random.value);
                puck.vv = 0.5f * Random.value;
                puck.dir = tr + 3f * Random.value;
                //if (td < 0.05) { puck.speed = param.speed; puck.dir = param.dir; }
            }
        }
   
    
    }






}