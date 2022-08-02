using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Renderer _backgroundRenderer;

    [SerializeField]
    private Material[] _materials;

    private int _currentMaterialIndex;

    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private TMP_Text _endScoreText;

    private int _score;

    public Transform ring;

    [SerializeField]
    private Vector3 _initialScale;

    [SerializeField]
    private Vector3 _maxScale;

    [SerializeField]
    private Vector3 _minScale;

    [SerializeField]
    private float _scaleChangeSpeed;

    [SerializeField]
    private float _scaleChangeDuration;

    private bool _isIncreasing = false;

    public GameObject gameEndUI;
    public GameObject gameStartUI;

    private void Awake() {
        GameStart();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.E)) {
            ChangeBackgroundColor();
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            UpdateScore();
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            if (gameStartUI.activeSelf) {
                gameStartUI.SetActive(false);
                _scoreText.gameObject.SetActive(true);
                ring.gameObject.SetActive(true);
                Time.timeScale = 1;
            }

            _isIncreasing = !_isIncreasing;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (gameStartUI.activeSelf) {
                gameStartUI.SetActive(false);
                _scoreText.gameObject.SetActive(true);
                ring.gameObject.SetActive(true);
                Time.timeScale = 1;
            }

            if (gameEndUI.activeSelf) {
                ResetScene();
            }

            _isIncreasing = !_isIncreasing;
        }
    }

    private void FixedUpdate() {
        if (ring.localScale.x > _minScale.x && ring.localScale.x < _maxScale.x) {
            ChangeScale(_isIncreasing);
        } else {
            GameOver();
        }
    }

    private void GameStart() {
        gameStartUI.SetActive(true);
        gameEndUI.SetActive(false);
        ring.gameObject.SetActive(false);
        ResetScene();
    }

    private void GameOver() {
        Time.timeScale = 0;
        _endScoreText.text = $"Score:{_scoreText.text}";
        gameEndUI.SetActive(true);
    }

    private void ChangeBackgroundColor() {
        _backgroundRenderer.material.color = _materials[_currentMaterialIndex].color;
        if (_currentMaterialIndex + 1 < _materials.Length) {
            _currentMaterialIndex++;
        } else {
            _currentMaterialIndex = 0;
        }
    }

    private void UpdateScore() {
        _score++;
        _scoreText.SetText(_score.ToString());
    }

    private void ChangeScale(bool isIncreasing) {
        var rate = (1.0f / _scaleChangeDuration) * _scaleChangeSpeed;

        if (isIncreasing) {
            ring.localScale += new Vector3(1f, 1f, 1f) * rate * Time.deltaTime;
        } else {
            ring.localScale -= new Vector3(1f, 1f, 1f) * rate * Time.deltaTime;
        }
    }

    private void ResetScene() {
        _currentMaterialIndex = 0;
        ChangeBackgroundColor();
        _score = 1;
        _scoreText.SetText(_score.ToString());
        ring.localScale = _initialScale;
        _scoreText.gameObject.SetActive(false);
        Time.timeScale = 0;
        if (gameEndUI.activeSelf) {
            gameEndUI.SetActive(false);
        }

        if (!gameStartUI.activeSelf) {
            gameStartUI.SetActive(true);
        }
    }
}