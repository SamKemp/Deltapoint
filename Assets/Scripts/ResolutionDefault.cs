using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDefault : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Dropdown>().value = 2;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }
}
