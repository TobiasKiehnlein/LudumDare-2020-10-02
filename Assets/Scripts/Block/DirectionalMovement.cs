using UnityEngine;

[CreateAssetMenu(menuName = "Game/Movement/Directional Movement")]
public class DirectionalMovement : BlockMovementTemplate, ITeleport
{
    [SerializeField] private bool _isTeleportingWhenOutOfRange;
    [SerializeField] private Vector2 _direction;
    
    public bool IsTeleportingWhenOutOfRange => _isTeleportingWhenOutOfRange;
    public Vector2 Direction => _direction;

    public override void Move(Rigidbody2D rb)
    {
        var direction = new Vector3(_direction.normalized.x * _speed, _direction.normalized.y * _speed);
        rb.velocity = direction;
    }
}