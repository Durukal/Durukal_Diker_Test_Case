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

    private int _score;

    public Transform ring;

    [SerializeField]
    private Vector3 _maxScale;

    [SerializeField]
    private Vector3 _minScale;

    [SerializeField]
    private float _scaleChangeSpeed;

    [SerializeField]
    private float _scaleChangeDuration;

    private bool _isIncreasing = true;


    private void Awake() {
        _currentMaterialIndex = 0;
        ChangeBackgroundColor();
        _score = 1;
        _scoreText.SetText(_score.ToString());
        ring.localScale = new Vector3(5.1f, 5.1f, 5.1f);
    }

    // IEnumerator Start() {
    //     while (true) {
    //         yield return ChangeScale(true);
    //         yield return ChangeScale(false);
    //     }
    // }
    // private void Start() {
    //     StartCoroutine(ChangeScale(true));
    // }

    void Update() {
        if (Input.GetKeyUp(KeyCode.E)) {
            ChangeBackgroundColor();
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            UpdateScore();
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            _isIncreasing = !_isIncreasing;
        }

        if (Input.GetMouseButtonDown(0)) {
            _isIncreasing = !_isIncreasing;
        }
    }

    private void FixedUpdate() {
        if (ring.localScale.x > _minScale.x && ring.localScale.x < _maxScale.x) {
            ChangeScale(_isIncreasing);
        }
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
}