using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public int player;
    
    private Vector3 movementVector;
    private Vector3 rotationVector;
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationVector2;
    
    private CharacterController characterController;
    
    private float movementSpeed = 8;
    private float rotateSpeed = 40.0f;
    
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    
    private float Health = 100;
    private Image HealthBar;

    private int Deaths = 0;
    private Text DeathCount;

    private int cooldown = 0;
    
    Rigidbody m_Rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        HealthBar = GameObject.Find("P" + player + "-Health").GetComponent<Image>();
        DeathCount = GameObject.Find("P" + player + "-Deaths").GetComponent<Text>();
        
        if (player == 0)
        {
            //characterController.enabled = false;
            //m_Rigidbody = this.gameObject.AddComponent<Rigidbody>();
            //m_Rigidbody.useGravity = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 0)
         {
             MouseKeyboardMovement();
         }
         else
         {
             ControllerMovement();
         }
        
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    void FixedUpdate()
    {
        HealthBar.fillAmount = (Health/100);
        DeathCount.text = Deaths.ToString();
        
        if (cooldown > 0)
        {
            cooldown--;
        }

        if (Health <= 0)
        {
            Health = 100;
            Deaths++;
        }
    }

    void MouseKeyboardMovement()
    {
        
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * movementSpeed;
        
        //rotationVector2.y = (-Input.GetAxis("Rotate")) * movementSpeed;
        //rotationVector2.z = 0;
        //rotationVector2.x = 0;
        
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        //transform.Rotate(rotationVector2);
        
        
        /*
        if (Input.GetKey(KeyCode.A))
            m_Rigidbody.AddForce(Vector3.left * movementSpeed);
        if (Input.GetKey(KeyCode.D))
            m_Rigidbody.AddForce(Vector3.right * movementSpeed);
        if (Input.GetKey(KeyCode.W))
            m_Rigidbody.AddForce(Vector3.forward * movementSpeed);
        if (Input.GetKey(KeyCode.S))
            m_Rigidbody.AddForce(Vector3.back * movementSpeed);
        
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate();
        if (Input.GetKey(KeyCode.E))
            m_Rigidbody.AddForce(Vector3.back * movementSpeed);
        */
        
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }
    
    void ControllerMovement()
    {
        movementVector.x = Input.GetAxis("LeftJoystickX_P" + player) * movementSpeed;
        movementVector.z = (-Input.GetAxis("LeftJoystickY_P" + player)) * movementSpeed;
        movementVector.y = 0;
        
        rotationVector.y = (-Input.GetAxis("RightJoystickX_P" + player)) * movementSpeed;
        rotationVector.z = 0;
        rotationVector.x = 0;
        
        characterController.Move(movementVector * Time.deltaTime);
        transform.Rotate(rotationVector);

        if (Input.GetButtonDown("A_P" + player))
        {
            FireBullet();
        }
    }
    
    void FireBullet()
    {
        if (cooldown == 0)
        {
            cooldown = 60;
            
            GameObject bullet = (GameObject) Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
            bullet.GetComponent<BulletMove>().player = player;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        }
    }

    void Hit(int Attacker)
    {
        Health = Health - 10;
        Debug.Log("Player " + player + " hit by " + Attacker + " Health = " + Health);
    }
}
