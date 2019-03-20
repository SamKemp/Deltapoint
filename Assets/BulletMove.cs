using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward);
    }
}
