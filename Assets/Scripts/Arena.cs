using UnityEngine;
using System.Collections;




public class CArena  
{

    // objects
    public GameObject ArenaObj;
    public GameObject IceObj;


    // parameters
    public DATA_ARENA data;



    public void Init(string dbkey)
    {
        // load params from db first
        data = Database.LoadArena(dbkey);
    }


    public void Load( string dbkey )
    {
        // load params from db first
        data = Database.LoadArena(dbkey);

        // load arena object
        ArenaObj = Database.LoadGameObject( dbkey, Database.path_arena+dbkey+"/arena");
 
        //IceObj = Object.Instantiate(Resources.Load("meshes/ice", typeof(GameObject))) as GameObject;

    }





    public void Process()
    {
    }


    public void Post()
    {
    }







    public PARAM CheckBoardCollision( PARAM pos, float radius )
    {
        float board_dir=0f;

        // rounded collisions
        if (pos.x > data.lenmax && pos.y > data.widmax && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmax)) > data.round-radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmax));
            float r = Physics.Angle(data.lenmax, data.widmax, pos.x, pos.y);
            pos.x = data.lenmax + ((data.round-radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.collision = 1;
        }
        if (pos.x < data.lenmin && pos.y > data.widmax && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmax)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmax));
            float r = Physics.Angle(data.lenmin, data.widmax, pos.x, pos.y);
            pos.x = data.lenmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.collision = 1;
        }
        if (pos.x > data.lenmax && pos.y < data.widmin && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmin)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmax, data.widmin));
            float r = Physics.Angle(data.lenmax, data.widmin, pos.x, pos.y);
            pos.x = data.lenmax + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.collision = 1;
        }
        if (pos.x < data.lenmin && pos.y < data.widmin && Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmin)) > data.round - radius)
        {
            float d = Vector2.Distance(new Vector2(pos.x, pos.y), new Vector2(data.lenmin, data.widmin));
            float r = Physics.Angle(data.lenmin, data.widmin, pos.x, pos.y);
            pos.x = data.lenmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Sin(Mathf.Deg2Rad * r);
            pos.y = data.widmin + ((data.round - radius) - (d - (data.round - radius))) * Mathf.Cos(Mathf.Deg2Rad * r);
            board_dir = r - 180f; pos.collision = 1;
        }

        // horizontal collisions
        if (pos.x > (data.lenght / 2f)-radius) { pos.x -= (pos.x - ((data.lenght / 2f)-radius)) * 2; board_dir = -90f; pos.collision = 1; }
        if (pos.x < -(data.lenght / 2f-radius)) { pos.x -= (pos.x + ((data.lenght / 2f)-radius)) * 2; board_dir = 90f; pos.collision = 1; }
        if (pos.y > data.width / 2f-radius) { pos.y -= (pos.y - ((data.width / 2f)-radius)) * 2; board_dir = 180f; pos.collision = 1; }
        if (pos.y < -(data.width / 2f-radius)) { pos.y -= (pos.y + ((data.width / 2f)-radius)) * 2; board_dir = 0f; pos.collision = 1; }

        if (pos.collision != 0)
        {
            pos.speed *= data.board_firm;
            float ddir = ((board_dir - 90) - pos.dir) * 2;
            pos.dir = pos.dir + ddir;
            if ( pos.speed>3 && ddir>30 ) pos.collision=2;
            if (pos.alt > 1.5f)
            {
                pos.speed *= data.glass_firm;
                pos.collision = 10; // glass hit
            }
            // add rotation
            pos.rp = pos.vv * 150.0f * (0.5f+Random.value);
            pos.rb = pos.vv * 150.0f * (0.5f + Random.value);
            pos.rh = pos.vv * 100.0f * (0.5f + Random.value);
        }


        // vertical ice collision and bouncing
        if (pos.alt < 0) {
            if( pos.vv<-1.0f ) pos.collision = 99;
            pos.alt -= pos.alt; 
            pos.vv = pos.vv * (-0.5f);
            pos.rp *= 0.8f; if (pos.rp < 10) pos.p = 0;
            pos.rb *= 0.8f; if (pos.rb < 10) pos.b = 0;
        }
 

        return pos;
    }







 

}