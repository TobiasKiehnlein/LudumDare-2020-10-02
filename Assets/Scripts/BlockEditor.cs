using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BlockEditor : MonoBehaviour
{
    [SerializeField] private GameObject[] listElements;
    [SerializeField] private float distance = 3;
    [SerializeField] private float scrollBlockSize = 3;
    [SerializeField] private float blockScrollSpeed = 5;
    [SerializeField] private float lerpingThreshould = 0.1f;

    private Transform _localTransform;
    private Vector3 _localPosition;
    private Vector3 _destinationPosition;
    private bool _dragging;
    private Vector3 _initialOffset;

    // Start is called before the first frame update
    void Start()
    {
        _localTransform = transform;
        _localPosition = transform.position;
        _destinationPosition = _localPosition;
        float offset = 0;

        foreach (var listElement in listElements)
        {
            Vector3 position = new Vector3(offset + _localPosition.x, _localPosition.y, _localPosition.z);
            Instantiate(listElement, position, Quaternion.identity).transform.parent = _localTransform;
            offset += distance;
        }
    }

    public void scrollRightBlockwise()
    {
        scrollBlockwise(-scrollBlockSize);
    }

    public void scrollLeftBlockwise()
    {
        scrollBlockwise(scrollBlockSize);
    }

    private void scrollBlockwise(float amount)
    {
        _destinationPosition = new Vector3(amount + _destinationPosition.x, _destinationPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dragging)
        {
            transform.position = Vector3.Lerp(transform.position, _destinationPosition, blockScrollSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        _dragging = true;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        objPosition.y = transform.position.y;

        _initialOffset = transform.position-objPosition;
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        objPosition.y = transform.position.y;

        transform.position = objPosition + _initialOffset;
        _destinationPosition = objPosition + _initialOffset;
    }
}