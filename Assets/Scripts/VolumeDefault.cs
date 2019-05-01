using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeDefault : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = GetComponent<Slider>().value;
    }
}
