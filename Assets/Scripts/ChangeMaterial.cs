using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ChangeMaterial : MonoBehaviour
{
    public Material material;
    private Material startMaterial;
    public int indexOfMaterial = -1;
    public MeshRenderer pasteRend;
    [HideInInspector] public bool changed;
    private Material[] materials;
    private Renderer m_Renderer;

    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        materials = m_Renderer.materials;
        if (indexOfMaterial >= 0)
            startMaterial = materials[indexOfMaterial];
        changed = false;
    }

    public void Change()
    {
        if (indexOfMaterial >= 0)
        {
            materials[indexOfMaterial] = material;
            m_Renderer.materials = materials;
        }
        if (pasteRend)
        {
            pasteRend.enabled = true;
        }
        changed = true;
    }

    public void ChangeBack()
    {
        if (indexOfMaterial >= 0)
        {
            materials[indexOfMaterial] = startMaterial;
            m_Renderer.materials = materials;
        }
        if (pasteRend)
        {
            pasteRend.enabled = false;
        }
        changed = false;
    }

}
