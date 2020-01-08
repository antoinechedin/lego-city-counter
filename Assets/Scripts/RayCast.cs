﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public Grid g;
    public LayerMask layerMask; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createJSON()
    {
        Map m = new Map();
        m.mapSize = new Vector2Int((int)g.sizeGrid.x, (int)g.sizeGrid.z);
        m.mapScale = g.sizeVoxel;
        m.columns = new List<Column>();

        for (int i = 1; i <= g.sizeGrid.x / g.sizeVoxel; i++)
        {
            List<Column> list = new List<Column>();
            for (int k = 1; k <= g.sizeGrid.z / g.sizeVoxel; k++)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, g.sizeGrid.y / g.sizeVoxel, k) * g.sizeVoxel, Vector3.down, out hit, g.sizeGrid.y, layerMask))
                {
                    Debug.Log(hit.point.y);
                    Debug.Log("Did Hit");

                    Column c = new Column();
                    c.type = Column.Type.Ground;
                    c.height = hit.point.y;
                    m.columns.Add(c);
                }
                else
                {
                    Debug.DrawRay(transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, g.sizeGrid.y / g.sizeVoxel, k) * g.sizeVoxel, Vector3.down * g.sizeGrid.y, Color.white);
                    Debug.Log("No Hit");
                }
            }
        }

        Debug.Log(JsonUtility.ToJson(m));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.75F);

        for (int i = 1; i <= g.sizeGrid.x / g.sizeVoxel; i++)
        {
            for (int k = 1; k <= g.sizeGrid.z / g.sizeVoxel; k++)
            {
                Gizmos.DrawLine(transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, g.sizeGrid.y / g.sizeVoxel, k) * g.sizeVoxel, transform.position + g.basePos + new Vector3(-g.sizeVoxel / 2, 0, -g.sizeVoxel / 2) + new Vector3(i, 0, k) * g.sizeVoxel);
            }
        }
    }
}
