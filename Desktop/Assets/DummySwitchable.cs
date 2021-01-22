﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySwitchable : MonoBehaviour, ISwitchable
{
    public void CloseAsync()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
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
