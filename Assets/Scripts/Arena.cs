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







    






 

}