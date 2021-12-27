using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject collParticle;
    public float speed;
    public float speedUpperLimit;

    private CanvasBehavior _scoreManager;
    private GameObject _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager");
        _scoreManager = _gameManager.GetComponent<CanvasBehavior>();
    }


    private void Update()
    {
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position,
            speed * Time.deltaTime);

        if (Vector2.Distance(gameObject.transform.position, target.transform.position) <= 0)
        {
            _gameManager.GetComponent<SpawnBubble>().bubbleAmount--;
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _scoreManager.ModifyScore(gameObject.tag);
        var collParticleObj = Instantiate(collParticle, other.gameObject.transform.position, Quaternion.identity);
        collParticleObj.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
        Destroy(collParticleObj, 1.2f);
        _gameManager.GetComponent<SpawnBubble>().bubbleAmount--;
    }
    
}
