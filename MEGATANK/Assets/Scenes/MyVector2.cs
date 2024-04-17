using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyVector2
{
    public float x;
    public float y;
    public float z;

    public Vector2[] Vertices { get; }

    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    // Properties or getters for x, y, and z

    // Define method for left direction
    public static MyVector2 Left()
    {
        return new MyVector2(-1, 0);
    }

    // Define method for right direction
    public static MyVector2 Right()
    {
        return new MyVector2(1, 0);
    }

    // Define method for top direction
    public static MyVector2 Top()
    {
        return new MyVector2(0, 1);
    }


    

    // Define method for up direction
    public static MyVector2 Up()
    {
        return new MyVector2(0, 1);
    }

    // Define method for down direction
    public static MyVector2 Down()
    {
        return new MyVector2(0, -1);
    }

    public MyVector2(UnityEngine.Vector2 UnityVector2) //Converts Unity Vector into my own MyVector2.
    {
        this.x = UnityVector2.x;
        this.y = UnityVector2.y;
    }
    public UnityEngine.Vector2 ToUnityVector2()
    {
        return new UnityEngine.Vector2(x, y);
    }

    public MyVector2(UnityEngine.Quaternion UnityVector2)
    {
        this.x = UnityVector2.x;
        this.y = UnityVector2.y;
        this.z = UnityVector2.z;
    }


    public static MyVector2 AddVectors(MyVector2 v1, MyVector2 v2) //A "static" function is defined on an object, but it doesn't change properties of the object.
    {
        MyVector2 rv = new MyVector2(0, 0); //Adds vectors together and Returns the value

        rv.x = v1.x + v2.x;
        rv.y = v1.y + v2.y;

        return rv;
    }

    public static MyVector2 SubtractVectors(MyVector2 v1, MyVector2 v2) //Subtracts vectors together and Returns the value
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = v1.x - v2.x;
        rv.y = v1.y - v2.y;

        return rv;
    }

    public float Length() // Returns the length/magnitude of the vector
    {
        float rv = 0f;

        // Calculate the magnitude of the vector using the Pythagorean theorem
        rv = Mathf.Sqrt(x * x + y * y);
        //sqaure roots the vector to turns it into 1 whole number 

        return rv;
    }

    public Vector3 ToUnityVector() //Converts into a UnityEngine.Vector3
    {
        Vector3 UnityVector = new Vector3(x, y);

        // Creates a Unity Vector3 using the x, y, and z components of the Myvector3

        return UnityVector;
    }

    public static MyVector2 operator +(MyVector2 lhs, MyVector2 rhs)
    // Operator overloading for addition, allowing the use of the '+' operator for vector addition
    {
        // Call the AddVectors method for vector addition
        return AddVectors(lhs, rhs);
    }

    public static MyVector2 operator -(MyVector2 lhs, MyVector2 rhs)
    // Operator overloading for subtract, allowing the use of the '-' operator for vector addition
    {
        // Call the AddVectors method for vector addition
        return SubtractVectors(lhs, rhs);
    }

    //Workshop 2

    public float LengthSquared()
    {
        float rv;

        rv = x * x + y * y;

        return rv;
    }

    //Vector Scale 

    public static MyVector2 VectorScalar(MyVector2 Vec1, float Scalar)
    {
        MyVector2 rv = new MyVector2(0, 0);

        //Scale Vectors
        rv.x = Vec1.x * Scalar;
        rv.y = Vec1.y * Scalar;

        return rv;
    }

    //normalize changes the magnitude/length of a vector without changing the direction
    public static MyVector2 VectorNormalizer(MyVector2 Vec1, float Divisor)
    {
        MyVector2 rv = new MyVector2(0, 0);

        //Normalize Vectors
        rv.x = Vec1.x / Divisor;
        rv.y = Vec1.y / Divisor;

        return rv;
    }

    public static MyVector2 operator *(MyVector2 lhs, float rhs)
    {
        return VectorScalar(lhs, rhs);
    }

    public static MyVector2 operator /(MyVector2 lhs, float rhs)
    {
        return VectorNormalizer(lhs, rhs);
    }

    //Define a Normalize class
    public MyVector2 Normalize()
    {
        MyVector2 rv = new MyVector2(0, 0);
        rv.x = x;
        rv.y = y;

        rv = rv / rv.Length();

        return rv;
    }

    public static float DotProduct(MyVector2 v1, MyVector2 v2, bool ShouldNormalize = true)
    //if both vectors are normalized it will return the angle between them both.
    {
        MyVector2 rv = new MyVector2(0, 0);

        MyVector2 A = new MyVector2(v1.x, v1.y);
        MyVector2 B = new MyVector2(v2.x, v2.y);

        //normalize it if necessary
        if (ShouldNormalize)
        {
            A.Normalize();
            B.Normalize();
        }

        rv.x = (v1.x * v2.x);
        rv.y = (v1.y * v2.y);

        return rv.x + rv.y;
    }

    public static MyVector2 RadiansToVector(float RadiansAngles)
    {
        MyVector2 rv = new MyVector2(Mathf.Cos(RadiansAngles), Mathf.Sin(RadiansAngles));


        return rv;
    }
    public static MyVector2 EulerAnglesToDirection(MyVector2 EulerAngles)
    {
        MyVector2 rv = new MyVector2(0,0);


        rv.y = Mathf.Sin(-EulerAngles.x);
        rv.x = Mathf.Cos(-EulerAngles.x) * Mathf.Sin(EulerAngles.y);
        //Values stored in EulerAngles must be in Radians
        return rv;
    }



    public static MyVector2 CrossProduct(MyVector2 v1, MyVector2 v2)
    {
        //produces a vector perpendicular to both vectors
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = v1.y * v2.y;
        rv.y = v2.x - v1.x;

        return rv;
    }

    public static MyVector2 VecLerp(MyVector2 A, MyVector2 B, float t)
    { //A = Start    B = End    T = Fractional value 
        MyVector2 C = A * (1.0f - t) + B * t;

        return C; //C = The interpolated Value
    }
    public static MyVector2 RotateVertexAroundAxis(float Angle, MyVector2 Axis, MyVector2 Vertex)
    {
        //THe rodrigues rotation formula
        //Angle has to be in radians

        MyVector2 rv = (Vertex * Mathf.Cos(Angle)) +
            //(DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)))  +
            CrossProduct(Axis, Vertex) * Mathf.Sin(Angle);

        return rv;
    }
}