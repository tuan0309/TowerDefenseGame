using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public ParticleSystem impactFX;
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = 15f * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (impactFX != null)
        {
            ParticleSystem impactInstance = Instantiate(impactFX, transform.position, transform.rotation);
            Destroy(impactInstance.gameObject, 2f); // Xóa hiệu ứng sau 2 giây
        }
        
        Destroy(target.gameObject);
        Destroy(gameObject);
    }

}
