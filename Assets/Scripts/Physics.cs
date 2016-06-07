using UnityEngine;
using System.Collections;




public class Physics
{





    //============================================
    // player movements 
    //============================================
    public static void PlayerPhysics(ref PARAM param)
    {
        Physics.Move(ref param);
    }







    //============================================
    // puck movement procedures 
    //============================================
    public static void PuckPhysics( ref PARAM param )
    {
       if (param.alt < 0.01)
        {
            param.speed = param.speed * 0.995f;
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
        param.speed *= 0.98f; // reduce speed
        param.rh *= 0.9f; // reduce rotation
        if( param.p>0 ) param.rp -= 0.5f; // dont jump too high
        if (param.p < 0) { param.p = 0; param.rp = 0f; } // stop jump rotation
        Physics.Move(ref param);
    }








    //============================================
    // general move suitable for all earth objects 
    //============================================
    public static PARAM Move(ref PARAM param)
    {
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
    public static PARAM RandomRotation(PARAM pos, float speed)
    {
        pos.rp = speed * 200.0f * (0.9f + Random.value);
        pos.rb = speed * 200.0f * (0.9f + Random.value);
        pos.rh = speed * 100.0f * (0.9f + Random.value);
        return pos;
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

}