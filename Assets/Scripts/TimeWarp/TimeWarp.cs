using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour
{
    struct Time
    {
        public float posX;
        public float posY;
        public float rotZ;
    }

    [SerializeField] private TimeDirectionReference _timeDirectionReference;
    [SerializeField] private int skiptIterations = 1;
    [SerializeField] private int skipBackTimeInMs = 5000;
    private Rigidbody2D _rb;
    private RigidbodyType2D _rbType;
    private Stack<Time> _timeStore; // posX, posY, rotZ
    private int cycle;

    // Start is called before the first frame update
    void Start()
    {
        _timeStore = new Stack<Time>();
        _rb = GetComponent<Rigidbody2D>();
        _rbType = _rb.bodyType;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeDirection direction = _timeDirectionReference.Value;
        
        var position = transform.position;
        if (direction == TimeDirection.Forward && cycle % (skiptIterations + 1) == 0)
        {
            if (_rb)
            {
                _rb.bodyType = _rbType;
            }

            _timeStore.Push(new Time { posX = position.x, posY = position.y, rotZ = transform.rotation.z });
        }

        if (direction == TimeDirection.Backward)
        {
            if (_rb)
            {
                _rb.bodyType = RigidbodyType2D.Kinematic;
            }

            if (_timeStore.Count == 0) return;

            Time time = _timeStore.Pop();
            transform.position = new Vector3(time.posX, time.posY);
            Quaternion currentRotation = transform.rotation;
            currentRotation.z = time.rotZ;
            transform.rotation = currentRotation;
        }

        cycle++;
    }
}