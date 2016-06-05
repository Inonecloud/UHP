using UnityEngine;
using System.Collections;




public class CNet
{

    // global objects
    public GameObject NetObj;

    // parameters
    public PARAM param;
    public DATA_EQUIPMENT data;





    public void Load( string dbkey )
    {
        data = Database.LoadEquipment(dbkey);

        NetObj = Database.LoadGameObject(dbkey, Database.path_equipment + dbkey + "/" + dbkey);
     }






    public void Process()
    {


    }






    public void Post()
    {
        float b = 1.0f; if (param.x < 0f) b = -1.0f;
        NetObj.transform.position = new Vector3(param.x+0.55f*b, param.alt + 0.01f, param.y);
        NetObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }










    public PARAM CheckNetCollision(PARAM pos, float radius)
    {
        float pole_width = 0.02f;
 
        if (pos.x > 0 && param.x < 0) return pos;
        if (pos.x < 0 && param.x > 0) return pos;

        // check left pole
        float d = Physics.Distance(pos.x, pos.y, param.x, param.y - 0.915f) - radius;
        if (pos.alt < 1.22f && d < pole_width)
        {
            float a = Physics.Angle(pos.x, pos.y, param.x, param.y - 0.915f);
            pos.dir = pos.dir + 180 - (a - pos.dir) * 2.0f;
            // apply random rotation for flying puck
            if (Mathf.Abs(a - pos.dir) > 60 && pos.alt > 0.1) pos = Physics.RandomRotation(pos, pos.vv);
            pos.speed *= 0.5f;
            pos.collision = 98;
        }
        // check right pole
        d = Physics.Distance(pos.x, pos.y, param.x, param.y + 0.915f) - radius;
        if (pos.alt < 1.22f && d < pole_width)
        {
            float a = Physics.Angle(pos.x, pos.y, param.x, param.y + 0.915f);
            pos.dir = pos.dir + 180 - (a-pos.dir) * 2.0f;
            if (Mathf.Abs(a - pos.dir) > 60 && pos.alt > 0.1) pos = Physics.RandomRotation(pos, pos.vv);
            pos.speed *= 0.5f;
            pos.collision = 98;
        }
        // check upper pole
        if (pos.alt > (1.22f - pole_width) && pos.alt < (1.22f + pole_width) && Physics.In(pos.x, param.x, pole_width) && pos.y > -0.919f && pos.y < 0.919f)
        {
            float da = (pos.alt - 1.22f) / pole_width;
            pos.dir = (360 - pos.dir) * (1.0f-Mathf.Abs(da));
            pos.vv *= 2.0f * da;
            pos.speed *= 1.0f * (1.0f-da);
            pos.collision = 98;
            if (Mathf.Abs(da) > 0.7) pos = Physics.RandomRotation(pos, pos.vv);
        }


        // if we collided then move object out of the pole
        if (pos.collision == 98)
        {
            pos = Physics.Move(pos);
        }


        //*****************************************************************************
        // check the inner net
        float b = Mathf.Abs(param.x) + 1.2f;// -pos.alt * 0.5f;
        if( pos.alt<1.22f && Physics.In(Mathf.Abs(pos.x), b, 0.1f) && pos.y > -0.920f && pos.y < 0.920f)
        {
            if (pos.dir > 180f) pos.dir = Physics.BounceDir(pos.dir, 90); else pos.dir = Physics.BounceDir(pos.dir, 270);
            pos = Physics.Move(pos);
            pos = Physics.Move(pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.5f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            pos = Physics.RandomRotation(pos, pos.speed);
        }
        // check the left net
        float bl = param.y - 0.920f;// -pos.alt * 0.5f;
        if (pos.alt < 1.22f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= b && pos.y > bl - 0.2f && pos.y < bl + 0.2f)
        {
            if (pos.dir > 190f && pos.dir<270f ) pos.dir = Physics.BounceDir(pos.dir, 0); else pos.dir = Physics.BounceDir(pos.dir, 180);
            pos = Physics.Move(pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.5f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            pos = Physics.RandomRotation(pos, pos.speed);
        }
        // check the right net
        float br = param.y + 0.920f;// -pos.alt * 0.5f;
        if (pos.alt < 1.22f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= b && pos.y > br - 0.2f && pos.y < br + 0.2f)
        {
            if (pos.dir > 190f && pos.dir < 270f) pos.dir = Physics.BounceDir(pos.dir, 0); else pos.dir = Physics.BounceDir(pos.dir, 180);
            pos = Physics.Move(pos);
            if (pos.alt <= 0.1f) pos.speed *= 0.5f;
            if (pos.alt > 0.1f && pos.alt < 1.0f) pos.speed *= 0.1f;
            pos = Physics.RandomRotation(pos, pos.speed);
        }

        if (pos.alt > 1.15f && pos.alt < 1.25f && Mathf.Abs(pos.x) >= Mathf.Abs(param.x) && Mathf.Abs(pos.x) <= Mathf.Abs(param.x) + 0.7f && pos.y > -0.920f && pos.y < 0.920f)
        {
            if (pos.vv < 0) { 
                pos.vv *= (-0.3f);
                pos.alt = 1.26f;
            } else { 
                pos.vv *= (-0.9f);
                pos.alt = 1.14f;
            }
            pos.speed *= 0.5f;
            pos = Physics.RandomRotation(pos, pos.speed);
        }



        return pos;
    }





}