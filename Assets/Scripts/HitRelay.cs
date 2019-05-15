using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRelay : MonoBehaviour
{
    void Hit(int attacker)
    {
        if (transform.GetComponentInParent<PlayerMove>())
        {
            transform.GetComponentInParent<PlayerMove>().Hit(attacker);
        }
        else if (transform.GetComponentInParent<HitRelay>())
        {
            transform.GetComponentInParent<HitRelay>().Hit(attacker);
        }
    }
}
