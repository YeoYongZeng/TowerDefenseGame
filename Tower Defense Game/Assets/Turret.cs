using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range = 15f;
    public float speed = 10f;
    public Transform target;
    public Transform partToRotate;
    private string _enemyTag = "Enemy";
    
    void Start()
    {
        InvokeRepeating(nameof(UpdateTarget), 0f, 1f);
    }

    void Update()
    {
        if(target == null)
            return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * speed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    
    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(_enemyTag);
        float nearestEnemyDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            var temp = Vector3.Distance(transform.position, enemy.transform.position);
            if(temp >= nearestEnemyDistance)
                continue;
            nearestEnemyDistance = temp;
            nearestEnemy = enemy;
        }

        if (nearestEnemy == null || nearestEnemyDistance > range)
        {
            target = null;
            return;
        }
        target = nearestEnemy.transform;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
