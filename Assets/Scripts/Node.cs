using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public GameObject turret;
    public Vector3 positionOffset;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen.");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject() || !buildManager.CanBuild)
        {
            return;
        }

        // Sử dụng phương thức để hiển thị Selection
        buildManager.ShowSelectionAt(GetBuildPosition());
    }

    void OnMouseExit()
    {
        // Sử dụng phương thức để ẩn Selection
        buildManager.HideSelection();
    }
}
