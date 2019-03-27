using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public int player;
	
	private int _controller;
    
    private Vector3 _movementVector;
    private Vector3 _rotationVector;
    
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _rotationVector2;
    
    private CharacterController _characterController;
    
    private float movementSpeed = 8;
    private float _rotateSpeed = 40.0f;
    
    [FormerlySerializedAs("BulletPrefab")] public GameObject bulletPrefab;
    [FormerlySerializedAs("BulletSpawn")] public Transform bulletSpawn;
    
    private float _health = 100;
    private Image _healthBar;

    private int _lives = 3;
    
    private int Deaths = 0;
    private Text _deathCount;

    private int _cooldown = 0;
    private int _cooldownWait = 15;
    
    Rigidbody _rigidbody;

    public bool keyboard = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = player;
		
        _characterController = GetComponent<CharacterController>();
        _healthBar = GameObject.Find("P" + player + "-Health").GetComponent<Image>();
        _deathCount = GameObject.Find("P" + player + "-Deaths").GetComponent<Text>();
        _deathCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 1 && keyboard)
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
        _healthBar.fillAmount = (_health/100);
        _deathCount.text = Deaths.ToString();
        
        if (_cooldown > 0)
        {
            _cooldown--;
        }

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MouseKeyboardMovement()
    {
        
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection = _moveDirection * movementSpeed;
        
        //rotationVector2.y = (-Input.GetAxis("Rotate")) * movementSpeed;
        //rotationVector2.z = 0;
        //rotationVector2.x = 0;
        
        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
        //transform.Rotate(rotationVector2);
        
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }
    
    void ControllerMovement()
    {
        _movementVector.x = Input.GetAxis("LeftJoystickX_P" + _controller) * movementSpeed;
        _movementVector.z = (-Input.GetAxis("LeftJoystickY_P" + _controller)) * movementSpeed;
        _movementVector.y = 0;
        
        _rotationVector.y = (-Input.GetAxis("RightJoystickX_P" + _controller)) * movementSpeed;
        _rotationVector.z = 0;
        _rotationVector.x = 0;
        
        _characterController.Move(_movementVector * Time.deltaTime);
        transform.Rotate(_rotationVector);

        //float controllerX = Input.GetAxisRaw("RightJoystickX_P" + _controller);
        //float controllerY = Input.GetAxisRaw("RightJoystickY_P" + _controller);

        //float rotateAngle = 0.0f;

        //rotateAngle = (Mathf.Atan2(controllerX, controllerY) * Mathf.Rad2Deg) + transform.eulerAngles.y;
        
        //transform.rotation = new Quaternion(0, rotateAngle, 0, 0);

        if (Input.GetButtonDown("A_P" + _controller) || Input.GetAxis("RightTrigger_P" + _controller) > 0.5f)
        {
            FireBullet();
        }
    }
    
    void FireBullet()
    {
        if (_cooldown == 0)
        {
            _cooldown = _cooldownWait;
            
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
            bullet.GetComponent<BulletMove>().player = player;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        }
    }

    void Hit(int Attacker)
    {
        _health = _health - 50;
        //Debug.Log("Player " + player + " hit by " + Attacker + " | Health = " + _health);
    }
}
