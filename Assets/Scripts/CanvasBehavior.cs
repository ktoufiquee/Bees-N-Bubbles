using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasBehavior : MonoBehaviour
{
    public GameObject scoreObject;
    public GameObject lifeObject;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject DeadUI;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _lifeText;
    private long _score;
    private float _life;
    private bool _badIsGood;

    private void Start()
    {
        _score = 0;
        _life = 50.0f;
        _badIsGood = true;
        _scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        _lifeText = lifeObject.GetComponent<TextMeshProUGUI>();
        _scoreText.text = _score.ToString();
        _lifeText.text = _life.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            _badIsGood = !_badIsGood;
        }
        
        if (_badIsGood)
        {
            _life -= Time.deltaTime;
            _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
        }

        if (_life <= 0)
        {
            Player.SetActive(false);
            DeadUI.SetActive(true);
        }
    }

    public void ModifyScore(string bubbleTag)
    {
        if (_badIsGood)
        {
            if (bubbleTag.Equals("White Bubble"))
            {
                _life -= 10;
                _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
            }
            else
            {
                _score++;
                _scoreText.text = _score.ToString();
            }
        }
        else
        {
            if (bubbleTag.Equals("White Bubble"))
            {
                _life += 5;
                _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
            }
            else
            {
                _life -= 10;
                _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
            }
        }
        
        
    }
}
