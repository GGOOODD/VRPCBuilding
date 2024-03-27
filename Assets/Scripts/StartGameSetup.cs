using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameSetup : MonoBehaviour
{
    public GameObject StartMenu;
    public Vector3 StartMenuPosition = new(0.0f, 0.0f, 0.0f);
    public Quaternion StartMenuRotation = Quaternion.identity;

    void Start()
    {
        Instantiate(StartMenu, StartMenuPosition, StartMenuRotation);
    }
}
