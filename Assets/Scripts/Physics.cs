using UnityEngine;
using System.Collections;




public class Physics
{





    //============================================
    // player movements 
    //============================================
    public static void PlayerPhysics(ref PARAM param, ref DATA_PLAYER data)
    {

        if (data.legs == Action.RUN)
        {
            param.tgt_speed += 5.0f * Time.deltaTime;
            if (param.tgt_speed > 4.0f) param.tgt_speed = 4.0f;
        }

        if (data.legs == Action.RUN_FAST)
        {
            param.tgt_speed += 5.0f * Time.deltaTime;
            if (param.tgt_speed > 5.0f) param.tgt_speed = 5.0f;
        }

        if (data.legs == Action.STOP)
        {
            param.speed -= 2.0f * Time.deltaTime;
            param.tgt_speed = param.speed;
        }
        if (data.legs == Action.TURN_LEFT)
        {
            param.tgt_dir -= (360.0f - param.speed * 30f) * Time.deltaTime;
        }
        if (data.legs == Action.TURN_RIGHT)
        {
            param.tgt_dir += (360.0f - param.speed * 30f) * Time.deltaTime;
        }
        if (data.legs == Action.TURN_LEFT_FAST)
        {
            param.tgt_dir -= (180.0f - param.speed * 30f) * Time.deltaTime;
        }
        if (data.legs == Action.TURN_RIGHT_FAST)
        {
            param.tgt_dir += (180.0f - param.speed * 30f) * Time.deltaTime;
        }


        if (param.dir - param.tgt_dir > 180f) param.dir -= 360;
        if (param.dir - param.tgt_dir < -180f) param.dir += 360;
        smooth(ref param.dir, param.tgt_dir, 10.0f * Time.deltaTime);
        if (param.h - param.dir > 180f) param.h -= 360;
        if (param.h - param.dir < -180f) param.h += 360;
        smooth(ref param.h, param.dir, 10.0f * Time.deltaTime);
        if (param.tgt_speed > param.speed) smooth(ref param.speed, param.tgt_speed, 2.0f * Time.deltaTime);

        if (param.speed > 0) param.speed *= 0.97f;
        if (param.speed < 0) param.speed = 0;

        Physics.Move(ref param);
    }







    //============================================
    // puck movement procedures 
    //============================================
    public static void PuckPhysics( ref PARAM param )
    {
        param.object_event = 0; // erase the flag
        
        if (param.alt < 0.01)
        {
            param.speed = param.speed * 0.992f;
            param.p *= 0.45f;
            param.b *= 0.45f;
            param.h *= 0.8f;
        }
        Physics.Move( ref param );
    }






    //============================================
    // net movement procedures 
    //============================================
    public static void NetPhysics(ref PARAM param)
    {
        param.object_event = 0; // erase the flag

        param.speed *= 0.98f; // reduce speed
        param.rh *= 0.9f; // reduce rotation
        if( param.p>0 ) param.rp -= 1.0f; // dont jump too high
        if (param.p < 0) { param.p = 0; param.rp = 0f; } // stop jump rotation
        Physics.Move(ref param);
    }








    //============================================
    // general move suitable for all earth objects 
    //============================================
    public static PARAM Move(ref PARAM param)
    {
        param.tgt_dir = Physics.d2d(param.tgt_dir); // normalize dir 
        param.dir = Physics.d2d(param.dir); // normalize dir 
        param.h = Physics.d2d(param.h); // normalize heading

        param.x += param.speed * Mathf.Sin(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        param.y += param.speed * Mathf.Cos(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        if( param.alt>0.02 ) param.vv -= 9.81f * Time.deltaTime;
        param.alt += param.vv * Time.deltaTime;
        param.p += param.rp * Time.deltaTime;
        param.b += param.rb * Time.deltaTime;
        param.h += param.rh * Time.deltaTime;

        return param;
    }










    //============================================
    // general math functions 
    //============================================
    
    // calculate bearing between objects in degrees
    public static float Angle(float x1, float y1, float x2, float y2)
    {
        float az = Mathf.Atan2((x2 - x1), (y1 - y2)) * Mathf.Rad2Deg;
        if (az < 0) az = 360 + az;
        if (az > 360) az = az - 360;
        return 180 - az;
    }

    // calculate distance between objects in meters
    public static float Distance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }

    // normalize heading degrees to read 0-360
    public static float d2d(float dir)
    {
        dir=dir%360f;
        if (dir < 0) dir += 360;
        return dir;
    }

    // find bearing difference -180 to 180
    public static float dd(float d1, float d2)
    {
        float d = d2 - d1;
        if (d > 180.0f) d = d - 360.0f;
        if (d < -180.0f) d = d + 360.0f;
        return d;
    }

    // check if src inside the range centered at tgt 
    public static bool In(float src, float tgt, float rad ) 
    {
        if (Mathf.Abs(tgt - src) <= rad) return true;
        return false;
    }

    // calculate random rotations
    public static void RandomRotation(ref PARAM pos, float speed)
    {
        pos.rp = speed * 200.0f * (0.9f + Random.value);
        pos.rb = speed * 200.0f * (0.9f + Random.value);
        pos.rh = speed * 100.0f * (0.9f + Random.value);
    }

    // calculate bounce direction
    public static float BounceDir( float dir, float sfc_dir )
    {
         return dir + ((sfc_dir - 90) - dir) * 2;
    }

    public static void protect(ref float var, float min, float max)
    {
        if (var > max) var = max;
        if (var < min) var = min;
    }


    public static void smooth(ref float var, float tgt, float spd = 0.5f)
    {
        var += (tgt - var) * spd;
    }
}