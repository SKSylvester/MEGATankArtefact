using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyMatrix4x4
{
    public float[,] values; //Mulidiemensional array of floats that store the matrix vlaue.

    //Matrix Columns
    public MyMatrix4x4(Vector4 Column1, Vector4 Column2, Vector4 Column3, Vector4 Column4)
    {
        values = new float[4, 4];

        // Column1
        values[0, 0] = Column1.x;
        values[1, 0] = Column1.y;
        values[2, 0] = Column1.z;
        values[3, 0] = Column1.w;

        // Column2 
        values[0, 1] = Column2.x;
        values[1, 1] = Column2.y;
        values[2, 1] = Column2.z;
        values[3, 1] = Column2.w;

        //column3 
        values[0, 2] = Column3.x;
        values[1, 2] = Column3.y;
        values[2, 2] = Column3.z;
        values[3, 2] = Column3.w;

        //column4
        values[0, 3] = Column4.x;
        values[1, 3] = Column4.y;
        values[2, 3] = Column4.z;
        values[3, 3] = Column4.w;
    }
    public MyMatrix4x4(MyVector2 Column1, MyVector2 Column2, MyVector2 Column3, MyVector2 Column4)
    {
        values = new float[4, 4];

        // If the column changes its the right hand side - If the row changes left hand side

        // Column1
        values[0, 0] = Column1.x;
        values[1, 0] = Column1.y;
        values[2, 0] = 0;
        values[3, 0] = 0;

        // Column2 
        values[0, 1] = Column2.x;
        values[1, 1] = Column2.y;
        values[2, 1] = 0;
        values[3, 1] = 0;

        //column3 
        values[0, 2] = Column3.x;
        values[1, 2] = Column3.y;
        values[2, 2] = 0;
        values[3, 2] = 0;

        //column4
        values[0, 3] = Column4.x;
        values[1, 3] = Column4.y;
        values[2, 3] = 0;
        values[3, 3] = 0;
    }


    public static MyMatrix4x4 Identity
    {
        get
        {
            return new MyMatrix4x4(
        new Vector4(1, 0, 0, 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 1));
        }
    }


    public static Vector4 operator *(MyMatrix4x4 lhs, Vector4 rhs) //Matrix multiplication 
    {
        Vector4 rv = new Vector4();

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * rhs.w;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * rhs.w;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * rhs.w;
        rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * rhs.w;

        return rv;
    }

    // Matrix and Vector2 operator for 2D transformation
    public static MyVector2 operator *(MyMatrix4x4 lhs, MyVector2 rhs)
    {
        MyVector2 rv = new MyVector2(0,0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 3];
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 3];

        return rv;
    }

    public static MyMatrix4x4 operator *(MyMatrix4x4 lhs, MyMatrix4x4 rhs)
    {
        MyMatrix4x4 rv = Identity;

        rv.values[0, 0] = lhs.values[0, 0] * rhs.values[0, 0] + lhs.values[0, 1] * rhs.values[1, 0] + lhs.values[0, 2] * rhs.values[2, 0] + lhs.values[0, 3] * rhs.values[3, 0];
        rv.values[1, 0] = lhs.values[1, 0] * rhs.values[0, 0] + lhs.values[1, 1] * rhs.values[1, 0] + lhs.values[1, 2] * rhs.values[2, 0] + lhs.values[1, 3] * rhs.values[3, 0];
        rv.values[2, 0] = lhs.values[2, 0] * rhs.values[0, 0] + lhs.values[2, 1] * rhs.values[1, 0] + lhs.values[2, 2] * rhs.values[2, 0] + lhs.values[2, 3] * rhs.values[3, 0];
        rv.values[3, 0] = lhs.values[3, 0] * rhs.values[0, 0] + lhs.values[3, 1] * rhs.values[1, 0] + lhs.values[3, 2] * rhs.values[2, 0] + lhs.values[3, 3] * rhs.values[3, 0];

        rv.values[0, 1] = lhs.values[0, 0] * rhs.values[0, 1] + lhs.values[0, 1] * rhs.values[1, 1] + lhs.values[0, 2] * rhs.values[2, 1] + lhs.values[0, 3] * rhs.values[3, 1];
        rv.values[1, 1] = lhs.values[1, 0] * rhs.values[0, 1] + lhs.values[1, 1] * rhs.values[1, 1] + lhs.values[1, 2] * rhs.values[2, 1] + lhs.values[1, 3] * rhs.values[3, 1];
        rv.values[2, 1] = lhs.values[2, 0] * rhs.values[0, 1] + lhs.values[2, 1] * rhs.values[1, 1] + lhs.values[2, 2] * rhs.values[2, 1] + lhs.values[2, 3] * rhs.values[3, 1];
        rv.values[3, 1] = lhs.values[3, 0] * rhs.values[0, 1] + lhs.values[3, 1] * rhs.values[1, 1] + lhs.values[3, 2] * rhs.values[2, 1] + lhs.values[3, 3] * rhs.values[3, 1];

        rv.values[0, 2] = lhs.values[0, 0] * rhs.values[0, 2] + lhs.values[0, 1] * rhs.values[1, 2] + lhs.values[0, 2] * rhs.values[2, 2] + lhs.values[0, 3] * rhs.values[3, 2];
        rv.values[1, 2] = lhs.values[1, 0] * rhs.values[0, 2] + lhs.values[1, 1] * rhs.values[1, 2] + lhs.values[1, 2] * rhs.values[2, 2] + lhs.values[1, 3] * rhs.values[3, 2];
        rv.values[2, 2] = lhs.values[2, 0] * rhs.values[0, 2] + lhs.values[2, 1] * rhs.values[1, 2] + lhs.values[2, 2] * rhs.values[2, 2] + lhs.values[2, 3] * rhs.values[3, 2];
        rv.values[3, 2] = lhs.values[3, 0] * rhs.values[0, 2] + lhs.values[3, 1] * rhs.values[1, 2] + lhs.values[3, 2] * rhs.values[2, 2] + lhs.values[3, 3] * rhs.values[3, 2];

        rv.values[0, 3] = lhs.values[0, 0] * rhs.values[0, 3] + lhs.values[0, 1] * rhs.values[1, 3] + lhs.values[0, 2] * rhs.values[2, 3] + lhs.values[0, 3] * rhs.values[3, 3];
        rv.values[1, 3] = lhs.values[1, 0] * rhs.values[0, 3] + lhs.values[1, 1] * rhs.values[1, 3] + lhs.values[1, 2] * rhs.values[2, 3] + lhs.values[1, 3] * rhs.values[3, 3];
        rv.values[2, 3] = lhs.values[2, 0] * rhs.values[0, 3] + lhs.values[2, 1] * rhs.values[1, 3] + lhs.values[2, 2] * rhs.values[2, 3] + lhs.values[2, 3] * rhs.values[3, 3];
        rv.values[3, 3] = lhs.values[3, 0] * rhs.values[0, 3] + lhs.values[3, 1] * rhs.values[1, 3] + lhs.values[3, 2] * rhs.values[2, 3] + lhs.values[3, 3] * rhs.values[3, 3];

        return rv;

    }

    public MyMatrix4x4 TranslationInverse()
    {
        MyMatrix4x4 rv = Identity;

        rv.values[0, 3] =  -values[0, 3];
        rv.values[1, 3] =  -values[1, 3];
        rv.values[2, 3] =  -values[2, 3];

        return rv;
    }

    //swaps the elements across the diagonal
    public MyMatrix4x4 RotationInverse()
    {
        MyMatrix4x4 rv = Identity;

        rv.values[0, 0] = values[0, 0];
        rv.values[1, 0] = values[0, 1];
        rv.values[2, 0] = values[0, 2];
        rv.values[0, 1] = values[1, 0];
        rv.values[1, 1] = values[1, 1];
        rv.values[2, 1] = values[1, 2];
        rv.values[0, 2] = values[2, 0];
        rv.values[1, 2] = values[2, 1];
        rv.values[2, 2] = values[2, 2];

        return rv;
    }


    public MyMatrix4x4 ScaleInverse()
    {
        MyMatrix4x4 rv = Identity;

        rv.values[0, 0] = 1.0f / values[0, 0];
        rv.values[1, 1] = 1.0f / values[1, 1];
        rv.values[2, 2] = 1.0f / values[2, 2];

        return rv;
    }

        
}
