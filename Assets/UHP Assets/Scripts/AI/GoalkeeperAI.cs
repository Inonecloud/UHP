using UnityEngine;
using System.Collections;

public class GoalkeeperAI : MonoBehaviour {//, Player {
   
    public Transform target; //puck
    [SerializeField]
    private float speed = 1;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        //Mathf.Lerp(rotation.x, );
        Debug.Log(rotation.x);
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
