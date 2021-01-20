using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public List<ISwitchable> gameObjects;
    public readonly List<int> start = new List<int>
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwapOneToOne(int turnOn, int turnOff)
    {
        this.gameObjects[turnOff].Close();
        this.gameObjects[turnOn].Open();
    }

    void SwapManyToOne(int turnOn, List<int> turnOff)
    {
        foreach(int el in turnOff) this.gameObjects[el].Close();
        this.gameObjects[turnOn].Open();
    }

    void Swap(List<int> turnOn, List<int> turnOff)
    {
        foreach(int el in turnOff) this.gameObjects[el].Close();
        foreach(int el in turnOn) this.gameObjects[el].Open();
    }
}
