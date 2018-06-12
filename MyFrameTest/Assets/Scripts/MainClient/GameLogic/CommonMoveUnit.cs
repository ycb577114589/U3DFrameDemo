﻿using UnityEngine;


namespace MainClient
{
    public class CommonMoveUnit
    {
        public static Vector3 Move(Vector3 pos, Vector3 forward,float delta,float speed)
        {
            Vector3 distance = forward * speed * delta;
            return pos + distance;
        }
    }
}