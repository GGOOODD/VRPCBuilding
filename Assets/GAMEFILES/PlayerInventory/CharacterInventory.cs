using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[System.Serializable]
public class BodySocket
{
    public GameObject gameObject = null;
    [Range(0.01f, 1f)]
    public float heightRatio;
    public float positionX;
    public float positionZ;
}

public class CharacterInventory : MonoBehaviour
{
    public XROrigin XROrigin;
    public BodySocket[] bodySockets;
    public GameObject defaultPrefab;

    private void Start()
    {
        foreach (var bodySocket in bodySockets)
        {
            if (bodySocket != null)
                bodySocket.gameObject = Instantiate(defaultPrefab, this.transform);
        }
    }

    void Update()
    {
        var playerHeight = XROrigin.CameraYOffset;
        var currentHMDRot = XROrigin.Camera.transform.rotation;
        var currentPlayerBodyPos = XROrigin.transform.position;

        foreach (var bodySocket in bodySockets)
        {
            bodySocket.gameObject.transform.localPosition = new Vector3(
                bodySocket.positionX, (playerHeight * bodySocket.heightRatio), bodySocket.positionZ);
        }
        transform.SetPositionAndRotation(
            new Vector3(currentPlayerBodyPos.x, currentPlayerBodyPos.y, currentPlayerBodyPos.z),
            new Quaternion(transform.rotation.x, currentHMDRot.y, transform.rotation.z, currentHMDRot.w)
        );
    }
}
