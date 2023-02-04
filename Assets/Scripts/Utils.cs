using UnityEngine;

public static class Utils
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static Direction GetHorizontalDirection(Vector2 direction)
    {
        switch (direction.x)
        {
            case > 0:
                return Direction.Right;
            case < 0:
                return Direction.Left;
            default:
                return Direction.Right;
        }
    }

    public static Direction GetVerticalDirection(Vector2 direction)
    {
        switch (direction.y)
        {
            case > 0:
                return Direction.Up;
            case < 0:
                return Direction.Down;
            default:
                return Direction.Up;
        }
    }

    public static Vector2 DirectionToVector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
    
    public static Vector2 ToVector2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
}