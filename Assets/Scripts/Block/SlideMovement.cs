using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game/Movement/Slide Movement")]
public class SlideMovement : BlockMovementTemplate
{
    [SerializeField] private float _timeBetweenMoves = 2.0f;
    [SerializeField] private Vector2 _relativeDestination;
    [SerializeField] private int depth = 20;

    private float _time;
    private Vector2 _absoluteDestination;
    private bool _isForwardDirection;
    private float _distance;

    private Vector2 initialPosition;

    public override void Initialize(Rigidbody2D rb)
    {
        initialPosition = rb.position;
        _absoluteDestination = initialPosition + _relativeDestination;
        _time = _timeBetweenMoves;
        _isForwardDirection = true;
        _distance = Vector2.Distance(initialPosition, _absoluteDestination);
    }

    public override void Move(Rigidbody2D rb)
    {
        // if (_time > 0)
        // {
        //     _time -= Time.deltaTime;
        // }
        // else
        // {
        //     rb.transform.position = Vector2.Lerp(rb.transform.position, _isForwardDirection ? _absoluteDestination : initialPosition, _speed * Time.deltaTime);
        //
        //     float distanceToDestination = Vector2.Distance(_isForwardDirection ? _absoluteDestination : initialPosition, rb.transform.position);
        //     if (distanceToDestination > .2f)
        //     {
        //         return;
        //     }
        //
        //     _isForwardDirection = !_isForwardDirection;
        //     _time = _timeBetweenMoves;
        // }
        rb.transform.position = initialPosition + _relativeDestination * calculateDistance(Time.time);
    }

    float calculateDistance(float t)
    {
        float factor = 0;

        for (int k = 1; k < depth; k++)
        {
            factor += Mathf.Sin((2 * k - 1) * 0.25f * Mathf.PI * t) / (2 * k - 1);
        }

        return (4 * 2 / Mathf.PI * factor + 2) / 4;
    }
}