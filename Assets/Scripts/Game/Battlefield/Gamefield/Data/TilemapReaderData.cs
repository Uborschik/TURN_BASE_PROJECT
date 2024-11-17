using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gameplay
{
    [Serializable]
    public struct TilemapReaderData
    {
        [SerializeField] private Tilemap gfTilemap;

        public readonly Tilemap GFTilemap => gfTilemap;
    }
}