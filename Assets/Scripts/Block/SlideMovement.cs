using UnityEngine;

[CreateAssetMenu(menuName = "Game/Movement/Slide Movement")]
public class SlideMovement : BlockMovementTemplate
{
    [SerializeField] private float _timeBetweenMoves = 2.0f;
    [SerializeField] private Vector2 _direction;

    [SerializeField] private float _time;
    private bool _hasMoved;
    private Vector2 _targetPosition;
    private bool _targetSet;

    public override void Move(Rigidbody2D rb)
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            var direction = _hasMoved ? _direction * 1 : _direction * -1;
            if (!_targetSet)
            {
                _targetPosition = rb.position + direction;
                _targetSet = true;
            }

            var nextMovement = new Vector3(_direction.normalized.x * _speed, _direction.normalized.y * _speed);
            rb.velocity = nextMovement;
            
            if (Vector2.Distance(rb.transform.position, _targetPosition) > .2f)
            {
                return;
            }

            rb.transform.position = _targetPosition;
            _hasMoved = !_hasMoved;
            _time = _timeBetweenMoves;
            _targetSet = false;
        }
    }
}