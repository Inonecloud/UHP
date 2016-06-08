using UnityEngine;
using System.Collections;




public class CNet
{

    // global objects
    public GameObject NetObj;

    // parameters
    public PARAM param;
    public DATA_EQUIPMENT data;





    //=================================================================================================
    // normal loading procedure that loads everything for the gameplay
    //=================================================================================================
    public void Load(string dbkey)
    {
        data = Database.LoadEquipment(dbkey);

        NetObj = Database.LoadGameObject(dbkey, Database.path_equipment + dbkey + "/" + dbkey);
     }






    //=================================================================================================
    // calculate physics for objects
    //=================================================================================================
    public void Process()
    {
        Physics.NetPhysics(ref param);
    }





    //=================================================================================================
    // play the sounds
    //=================================================================================================
    public void Sound()
    {
    }




    //=================================================================================================
    // put player object in the scene with reqwuired coordinates and rotations
    //=================================================================================================
    public void Post()
    {
        //float corr = 0.55f; if (param.x < 0f) corr = -0.55f; 
        // net position corrections for refpoint
        float x = param.x - 0.55f * Mathf.Sin(Mathf.Deg2Rad * param.h);
        float y = param.y - 0.55f * Mathf.Cos(Mathf.Deg2Rad * param.h);
        NetObj.transform.position = new Vector3(x, param.alt, y);
        NetObj.transform.eulerAngles = new Vector3(param.p, param.h, param.b);
    }










 





}