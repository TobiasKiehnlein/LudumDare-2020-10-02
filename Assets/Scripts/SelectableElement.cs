using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableElement : MonoBehaviour
{
    public bool isPlaced = false;
    [SerializeField] private float blockScrollSpeed = 5;
    [SerializeField] private float snapBackDistance = 5;
    private bool _dragging;
    private Vector3 _destinationPosition;
    private Vector3 _initalPosition;
    private Vector3 _initialOffset;

    // Start is called before the first frame update
    void Start()
    {
        _initalPosition = transform.position;
        _destinationPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dragging && !transform.position.Equals(_destinationPosition))
        {
            transform.position = Vector3.Lerp(transform.position, _destinationPosition, blockScrollSpeed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MouseDown");
        _dragging = true;

        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        _initialOffset = transform.position - objPosition;
        _initalPosition = transform.position;
    }

    private void OnMouseUp()
    {
        _dragging = false;
        // if (!((_initalPosition - transform.position).y < snapBackDistance) || isPlaced) return;

        if ((_initalPosition - transform.position).magnitude > snapBackDistance || isPlaced) return;

        _destinationPosition = _initalPosition;
        isPlaced = true;
        Debug.Log("Snap back");
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
        _destinationPosition = objPosition;
    }
}