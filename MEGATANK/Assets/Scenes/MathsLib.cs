using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsLib
{
    public static float VectorToRadians(MyVector2 v)
    {
        float rv = 0.0f;

        rv = Mathf.Atan(v.y / v.x);

        return rv;
    }
    public static MyVector2 RadiansToVector(float angle)
    {
        MyVector2 rv = new MyVector2(Mathf.Cos(angle), Mathf.Sin(angle));

        return rv;
    }
}