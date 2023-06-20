using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockNeutral : MonoBehaviour
{
    public bool isPlaceable = true;

    private void OnMouseOver() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlaceable)
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else 
            {
                print("Can't place here!");
            }
        }
    }
}
