using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySwitchable : MonoBehaviour, ISwitchable
{
    public bool Close()
    {
        gameObject.SetActive(false);
        return true;
    }

    public bool Open()
    {
        gameObject.SetActive(true);
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
