using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<GameObject> input = new List<GameObject>();
    List<ISwitchable> screens = new List<ISwitchable>();
    public int active = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject el in input)
        {
            if (el == null)
            {
                screens.Add(null);
                continue;
            }
            ISwitchable found = el.GetComponent<ISwitchable>();
            if (found != null)
            {
                screens.Add(found);
                found.CloseAsync();
            }
        }
        screens[active].Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Swap(int turnOn)
    {
        Debug.Log(active*10+turnOn);
        screens[active].CloseAsync();
        active = turnOn;
        Debug.Log(active);
        screens[active].Open();
    }
}
