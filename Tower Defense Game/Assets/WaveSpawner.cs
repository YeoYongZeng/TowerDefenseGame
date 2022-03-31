using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public float timeBetweenWaves = 5f;

    private float countDown = 5f;

    private int waveIndex = 0;
    public Text waveCountDownText;

    // Update is called once per frame
    void Update()
    {
        if (countDown < 1f)
        {
            waveIndex++;
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;
        waveCountDownText.text = Mathf.Round(countDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            Instantiate(enemyPrefab);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
