using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridCell", menuName = "Tower Defense/Grid Cell")]
public class GridCellObject : ScriptableObject
{
    public enum CellType
    {
        Path,
        Ground
    }

    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;
}

