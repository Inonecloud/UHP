using UnityEngine;
using System.Collections;

public class hpcontol : MonoBehaviour {
    CharacterController controller;
    Animator animator;
    public bool isEnable;

    public float speed = 5;
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isEnable)
        {
            // movement controll
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (x != 0)
                //animator.SetBool(); // Rotation animation
                transform.Rotate(0f, x * speed * 3, 0f);
            if (z != 0)
            {
                //animator.SetBool();  // Skating animation
                Vector3 dir = transform.TransformDirection(new Vector3(0f, 0f, z * speed * Time.deltaTime));
                controller.Move(dir);
            }
            /*
            else
            {
                animator.SetBool(); // Standing animation
            }
            */
        }
            
	}

    //Puck collision
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.gameObject.GetComponent<Rigidbody>(); 
        if (body != null)
        {
            body.AddForce(hit.moveDirection * 20f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                body.AddForce(hit.moveDirection * 100f); //Shoot
            }
        }
    }

    void Activate(){
        isEnable = true;
    }

    void Deactivate(){
        isEnable = false;
    }
}
