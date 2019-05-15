using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public int player;

    private GameObject _model;
	
	private int _controller;
    
    private Vector3 _movementVector;
    private Vector3 _rotationVector;
    
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _rotationVector2;
    
    private CharacterController _characterController;
    
    private float movementSpeed = 8;
    //private float _rotateSpeed = 40.0f;
    
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    private float _health = 100;
    private Image _healthBar;

    //private int _lives = 3;
    
    private int Deaths = 0;
    private Text _deathCount;

    private int _cooldown = 0;
    private int _cooldownWait = 15;
    
    Rigidbody _rigidbody;

    private bool _dead = false;

    //private GameObject _rotationPoint;

    public bool keyboard = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = player;

        _model = transform.GetChild(0).gameObject;
        //_model.AddComponent<HitRelay>();

        //_rotationPoint = transform.GetChild(1).gameObject;
        
        _characterController = GetComponent<CharacterController>();
        _healthBar = GameObject.Find("P" + player + "-Health").GetComponent<Image>();
        _deathCount = GameObject.Find("P" + player + "-Deaths").GetComponent<Text>();
        _deathCount.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_dead)
        {
            if (player == 1 && keyboard)
            {
                DEADMouseKeyboardMovement();
            }
            else
            {
                DEADControllerMovement();
            }
        }
        else
        {
            if (player == 1 && keyboard)
            {
                MouseKeyboardMovement();
            }
            else
            {
                ControllerMovement();
            }
        }
        
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }

    private void DEADMouseKeyboardMovement()
    {
        //NOOP
    }

    private void DEADControllerMovement()
    {
        //NOOP
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
            Dead();
        }
    }

    private void MouseKeyboardMovement()
    {
        
        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection = _moveDirection * movementSpeed;
        
        //rotationVector.y = (-Input.GetAxis("Rotate")) * movementSpeed;
        //rotationVector.z = 0;
        //rotationVector.x = 0;
        
        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
        //transform.Rotate(rotationVector);
        
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            FireBullet();
        }
    }
    
    private void ControllerMovement()
    {
        _movementVector.x = Input.GetAxis("LeftJoystickX_P" + _controller) * movementSpeed;
        _movementVector.z = (-Input.GetAxis("LeftJoystickY_P" + _controller)) * movementSpeed;
        _movementVector.y = 0;
        
        Vector3 rotationPointThingy = new Vector3(Input.GetAxis("RightJoystickY_P" + _controller) * 2, 0, Input.GetAxis("RightJoystickX_P" + _controller) * 2);

        //_rotationPoint.transform.localPosition = rotationPointThingy;
        
        // The step size is equal to speed times frame time.
        float step = 10 * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(_model.transform.forward, rotationPointThingy, step, 0.0f);
        
        _model.transform.localRotation = Quaternion.LookRotation(newDir);
        
        _characterController.Move(_movementVector * Time.deltaTime);

        if (Input.GetButtonDown("A_P" + _controller) || Input.GetAxis("RightTrigger_P" + _controller) > 0.5f)
        {
            FireBullet();
        }

        if (player == 1 && Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private void FireBullet()
    {
        if (_cooldown == 0)
        {
            _cooldown = _cooldownWait;
            
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = _model.GetComponent<Renderer>().material;
            bullet.GetComponent<BulletMove>().player = player;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        }
    }

    public void Hit(int Attacker)
    {
        _health = _health - 50;
    }

    private void Dead()
    {
        _dead = true;
        _healthBar.fillAmount = (_health/100);
        
        Invoke("Kill", 1);
        
        //gameObject.tag = "DeadPlayer";
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
