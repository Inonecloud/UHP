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










 





}