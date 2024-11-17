using UnityEngine;

namespace Utils
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public static class Directions2D
    {
        public static Vector3Int[] cardinalDirections = new Vector3Int[]
        {
            new(0, 1),
            new(1, 0),
            new(0, -1),
            new(-1, 0)
        };

        public static Vector3Int[] diagonalDirections = new Vector3Int[]
        {
            new( 1,  1),
            new(-1,  1),
            new(-1, -1),
            new( 1, -1)
        };

        public static Vector3Int[] eightDirections = new Vector3Int[]
        {
            new( 0,  1),
            new( 1,  1),
            new( 1,  0),
            new( 1, -1),
            new( 0, -1),
            new(-1, -1),
            new(-1,  0),
            new(-1,  1)
        };

        public static Vector3Int GetRandomCardinalDirection()
        {
            return cardinalDirections[Random.Range(0, cardinalDirections.Length)];
        }

        public static Vector3Int GetRandomDiagonalDirection()
        {
            return cardinalDirections[Random.Range(0, diagonalDirections.Length)];
        }

        public static Vector3Int GetRandomEightDirection()
        {
            return cardinalDirections[Random.Range(0, eightDirections.Length)];
        }

        public static Direction GetOppositeDirectionTo(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Right => Direction.Left,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                _ => direction,
            };
        }
    }
}