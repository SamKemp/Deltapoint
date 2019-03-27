using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour
{
    [FormerlySerializedAs("BulletPrefab")] public GameObject bulletPrefab;
    [FormerlySerializedAs("BulletSpawn")] public Transform bulletSpawn;
    
    private float _health = 100;
    
    private int _cooldown = 0;
    private int _cooldownWait = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        _cooldown = _cooldownWait;
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
    }
    
    void FixedUpdate()
    {        
        if (_cooldown > 0)
        {
            _cooldown--;
        }
        
        if (_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    void FireBullet()
    {
        if (_cooldown == 0)
        {
            _cooldown = _cooldownWait;
            
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
            bullet.GetComponent<BulletMove>().player = -1;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        }
    }
    
    void Hit(int Attacker)
    {
        _health = _health - 50;
        //Debug.Log("Turret hit by " + Attacker + " | Health = " + _health);
    }
}
