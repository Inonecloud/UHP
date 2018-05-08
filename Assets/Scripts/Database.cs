using UnityEngine;
using System.Collections;




public class Database
{
    public static string path_arena = "arena/";
    public static string path_player = "players/";
    public static string path_equipment = "equipment/";




    //=============================================
    // ARENA 
    //=============================================
    public static DATA_ARENA LoadArena(string key)
    {
        DATA_ARENA D=new DATA_ARENA();
        string text=Database.LoadTextAsset( key, path_arena + key + "/data" );
 
        string[] lines = text.Split('\n');

        D.key=key;
        D.name = lines[0].Trim();
        D.name_rus = lines[1].Trim();
        D.type = lines[2].Trim();

        D.board_firm = float.Parse(lines[3]);
        D.glass_firm = float.Parse(lines[4]);
        D.net_pos = float.Parse(lines[5]);


        // fill dynamic members
        if( D.type=="int" ){
            D.width = 29.6f;
            D.lenght = 59.6f;
            D.round = 8.7f;
        }
        D.lenmax = (D.lenght / 2f) - D.round;
        D.lenmin = D.lenmax * (-1);
        D.widmax = (D.width / 2f) - D.round;
        D.widmin = D.widmax * (-1);

        return D;
    }


    





    //=============================================
    // EQUIOMENT
    //=============================================
    public static DATA_EQUIPMENT LoadEquipment(string key)
    {
        DATA_EQUIPMENT D = new DATA_EQUIPMENT();
        string text = Database.LoadTextAsset(key, path_equipment + key + "/data");

        string[] lines = text.Split('\n');

        D.key = key;



        return D;
    }






    //=============================================
    // PLAYER 
    //=============================================
    public static DATA_PLAYER LoadPlayer(string key)
    {
        DATA_PLAYER D = new DATA_PLAYER();
        string text = Database.LoadTextAsset(key, path_player + key + "/data");

        string[] lines = text.Split('\n');

        D.key = key;



        return D;
    }










    //=============================================
    // OTHER PROCEDURES
    //=============================================
    public static string LoadTextAsset(string key, string path)
    {
        TextAsset text = Resources.Load( path ) as TextAsset;
        if (!text) CoreError.Add( key+" datafile not found");
        return text.text;
    }

    public static GameObject LoadGameObject( string key, string path )
    {
        Object Res=Resources.Load(path, typeof(GameObject));
        if (!Res) CoreError.Add( key + " object not found" );
        GameObject Obj = Object.Instantiate( Res ) as GameObject;
        if (!Obj) CoreError.Add( key + " object not found" );
        return Obj;
    }



}