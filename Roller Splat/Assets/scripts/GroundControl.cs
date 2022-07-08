using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    public bool isİtRolling = false;

    // When ball rolling on ground change the ground color.
    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
        isİtRolling = true;
        GameManager.singleton.checkGroundComplete();

    }
}


