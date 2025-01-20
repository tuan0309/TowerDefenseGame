using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public GameObject selectionPrefab; // Tham chiếu đến prefab Selection
    private GameObject selectionInstance; // Instance của prefab

    // private GameObject turretToBuild;
    // public GameObject standardTurretPrefab;
    // public GameObject anotherTurretPrefab;
    // public GameObject lazeTurretPrefab;

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

    private void Start()
    {
        // Tạo instance dùng chung của selectionPrefab
        if (selectionPrefab != null)
        {
            selectionInstance = Instantiate(selectionPrefab);
            selectionInstance.SetActive(false); // Ẩn ban đầu
        }
    }

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret build! Money left: " + PlayerStats.Money);
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    // public TurretBlueprint GetTurretToBuild()
    // {
    //     return turretToBuild;
    // }

    public void ShowSelectionAt(Vector3 position)
    {
        if (selectionInstance != null)
        {
            selectionInstance.transform.position = position;
            selectionInstance.SetActive(true);
        }
    }

    public void HideSelection()
    {
        if (selectionInstance != null)
        {
            selectionInstance.SetActive(false);
        }
    }
}
