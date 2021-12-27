using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    public GameObject[] targetPoint;
    public GameObject[] spawnPoint;
    public GameObject[] bubblePrefab;
    public int maxBubbleCount;
    [HideInInspector] public int bubbleAmount;

    private float[] _lastSpawnTime;

    private void Start()
    {
        _lastSpawnTime = new float[spawnPoint.Length];
        for (var i = 0; i < _lastSpawnTime.Length; ++i)
        {
            _lastSpawnTime[i] = 0f;
        }

        bubbleAmount = 0;
    }

    private void Update()
    {
        for (var i = 0; i < _lastSpawnTime.Length; ++i)
        {
            _lastSpawnTime[i] -= Time.deltaTime;
        }
        if (bubbleAmount >= maxBubbleCount)
        {
            return;
        }
        var spawnIndex = GetRandIndex();
        if (spawnIndex == -1)
        {
            return;
        }

        var bubbleIndex = Random.Range(0, bubblePrefab.Length);
        var bubble = Instantiate(bubblePrefab[bubbleIndex], spawnPoint[spawnIndex].transform.position, Quaternion.identity);
        var bubbleAtt = bubble.GetComponent<BubbleBehavior>();
        bubbleAtt.target = targetPoint[Random.Range(0, targetPoint.Length)];
        bubbleAtt.speed = Random.Range(0.5f, 2f);
        _lastSpawnTime[spawnIndex] = 2f;
        bubbleAmount++;
    }

    private int GetRandIndex()
    {
        var randIndex = Random.Range(0, spawnPoint.Length);
        if (_lastSpawnTime[randIndex] <= 0f)
        {
            return randIndex;
        }

        for (var i = 0; i < spawnPoint.Length; ++i)
        {
            if (_lastSpawnTime[i] <= 0f) return i;
        }

        return -1;
    }
}
