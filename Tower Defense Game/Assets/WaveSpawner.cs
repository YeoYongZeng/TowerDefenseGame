using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public float timeBetweenWaves = 5f;

    private float _countDown = 5f;

    private int _waveIndex;
    public Text waveCountDownText;

    // Update is called once per frame
    void Update()
    {
        if (_countDown < 1f)
        {
            _waveIndex++;
            StartCoroutine(SpawnWave());
            _countDown = timeBetweenWaves;
        }

        _countDown -= Time.deltaTime;
        waveCountDownText.text = $"{Mathf.Round(_countDown)}";
}

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < _waveIndex; i++)
        {
            Instantiate(enemyPrefab);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
