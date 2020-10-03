﻿using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BlockMovementSystem : MonoBehaviour
{
    [SerializeField] private List<BlockMovementTemplate> _blockMovementTemplates = new List<BlockMovementTemplate>();

    private BlockMovementTemplate _selectedMovement;

    public BlockMovementTemplate SelectedMovement => _selectedMovement;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        var randomId = Random.Range(0, _blockMovementTemplates.Count);
        _selectedMovement = _blockMovementTemplates[randomId];
    }

    private void FixedUpdate()
    {
        _selectedMovement.Move(_rigidbody2D);
    }
}