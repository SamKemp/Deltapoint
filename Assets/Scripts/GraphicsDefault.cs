using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsDefault : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Dropdown>().value = QualitySettings.GetQualityLevel();
    }
}
