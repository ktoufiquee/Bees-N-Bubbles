using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CanvasBehavior : MonoBehaviour
{
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject lifeObject;
    [SerializeField] private GameObject bigButtonObj;
    [SerializeField] private GameObject cooldownObj;
    [SerializeField] private GameObject deadScoreObj;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deadUI;
    [SerializeField] private GameObject aliveUI;
    [SerializeField] private GameObject bigAvailable;
    [SerializeField] private GameObject bigAlternative;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _lifeText;
    private TextMeshProUGUI _lmbText;
    private TextMeshProUGUI _cdText;
    private TextMeshProUGUI _deadScore;
    private long _score;
    private float _life;
    private float _cooldown;
    private bool _badIsGood;
    private bool _isPaused;
    [SerializeField] private float maxCooldown;
    [SerializeField] private CameraBehavior cameraBehavior;
    private const string ACITVATE_BIG = "Activate Bad is Good";
    private const string DEACTIVATE_BIG = "Deactivate Bad is Good";
    
    private void Start()
    {
        _cooldown = 0f;
        _score = 0;
        _life = 50.0f;
        _badIsGood = true;
        _isPaused = false;
        _deadScore = deadScoreObj.GetComponent<TextMeshProUGUI>();
        _scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        _lifeText = lifeObject.GetComponent<TextMeshProUGUI>();
        _lmbText = bigButtonObj.GetComponent<TextMeshProUGUI>();
        _cdText = cooldownObj.GetComponent<TextMeshProUGUI>();
        _scoreText.text = _score.ToString();
        _lifeText.text = _life.ToString("F0");
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1") && _cooldown <= 0)
        {
            _badIsGood = !_badIsGood;
            _lmbText.text = _badIsGood ? DEACTIVATE_BIG : ACITVATE_BIG;
            _cooldown = maxCooldown;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                deadUI.SetActive(false);
                aliveUI.SetActive(true);
                player.SetActive(true);
                Time.timeScale = 1;
            }
            else
            {
                deadUI.SetActive(true);
                aliveUI.SetActive(false);
                player.SetActive(false);
                _deadScore.text = "PAUSED";
                Time.timeScale = 0;
            }
            _isPaused = !_isPaused;
        }

        _cooldown -= Time.deltaTime;
        math.clamp(_cooldown, 0, 5);

        if (_cooldown <= 0)
        {
            bigAvailable.SetActive(true);
            bigAlternative.SetActive(false);
        }
        else
        {
            bigAvailable.SetActive(false);
            bigAlternative.SetActive(true);
            _cdText.text = "(" + _cooldown.ToString("F0") + "s)";
        }

        if (_badIsGood)
        {
            _life -= Time.deltaTime;
            _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
        }

        if (_life <= 0)
        {
            player.SetActive(false);
            deadUI.SetActive(true);
            aliveUI.SetActive(false);
            _deadScore.text = "Score: " + _score.ToString();
        }
    }

    public void ModifyScore(string bubbleTag)
    {
        if (_badIsGood)
        {
            if (bubbleTag.Equals("White Bubble"))
            {
                cameraBehavior.CameraShake();
                _life -= 15;
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
                _life = math.clamp(_life + 5, 0, 100);
                _lifeText.text = Convert.ToInt32(_life).ToString();
            }
            else
            {
                cameraBehavior.CameraShake();
                _life -= 15;
                _lifeText.text = Convert.ToInt32(math.clamp(_life, 0, 100)).ToString();
            }
        }
        
        
    }
}
