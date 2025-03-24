using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LightManager.Instance.LightsActivated(this);
    }
}
