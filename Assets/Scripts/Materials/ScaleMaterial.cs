using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMaterial : MonoBehaviour
{
    MeshRenderer mr;
    private Material mat;

    // By default scales by Y
    [field: SerializeField] private bool TileYByZScaling { get; set; } = false;

    // Set this to apply custom tiling
    [field: SerializeField] private float TilingX { get; set; } = 0;
    [field: SerializeField] private float TilingY { get; set; } = 0;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;

        float tilingX = TilingX == 0 ?
            this.transform.localScale.x * mr.material.mainTextureScale.x
            : TilingX;
        float tilingY = TilingY == 0 ?
            (TileYByZScaling ?
                this.transform.localScale.z * mr.material.mainTextureScale.y
                : this.transform.localScale.y * mr.material.mainTextureScale.y)
            : TilingY;
        //Debug
        //    .Log("" + tilingX + " " + tilingY);
        mat.mainTextureScale = new Vector2(tilingX, tilingY);
        mr.material = mat;
    }
}
