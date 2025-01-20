using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Vector3> pathPoints;
    private int currentPointIndex = 0;
    public float speed = 2f;

    void Start()
    {
        if (pathPoints == null || pathPoints.Count == 0)
        {
            Debug.LogError("No path points assigned to the enemy.");
            return;
        }

        // Đặt vị trí ban đầu tại điểm đầu tiên trong danh sách
        transform.position = pathPoints[0];
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Count == 0)
            return;

        // Di chuyển dần dần tới điểm tiếp theo
        Vector3 targetPosition = pathPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Khi đạt đến điểm hiện tại, chuyển sang điểm tiếp theo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Count)
            {
                // Kết thúc đường đi, có thể xử lý gì đó nếu cần
                Destroy(gameObject);
            }
        }
    }
}
