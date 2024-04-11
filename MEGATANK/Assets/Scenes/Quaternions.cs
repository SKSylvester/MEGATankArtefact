using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Quat
{
    public float w, x, y, z;
    public MyVector2 v;
    public Quat()
    {
        w = 0.0f;
        v = new MyVector2(0, 0);
    }

    public Quat(float Angle, MyVector2 Axis)
    {

        float halfAngle = Angle / 2;
        w = Mathf.Cos(halfAngle);
        x = Axis.x * Mathf.Sin(halfAngle);
        y = Axis.y * Mathf.Sin(halfAngle);
        z = Axis.z * Mathf.Sin(halfAngle);

        return;
    }

    public static Quat Identity()
    {
         return new Quat(1.0f, new MyVector2(0.0f, 0.0f));  
    }

    public Quat(MyVector2 Position)
    {
        w = 0.0f;
        v = new MyVector2(Position.x, Position.y);
    }

    public static Quat operator *(Quat lhs, Quat rhs)
    {
        Quat rv = new Quat();

        //Scaler Formula
        rv.w = lhs.w * rhs.w - (lhs.x * rhs.x + lhs.y * rhs.y);

        //Vector Forumla
        MyVector2 lhsVector = new MyVector2(lhs.x, lhs.y); //Get left hand side Quaternions
        MyVector2 rhsVector = new MyVector2(rhs.x, rhs.y); //Get Right hand side Quaternions
        MyVector2 VectorCross = MyVector2.CrossProduct(lhsVector, rhsVector); //Calculate the cross product between the lhs and rhs vectors

        // Adds the Cross product and scales quaternions on each axis.
        rv.x = lhs.w * rhs.x + lhs.x * rhs.w + VectorCross.x;
        rv.y = lhs.w * rhs.y + lhs.y * rhs.w + VectorCross.y;
        rv.z = lhs.w * rhs.z + lhs.z * rhs.w + VectorCross.z;


        return rv;
    }
    // Getter and setter for axis

    public MyVector2 Axis
    {
        get
        {
            return new MyVector2(x, y);
        }
        set
        {
            x = value.x;
            y = value.y;
        }
    }

    // Set axis method
    public void SetAxis(MyVector2 axis)
    {
        x = axis.x;
        y = axis.y;
        z = axis.z;
    }

    // Get axis method
    public MyVector2 GetAxis()
    {
        return new MyVector2(x, y);
    }


    // Inverse method
    public Quat Inverse()
    {
        Quat rv = new Quat();
        rv.w = w;

        // Set the axis to the negative of the current axis
        rv.SetAxis(new MyVector2(0, 0) - GetAxis()); //- Get axis from nothing that way it becomes a negative
        return rv;
    }
    public UnityEngine.Vector4 GetAxisAngle()
    {
        UnityEngine.Vector4 rv = new UnityEngine.Vector4();

        //Inverse cosine to get half angle back
        float halfAngle = Mathf.Acos(w);
        rv.w = halfAngle * 2;

        rv.x = x / Mathf.Sin(halfAngle);
        rv.y = y / Mathf.Sin(halfAngle);
        rv.z = z / Mathf.Sin(halfAngle);

        return rv;
    }

    public static Quat SLERP(Quat q, Quat r, float t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);

        Quat d = r * q.Inverse();
        UnityEngine.Vector4 AxisAngle = d.GetAxisAngle();
        Quat dT = new Quat(AxisAngle.w * t, new MyVector2(AxisAngle.x, AxisAngle.y));

        return dT * q;
    }
}