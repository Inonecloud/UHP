using UnityEngine;
using System.Collections;

public class GoalkeeperAI : MonoBehaviour {//, Player {
   
    public Transform target; //puck
    [SerializeField]
    private float speed = 10;
    private int maxangle = 360;
    private int minangle = 180;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0)); //Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0)), speed * Time.deltaTime);
        // Требуется корректировка, либо сделать по-другоу
        /*float angle = Mathf.Asin(rotation.y) * Mathf.Rad2Deg - 45;
        rotation = Quaternion.Euler(0, angle, 0);*/
        //Mathf.Lerp(rotation.x, );
        //Debug.Log(angle);
        transform.rotation = rotation;
    }

   /* public string playerName()
    {
        string player = "Name";
    }

    public bool puckOwner()
    {

    }

    public playerRole(PlayerRole )
    {

    }*/
}
