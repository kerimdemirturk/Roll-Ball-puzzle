using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public bool is›tRolling = false;

    // When ball rolling on ground change the ground color.
    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        is›tRolling = true;
        GameManager.singleton.checkGroundComplete();

    }
}


