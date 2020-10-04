using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Block List")]
public class BlockPrefabsReference : ScriptableObject
{
    [SerializeField] private List<GameObject> _blockList = new List<GameObject>();
    public List<GameObject> BlockList => _blockList;
}