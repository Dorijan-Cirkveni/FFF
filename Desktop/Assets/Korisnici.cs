﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Korisnici : MonoBehaviour, ISwitchable
{
    //TODO poveznica za dohvat i spremanje podataka o korisnicima

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
