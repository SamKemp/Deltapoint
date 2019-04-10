using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRelay : MonoBehaviour
{
    void Hit(int Attacker)
    {
        transform.GetComponentInParent<PlayerMove>().Hit(Attacker);
    }
}
