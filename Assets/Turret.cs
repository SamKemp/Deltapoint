using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    
    private float Health = 100;
    
    private int cooldown = 60;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FireBullet();
    }
    
    void FixedUpdate()
    {        
        if (cooldown > 0)
        {
            cooldown--;
        }
    }
    
    void FireBullet()
    {
        if (cooldown == 0)
        {
            cooldown = 60;
            
            GameObject bullet = (GameObject) Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);
            bullet.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
            bullet.GetComponent<BulletMove>().player = -1;
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 10;
        }
    }
}
