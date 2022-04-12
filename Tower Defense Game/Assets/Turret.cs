using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    
    [Header("Unity Setup Fields")]
    public Transform target;
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float speed = 10f;
    
    private string _enemyTag = "Enemy";
    private float _fireCountDown = 0f;
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
        if (_fireCountDown <= 0f)
        {
            //Shoot();
            _fireCountDown = 1f / fireRate;
        }

        _fireCountDown -= Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
