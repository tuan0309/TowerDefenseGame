using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void SelectAnotherTurret()
    {
        buildManager.SetTurretToBuild(buildManager.anotherTurretPrefab);
    }
}
