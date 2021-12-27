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
    public GameObject bigButtonObj;
    public GameObject cooldownObj;
    public GameObject deadScoreObj;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject DeadUI;
    [SerializeField] private GameObject AliveUI;
    [SerializeField] private GameObject BigAvailable;
    [SerializeField] private GameObject BigAlternative;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _lifeText;
    private TextMeshProUGUI _lmbText;
    private TextMeshProUGUI _cdText;
    private TextMeshProUGUI _deadScore;
    private long _score;
    private float _life;
    private bool _badIsGood;
    private float _cooldown;
    [SerializeField] private float _maxCooldown;

    private const string ACITVATE_BIG = "Activate Bad is Good";
    private const string DEACTIVATE_BIG = "Deactivate Bad is Good";
    private void Start()
    {
        _cooldown = 0f;
        _score = 0;
        _life = 50.0f;
        _badIsGood = true;
        _deadScore = deadScoreObj.GetComponent<TextMeshProUGUI>();
        _scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        _lifeText = lifeObject.GetComponent<TextMeshProUGUI>();
        _lmbText = bigButtonObj.GetComponent<TextMeshProUGUI>();
        _cdText = cooldownObj.GetComponent<TextMeshProUGUI>();
        _scoreText.text = _score.ToString();
        _lifeText.text = _life.ToString();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") && _cooldown <= 0)
        {
            _badIsGood = !_badIsGood;
            _lmbText.text = _badIsGood ? DEACTIVATE_BIG : ACITVATE_BIG;
            _cooldown = _maxCooldown;
        }

        _cooldown -= Time.deltaTime;
        math.clamp(_cooldown, 0, 5);

        if (_cooldown <= 0)
        {
            BigAvailable.SetActive(true);
            BigAlternative.SetActive(false);
        }
        else
        {
            BigAvailable.SetActive(false);
            BigAlternative.SetActive(true);
            _cdText.text = "(" + _cooldown.ToString("F0") + "s)";
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
            AliveUI.SetActive(false);
            _deadScore.text = "Score: " + _score.ToString();
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
