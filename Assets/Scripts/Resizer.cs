using System;
using UnityEngine;

public class Resizer : MonoBehaviour
{
    [SerializeField] private Vector2 _maximumSize;
    [SerializeField] private Camera _camera;
    private void Start()
    {
        OnRectTransformDimensionsChange();
    }
    private void OnRectTransformDimensionsChange()
    {
        _camera.orthographicSize = Mathf.Min(_maximumSize.x / _camera.aspect, _maximumSize.y) / 2;
    }
}
