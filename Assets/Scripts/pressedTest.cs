using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressedTest : MonoBehaviour
{
    public bool m_pressed;
    public bool pressed
    {
        get => m_pressed;
        set => m_pressed = value;
    }

    private void Start()
    {
        pressed = false;
    }

    private void Update()
    {
        if (pressed == false)
        {
            return;
        } else
        {
            sayHello();
            pressed = false;
        }
    }

    void sayHello()
    {
        Debug.Log("Hellloo!");
    }
}
