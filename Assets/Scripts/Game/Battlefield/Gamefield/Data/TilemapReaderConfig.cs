using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Gameplay.Gamefields
{
    [Serializable]
    public struct TilemapReaderConfig
    {
        [SerializeField] private Tilemap gfTilemap;

        public readonly Tilemap GFTilemap => gfTilemap;
    }
}