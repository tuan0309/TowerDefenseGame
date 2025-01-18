using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 100;
    public int gold = 50;
    private Transform target;
    private int wavepointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {   
        PlayerStats.Money += gold;
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (dir.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        else
        {
            if (dir.z > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (dir.z < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        // Di chuyển enemy về hướng waypoint
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Kiểm tra khoảng cách tới waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath(); // Hủy đối tượng khi đi hết waypoint
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
