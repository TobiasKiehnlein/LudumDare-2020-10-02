using Packages.Rider.Editor;
using UnityEngine;

public abstract class BlockMovementTemplate : ScriptableObject
{
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected float _speed;

    public Sprite Icon => _icon;

    public abstract void Move(Rigidbody2D rb);
    public virtual void Initialize(Rigidbody2D rb){}
}