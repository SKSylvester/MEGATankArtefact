using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyVector3
{
    public float x;
    public float y;
    public float z;

    public Vector3[] Vertices { get; }

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Properties or getters for x, y, and z

    // Define method for left direction
    public static MyVector3 Left()
    {
        return new MyVector3(-1, 0, 0);
    }

    // Define method for right direction
    public static MyVector3 Right()
    {
        return new MyVector3(1, 0, 0);
    }

    // Define method for top direction
    public static MyVector3 Top()
    {
        return new MyVector3(0, 1, 0);
    }

    // Define method for front direction
    public static MyVector3 Front()
    {
        return new MyVector3(0, 0, 1);
    }

    // Define method for back direction
    public static MyVector3 Back()
    {
        return new MyVector3(0, 0, -1);
    }

    // Define method for up direction
    public static MyVector3 Up()
    {
        return new MyVector3(0, 1, 0);
    }

    // Define method for down direction
    public static MyVector3 Down()
    {
        return new MyVector3(0, -1, 0);
    }

    public MyVector3(UnityEngine.Vector3 UnityVector3) //Converts Unity Vector into my own MyVector3.
    {
        this.x = UnityVector3.x;
        this.y = UnityVector3.y;
        this.z = UnityVector3.z;
    }

    public MyVector3(UnityEngine.Quaternion UnityVector3)
    {
        this.x = UnityVector3.x;
        this.y = UnityVector3.y;
        this.z = UnityVector3.z;
    }


    public static MyVector3 AddVectors(MyVector3 v1, MyVector3 v2) //A "static" function is defined on an object, but it doesn't change properties of the object.
    {
        MyVector3 rv = new MyVector3(0, 0, 0); //Adds vectors together and Returns the value

        rv.x = v1.x + v2.x;
        rv.y = v1.y + v2.y;
        rv.z = v1.z + v2.z;

        return rv;
    }

    public static MyVector3 SubtractVectors(MyVector3 v1, MyVector3 v2) //Subtracts vectors together and Returns the value
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v1.x - v2.x;
        rv.y = v1.y - v2.y;
        rv.z = v1.z - v2.z;

        return rv;
    }

    public float Length() // Returns the length/magnitude of the vector
    {
        float rv = 0f;

        // Calculate the magnitude of the vector using the Pythagorean theorem
        rv = Mathf.Sqrt(x * x) + (y * y) + (z * z);
        //sqaure roots the vector to turns it into 1 whole number 

        return rv;
    }

    public Vector3 ToUnityVector() //Converts into a UnityEngine.Vector3
    {
        Vector3 UnityVector = new Vector3(x, y, z);

        // Creates a Unity Vector3 using the x, y, and z components of the Myvector3

        return UnityVector;
    }

    public static MyVector3 operator +(MyVector3 lhs, MyVector3 rhs)
    // Operator overloading for addition, allowing the use of the '+' operator for vector addition
    {
        // Call the AddVectors method for vector addition
        return AddVectors(lhs, rhs);
    }

    public static MyVector3 operator -(MyVector3 lhs, MyVector3 rhs)
    // Operator overloading for subtract, allowing the use of the '-' operator for vector addition
    {
        // Call the AddVectors method for vector addition
        return SubtractVectors(lhs, rhs);
    }

    //Workshop 2

    public float LengthSquared()
    {
        float rv;

        rv = x * x + y * y + z * z;

        return rv;
    }

    //Vector Scale 

    public static MyVector3 VectorScalar(MyVector3 Vec1, float Scalar)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        //Scale Vectors
        rv.x = Vec1.x * Scalar;
        rv.y = Vec1.y * Scalar;
        rv.z = Vec1.z * Scalar;

        return rv;
    }

    //normalize changes the magnitude/length of a vector without changing the direction
    public static MyVector3 VectorNormalizer(MyVector3 Vec1, float Divisor)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        //Normalize Vectors
        rv.x = Vec1.x / Divisor;
        rv.y = Vec1.y / Divisor;
        rv.z = Vec1.z / Divisor;

        return rv;
    }

    public static MyVector3 operator *(MyVector3 lhs, float rhs)
    {
        return VectorScalar(lhs, rhs);
    }

    public static MyVector3 operator /(MyVector3 lhs, float rhs)
    {
        return VectorNormalizer(lhs, rhs);
    }

    //Define a Normalize class
    public MyVector3 Normalize()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);
        rv.x = x;
        rv.y = y;
        rv.z = z;

        rv = rv / rv.Length();

        return rv;
    }

    public static float DotProduct(MyVector3 v1, MyVector3 v2, bool ShouldNormalize = true)
    //if both vectors are normalized it will return the angle between them both.
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        MyVector3 A = new MyVector3(v1.x, v1.y, v1.z);
        MyVector3 B = new MyVector3(v2.x, v2.y, v1.z);

        //normalize it if necessary
        if (ShouldNormalize)
        {
            A.Normalize();
            B.Normalize();
        }

        rv.x = (v1.x * v2.x);
        rv.y = (v1.y * v2.y);
        rv.z = (v1.z * v2.z);

        return rv.x + rv.y + rv.z;
    }

    public static MyVector3 RadiansToVector(float RadiansAngles)
    {
        MyVector3 rv = new MyVector3 (Mathf.Cos(RadiansAngles), Mathf.Sin(RadiansAngles), Mathf.Cos(RadiansAngles));


        return rv;
    }
    public static MyVector3 EulerAnglesToDirection(MyVector3 EulerAngles)
    {
        MyVector3 rv = new MyVector3(0,0,0);


        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(-EulerAngles.x);
        rv.y = Mathf.Sin(-EulerAngles.x);
        rv.x = Mathf.Cos(-EulerAngles.x) * Mathf.Sin(EulerAngles.y);
        //Values stored in EulerAngles must be in Radians
        return rv;
    }

    public static MyVector3 RollAngle(MyVector3 RollAngles)
    {
        MyVector3 rv = new MyVector3(Mathf.Cos(RollAngles.z), Mathf.Sin(RollAngles.z), 0);

        rv.x = Mathf.Cos(RollAngles.z);
        rv.y = Mathf.Sin(RollAngles.z);
        rv.z = 0;

        return rv;
    }

    public static MyVector3 CrossProduct(MyVector3 v1, MyVector3 v2)
    {
        //produces a vector perpendicular to both vectors
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v1.y * v2.z - v1.z * v2.y;
        rv.y = v1.z * v2.x - v1.x * v2.z;
        rv.z = v1.x * v2.y - v1.y * v2.x;

        return rv;
    }

    public static MyVector3 VecLerp(MyVector3 A, MyVector3 B, float t)
    { //A = Start    B = End    T = Fractional value 
        MyVector3 C = A * (1.0f - t) + B * t;

        return C; //C = The interpolated Value
    }
    public static MyVector3 RotateVertexAroundAxis(float Angle, MyVector3 Axis, MyVector3 Vertex)
    {
        //THe rodrigues rotation formula
        //Angle has to be in radians
        
        MyVector3 rv = (Vertex * Mathf.Cos(Angle)) +
            //(DotProduct(Vertex, Axis) * Axis * (1 - Mathf.Cos(Angle)))  +
            CrossProduct(Axis, Vertex) * Mathf.Sin(Angle);

        return rv;
    }
}