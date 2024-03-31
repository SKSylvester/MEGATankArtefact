using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsLib
{
    public static float VectorToRadians(MyVector3 v)
    {
        float rv = 0.0f;

        rv = Mathf.Atan(v.y / v.x);

        return rv;
    }
    public static MyVector3 RadiansToVector(float angle)
    {
        MyVector3 rv = new MyVector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        return rv;
    }
}