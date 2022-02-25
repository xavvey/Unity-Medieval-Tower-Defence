using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways] // run in Edit and Game mode. Use [ExecuteAlways] with care! 
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultTileColor = Color.white;
    [SerializeField] Color blockedTileColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro tileLabel;
    Vector2Int tileCoordinates = new Vector2Int();
    GridManager gridManager;

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        tileLabel = GetComponent<TextMeshPro>();
        tileLabel.enabled = false;
        
        DisplayCoordinates();  
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }   

        SetLabelColor();
        ToggleLables();
    }

    private void ToggleLables()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            tileLabel.enabled = !tileLabel.IsActive();
        }
    }

    private void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(tileCoordinates);

        if(node == null) { return; }

        if(!node.isWalkable)
        {
            tileLabel.color = blockedTileColor;
        } 
        else if(node.isPath)
        {
            tileLabel.color = pathColor;
        }
        else if(node.isExplored)
        {
            tileLabel.color = exploredColor;
        }
        else
        {
            tileLabel.color = defaultTileColor;
        }

    }

    private void DisplayCoordinates()
    {
        tileCoordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        tileCoordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        tileLabel.text = tileCoordinates.x + "," + tileCoordinates.y;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = tileCoordinates.ToString();
    }
}
