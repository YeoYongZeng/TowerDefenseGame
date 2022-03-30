using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public float speed = 5f;

    private Transform _target;

    private int _wavePointIndex;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(3.0f, 5.0f);
        _wavePointIndex = 0;
        UpdateWaypointIndex();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate( Time.deltaTime * speed * dir.normalized, Space.World);
 
        if(Vector3.Distance(transform.position, _target.position) > 0.4f)
            return;
        
        UpdateSpeed();
        _wavePointIndex++;
        if (Waypoints.Points.Length > _wavePointIndex)
        {
            UpdateWaypointIndex();
            return;
        }

        Destroy(gameObject);
    }

    void UpdateSpeed()
    {
        speed *= 1.3f;
    }
    
    void UpdateWaypointIndex()
    {
        _target = Waypoints.Points[_wavePointIndex];
    }
}
