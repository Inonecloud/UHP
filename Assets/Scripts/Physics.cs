using UnityEngine;
using System.Collections;




public class Physics
{












    public static PARAM Move(PARAM param)
    {
        param.dir = Physics.d2d(param.dir); // normalize dir 
        param.h = Physics.d2d(param.h); // normalize heading

        param.x += param.speed * Mathf.Sin(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        param.y += param.speed * Mathf.Cos(Mathf.Deg2Rad * param.dir) * Time.deltaTime;
        if( param.alt>0.02 ) param.vv -= 9.81f * Time.deltaTime;
        param.alt += param.vv * Time.deltaTime;
        param.p += param.rp * Time.deltaTime;
        param.b += param.rb * Time.deltaTime;

        return param;
    }






    public static float Angle(float x1, float y1, float x2, float y2)
    {
        float az = Mathf.Atan2((x2 - x1), (y1 - y2)) * Mathf.Rad2Deg;
        if (az < 0) az = 360 + az;
        if (az > 360) az = az - 360;
        return 180 - az;
    }

    public static float Distance(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }

    public static float d2d(float dir)
    {
        dir=dir%360f;
        if (dir < 0) dir += 360;
        return dir;
    }


    public static bool In(float src, float tgt, float rad ) 
    {
        if (Mathf.Abs(tgt - src) <= rad) return true;
        return false;
    }

    public static PARAM RandomRotation(PARAM pos, float speed)
    {
        pos.rp = speed * 200.0f * (0.9f + Random.value);
        pos.rb = speed * 200.0f * (0.9f + Random.value);
        pos.rh = speed * 100.0f * (0.9f + Random.value);
        return pos;
    }

    public static float BounceDir( float dir, float sfc_dir )
    {
         return dir + ((sfc_dir - 90) - dir) * 2;
    }
}