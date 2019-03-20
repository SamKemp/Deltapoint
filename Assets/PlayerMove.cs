using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMove : MonoBehaviour
{
    public int player;
    
    private Vector3 movementVector;
    
    private CharacterController characterController;
    
    private float movementSpeed = 100;
    
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxis("LeftJoystickX_P" + player) * movementSpeed;
        movementVector.z = Input.GetAxis("LeftJoystickY_P" + player) * movementSpeed;
        movementVector.y = 0;
        
        characterController.Move(movementVector * Time.deltaTime);

        if (Input.GetButtonDown("A_P" + player))
        {
            FireBullet();
        }
    }
    
    void FireBullet()
    	{
    		GameObject bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
    		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
    	}
}
