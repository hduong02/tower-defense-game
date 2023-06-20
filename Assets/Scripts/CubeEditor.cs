using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void  Awake() {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        Vector2 gridPos = waypoint.GetGridPos();
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = gridPos.x + "," + gridPos.y;
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            0f,
            Mathf.Round(transform.position.z / gridSize) * gridSize
        );
    }
}
