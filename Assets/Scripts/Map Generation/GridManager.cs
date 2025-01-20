using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 25;
    public int scaleObj = 5;
    public GameObject pathTile;
    public GridCellObject[] gridCells;
    public GridCellObject[] sceneryCells;
    private GameObject pathParent;
    private GameObject sceneryParent;
    private GameObject pathPointsParent;
    private List<Vector2Int> pathCells;

    private PathGenerator pathGenerator;

    // void Start()
    // {
    //     pathParent = new GameObject("PathParent");
    //     sceneryParent = new GameObject("SceneryParent");

    //     pathGenerator = new PathGenerator(gridWidth, gridHeight);

    //     List<Vector2Int> pathCells = pathGenerator.GeneratePath();
    //     int pathSize = pathCells.Count;

    //     while (pathSize < minPathLength)
    //     {
    //         pathCells = pathGenerator.GeneratePath();
    //         pathSize = pathCells.Count;
    //     }

    //     StartCoroutine(LayPathCells(pathCells));
    //     StartCoroutine(LaySceneryCells());

    //     SpawnEnemy();
    // }

    void Start()
    {
        pathParent = new GameObject("PathParent");
        sceneryParent = new GameObject("SceneryParent");
        pathPointsParent = new GameObject("Waypoints");

        pathGenerator = new PathGenerator(gridWidth, gridHeight);

        pathCells = pathGenerator.GeneratePath();
        int pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }

        // Tạo các điểm đánh dấu đường đi
        CreatePathPointMarkers(pathCells);

        StartCoroutine(LayPathCells(pathCells));
        StartCoroutine(LaySceneryCells());
    }



    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.getCellNeightbourValue(pathCell.x, pathCell.y);
            GameObject pathTile = gridCells[neighbourValue].cellPrefab;

            Vector3 worldPosition = new Vector3(pathCell.x * scaleObj, 0f, pathCell.y * scaleObj);
            GameObject pathTileCell = Instantiate(pathTile, worldPosition, Quaternion.identity);

            pathTileCell.transform.SetParent(pathParent.transform);
            pathTileCell.transform.Rotate(0f, gridCells[neighbourValue].yRotation, 0f, Space.Self);

            // Debug.Log("Tile " + pathCell.x + "," + pathCell.y + "." + "neighbour value=" + neighbourValue);
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

    IEnumerator LaySceneryCells()
    {
        Debug.Log("Laying scenery cells");

        // Vòng lặp qua tất cả các ô trong grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Kiểm tra nếu ô này là một phần của đường đi
                if (!pathGenerator.CellIsEmpty(x, y))
                {
                    // Nếu là đường, bỏ qua ô này
                    continue;
                }

                // Sinh scenery cho ô không phải đường
                int randomSceneryCellIndex = Random.Range(0, sceneryCells.Length);
                GameObject sceneryTile = sceneryCells[randomSceneryCellIndex].cellPrefab;

                Vector3 worldPosition = new Vector3(x * scaleObj, 0f, y * scaleObj);
                GameObject sceneryTileCell = Instantiate(sceneryTile, worldPosition, Quaternion.identity);

                sceneryTileCell.transform.SetParent(sceneryParent.transform);

                yield return new WaitForSeconds(0.01f);
            }
            yield return null;
        }
    }

    private void CreatePathPointMarkers(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            Vector3 worldPosition = new Vector3(pathCell.x * scaleObj, 0.5f, pathCell.y * scaleObj);

            GameObject pathPoint = new GameObject($"PathPoint ({pathCell.x}, {pathCell.y})");
            pathPoint.transform.position = worldPosition;

            pathPoint.transform.SetParent(pathPointsParent.transform);
        }
    }

    // private void SpawnEnemy()
    // {
    //     if (enemyPrefab == null)
    //     {
    //         Debug.LogError("Enemy Prefab is not assigned in GridManager!");
    //         return;
    //     }

    //     if (pathCells == null || pathCells.Count == 0)
    //     {
    //         Debug.LogError("PathCells is null or empty. Ensure path is generated correctly before spawning enemies.");
    //         return;
    //     }

    //     // Chuyển đổi pathCells sang Vector3
    //     List<Vector3> pathPoints = new List<Vector3>();
    //     foreach (Vector2Int cell in pathCells)
    //     {
    //         pathPoints.Add(new Vector3(cell.x * scaleObj, 0.2f, cell.y * scaleObj));
    //     }

    //     GameObject enemy = Instantiate(enemyPrefab, pathPoints[0], Quaternion.identity);

    //     EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
    //     if (enemyMovement != null)
    //     {
    //         enemyMovement.pathPoints = pathPoints;
    //     }
    //     else
    //     {
    //         Debug.LogError("EnemyPrefab is missing the EnemyMovement script!");
    //     }
    // }


}
