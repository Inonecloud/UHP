using UnityEngine;
using System.Collections;



public struct Collision
{
 




    //==================================================================================
    //==================================================================================
    //==================================================================================
    // collision with board and glass
    // data = arena object parameters
    // pos = moving object parameters
    //==================================================================================
    //==================================================================================
    //==================================================================================
    public static void CheckBoardCollision(ref DATA_ARENA data, ref PARAM pos, float radius)
    {
        float board_dir = 0f;

        // rounded collisions
        if (pos.x > data.lenmax && pos.y > data.widmax && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmax)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmax));
            float r = Physics.Angle(data.lenmax, data.widmax, pos.x, pos.y);
            pos.x = data.lenmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.object_event = Event.BOARD;
        }
        if (pos.x < data.lenmin && pos.y > data.widmax && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmax)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmax));
            float r = Physics.Angle(data.lenmin, data.widmax, pos.x, pos.y);
            pos.x = data.lenmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.object_event = Event.BOARD;
        }
        if (pos.x > data.lenmax && pos.y < data.widmin && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmin)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmin));
            float r = Physics.Angle(data.lenmax, data.widmin, pos.x, pos.y);
            pos.x = data.lenmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.object_event = Event.BOARD;
        }
        if (pos.x < data.lenmin && pos.y < data.widmin && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmin)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmin));
            float r = Physics.Angle(data.lenmin, data.widmin, pos.x, pos.y);
            pos.x = data.lenmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.object_event = Event.BOARD;
        }

        // horizontal collisions
        if (pos.x > (data.lenght / 2f) - radius) { pos.x -= (pos.x - ((data.lenght / 2f) - radius)) * 2; board_dir = -90f; pos.object_event = Event.BOARD; }
        if (pos.x < -(data.lenght / 2f - radius)) { pos.x -= (pos.x + ((data.lenght / 2f) - radius)) * 2; board_dir = 90f; pos.object_event = Event.BOARD; }
        if (pos.y > data.width / 2f - radius) { pos.y -= (pos.y - ((data.width / 2f) - radius)) * 2; board_dir = 180f; pos.object_event = Event.BOARD; }
        if (pos.y < -(data.width / 2f - radius)) { pos.y -= (pos.y + ((data.width / 2f) - radius)) * 2; board_dir = 0f; pos.object_event = Event.BOARD; }

        if (pos.object_event != 0)
        {
            pos.speed *= 0.5f;
            float ddir = ((board_dir - 90) - pos.dir) * 2;
            pos.dir = pos.dir + ddir;
            if (pos.speed > 3 && ddir > 30) pos.object_event = Event.BOARD_HIT;
            if (pos.alt > 1.5f)
            {
                pos.speed *= data.glass_firm;
                pos.object_event = Event.GLASS; // glass hit
            }
            else
            {
                pos.speed *= data.board_firm;
            }
            // add rotation
            pos.rp = pos.vv * 150.0f * (0.5f + Random.value);
            pos.rb = pos.vv * 150.0f * (0.5f + Random.value);
            pos.rh = pos.vv * 100.0f * (0.5f + Random.value);
        }


        // vertical ice collision and bouncing
        if (pos.alt < 0)
        {
            if (pos.vv < -1.0f) pos.object_event = Event.PUCK_ICE;
            pos.alt -= pos.alt;
            pos.vv = pos.vv * (-0.5f);
            pos.rp *= 0.8f; if (pos.rp < 10) pos.p = 0;
            pos.rb *= 0.8f; if (pos.rb < 10) pos.b = 0;
        }

    }








    //==================================================================================
    //==================================================================================
    //==================================================================================
    // collision with net poles and net itself
    // better used for puck and maybe a stick
    // param = net object parameters
    // pos = moving object parameters
    //==================================================================================
    //==================================================================================
    //==================================================================================
    public static void CheckNetCollision(ref PARAM param, ref PARAM pos, float radius)
    {
        float pole_width = 0.02f;
        float net_width = 0.20f;

        if (pos.x > 0 && param.x < 0) return;
        if (pos.x < 0 && param.x > 0) return;

        // check left pole
        float d = Physics.Distance(pos.x, pos.y, param.x, param.y - 0.915f) - radius;
        if (pos.alt < 1.22f && d < pole_width)
        {
            float a = Physics.Angle(pos.x, pos.y, param.x, param.y - 0.915f);
            pos.dir = pos.dir + 180 - (a - pos.dir) * 2.0f;
            // apply random rotation for flying puck
            if (Mathf.Abs(a - pos.dir) > 60 && pos.alt > 0.1) Physics.RandomRotation(ref pos, pos.vv);
            pos.speed *= 0.5f;
            pos.object_event = Event.POLE;
        }
        // check right pole
        d = Physics.Distance(pos.x, pos.y, param.x, param.y + 0.915f) - radius;
        if (pos.alt < 1.22f && d < pole_width)
        {
            float a = Physics.Angle(pos.x, pos.y, param.x, param.y + 0.915f);
            pos.dir = pos.dir + 180 - (a - pos.dir) * 2.0f;
            if (Mathf.Abs(a - pos.dir) > 60 && pos.alt > 0.1) Physics.RandomRotation(ref pos, pos.vv);
            pos.speed *= 0.5f;
            pos.object_event = Event.POLE;
        }
        // check upper pole
        if (pos.alt > (1.22f - pole_width) && pos.alt < (1.22f + pole_width) && Physics.In(pos.x, param.x, pole_width) && pos.y > -0.919f && pos.y < 0.919f)
        {
            float da = (pos.alt - 1.22f) / pole_width;
            pos.dir = (360 - pos.dir) * (1.0f - Mathf.Abs(da));
            pos.vv *= 2.0f * da;
            pos.speed *= 0.5f * (1.0f - da);
            pos.object_event = Event.POLE;
            if (Mathf.Abs(da) > 0.7) Physics.RandomRotation(ref pos, pos.vv);
        }


        // if we collided then move object out of the pole
        if (pos.object_event == Event.POLE)
        {
            Physics.Move( ref pos);
        }


        //*****************************************************************************
        // check the inner net
        float b = Mathf.Abs(param.x) + 1.2f;// -pos.alt * 0.5f;
        if (pos.alt < 1.22f && Physics.In(Mathf.Abs(pos.x), b, net_width) && pos.y > -0.920f && pos.y < 0.920f)
        {
            if (pos.dir > 180f) pos.dir = Physics.BounceDir(pos.dir, 90); else pos.dir = Physics.BounceDir(pos.dir, 270);
            Physics.Move(ref pos);
            Physics.Move(ref pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.3f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            Physics.RandomRotation(ref pos, pos.speed);
        }
        // check the left net
        float bl = param.y - 0.920f;// -pos.alt * 0.5f;
        if (pos.alt < 1.22f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= b && pos.y > bl - net_width && pos.y < bl + net_width)
        {
            if (pos.dir > 190f && pos.dir < 270f) pos.dir = Physics.BounceDir(pos.dir, 0); else pos.dir = Physics.BounceDir(pos.dir, 180);
            Physics.Move(ref pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.3f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            Physics.RandomRotation(ref pos, pos.speed);
        }
        // check the right net
        float br = param.y + 0.920f;// -pos.alt * 0.5f;
        if (pos.alt < 1.22f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= b && pos.y > br - net_width && pos.y < br + net_width)
        {
            if (pos.dir > 190f && pos.dir < 270f) pos.dir = Physics.BounceDir(pos.dir, 0); else pos.dir = Physics.BounceDir(pos.dir, 180);
            Physics.Move(ref pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.3f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            Physics.RandomRotation(ref pos, pos.speed);
        }

        if (pos.alt > 1.15f && pos.alt < 1.25f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= Mathf.Abs(param.x) + 0.7f && pos.y > -0.920f && pos.y < 0.920f)
        {
            if (pos.vv < 0)
            {
                pos.vv *= (-0.3f);
                pos.alt = 1.26f;
            }
            else
            {
                pos.vv *= (-0.9f);
                pos.alt = 1.14f;
            }
            pos.speed *= 0.5f;
            Physics.RandomRotation(ref pos, pos.speed);
        }


    }








    //==================================================================================
    //==================================================================================
    //==================================================================================
    // collision with net poles and net itself
    // better used for puck and maybe a stick
    // param = net object parameters
    // pos = moving object parameters
    //==================================================================================
    //==================================================================================
    //==================================================================================
    public static void CheckNetCollisionHeavy(ref PARAM param, ref PARAM pos, float radius)
    {

        float pole_width = 0.02f;

        if (pos.x > 0 && param.x < 0) return;
        if (pos.x < 0 && param.x > 0) return;

        // check left pole
        float d = Physics.Distance(pos.x, pos.y, param.x, param.y - 0.915f) - radius;
        if ( d < pole_width)
        {
            // apply moving to net
            param.speed = pos.speed * 0.9f;
            param.dir = pos.dir;
            param.rh = -param.x;
            param.object_event = Event.PLAYER;
            // affect the player            
            pos.speed *= 0.9f;
            pos.object_event = Event.POLE;

        }
        // check right pole
        d = Physics.Distance(pos.x, pos.y, param.x, param.y + 0.915f) - radius;
        if ( d < pole_width)
        {
            // apply moving to net
            param.speed = pos.speed * 0.9f;
            param.dir = pos.dir;
            param.rh = param.x;
            param.object_event = Event.PLAYER;
            // affect the player            
            pos.speed *= 0.9f;
            pos.object_event = Event.POLE;
        }
        // check upper pole
        if ( Physics.In(pos.x, param.x, pole_width) && pos.y > -0.919f && pos.y < 0.919f)
        {
            // apply moving to net
            param.speed = pos.speed * 0.9f;
            param.dir = pos.dir;
            param.rp = -40.0f;
            param.object_event = Event.PLAYER;
            // affect the player            
            pos.speed *= 0.9f;
            pos.object_event = Event.POLE;
        }
        //*****************************************************************************
        // check the inner net
        if (Physics.In(Mathf.Abs(pos.x), Mathf.Abs(param.x) + 0.8f, 0.4f) && pos.y > param.y-0.920f && pos.y < param.y+0.920f)
        {
            // apply moving to net
            param.speed = pos.speed * 0.9f;
            param.dir = pos.dir;
            param.object_event = Event.PLAYER;
            // affect the player            
            pos.speed *= 0.9f;
            pos.object_event = Event.NET;
        }


    }








    //==================================================================================
    //==================================================================================
    //==================================================================================
    // puck inside the net
    //==================================================================================
    //==================================================================================
    //==================================================================================
    public static bool CheckGoal( PARAM net, PARAM puck)
    {
        if (puck.alt > 1.2f) return false;
        if (puck.x > 0 && net.x < 0) return false;
        if (puck.x < 0 && net.x > 0) return false;

        if (puck.x > Mathf.Abs(net.x) + 0.05f && puck.x < Mathf.Abs(net.x) + 1.0f && puck.y > net.y - 0.920f && puck.y < net.y + 0.920f)
        {
            return true;
        }
        return false;
    }









};