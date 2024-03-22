using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparency : MonoBehaviour {
    private Material normalMaterial;
    public Material transparentMaterial;

    private MeshRenderer objectRenderer;

    void Start() {
        objectRenderer = GetComponent<MeshRenderer>();
        normalMaterial = objectRenderer.material;
    }
/*
    void Update() {
        objectRenderer.material = normalMaterial;
    }
*/
    public void GetTransparent() {
        objectRenderer.material = transparentMaterial;
        Debug.Log("[ChangeTransparency] GetTransparent");
    }

    public void GetNormal() {
        objectRenderer.material = normalMaterial;
    }

}
