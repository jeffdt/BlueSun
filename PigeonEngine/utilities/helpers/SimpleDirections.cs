﻿using System;

namespace pigeon.utilities.helpers {
    public enum SimpleDirections {
        Up = 0, Right = 1, Down = 2, Left =3
    }

    public static class SimpleDirectionsExtensions {
        public static int ToInt(this SimpleDirections sd) {
            switch (sd) {
                case SimpleDirections.Up:
                    return 0;
                case SimpleDirections.Right:
                    return 1;
                case SimpleDirections.Down:
                    return 2;
                case SimpleDirections.Left:
                    return 3;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sd));
            }
        }

        public static SimpleDirections Opposite(this SimpleDirections sd) {
            switch (sd) {
                case SimpleDirections.Up:
                    return SimpleDirections.Down;
                case SimpleDirections.Down:
                    return SimpleDirections.Up;
                case SimpleDirections.Left:
                    return SimpleDirections.Right;
                case SimpleDirections.Right:
                    return SimpleDirections.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sd));
            }
        }
    }
}
