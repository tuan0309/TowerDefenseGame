using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
