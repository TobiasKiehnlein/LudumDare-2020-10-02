using UnityEngine;

[CreateAssetMenu(menuName = "Game/Movement/Rotation Movement")]
public class RotationMovement : BlockMovementTemplate
{
    [SerializeField] private bool isMovingClockwise = true;

    public override void Move(Rigidbody2D rb)
    {  
        var direction = isMovingClockwise ? 1 : -1;
        rb.rotation += (_speed * Time.deltaTime) * direction;
    }
}