using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
// Used to apply Inverse Matrices
public class AABB //Axis-Aligned Bounding Box 
{


    //3D bound boxes
    MyVector3 MinExtent;
    MyVector3 MaxExtent;

    //Axis-Aligned Bounding Box Constructor
    public AABB(MyVector3 Min, MyVector3 Max)
    {
        MinExtent = Min;
        MaxExtent = Max;
    }

    public float Top
    {
        get { return MinExtent.y; }
    }
    public float Bottom
    {
        get { return MaxExtent.y; }
    }
    
        public float Left
    {
        get { return MinExtent.x; }
    }
 
    public float Right
    {
        get { return MaxExtent.x; }
    }

    public float Front
    {
        get { return MaxExtent.z; }
    }

    public float Back
    {
        get { return MinExtent.z; }
    }

    //Collision detection/Intersection
    public static bool Intersects(AABB Box1, AABB Box2)
    {
        return !(Box1.Left > Box1.Right
            || Box2.Right < Box1.Left
            || Box2.Top < Box1.Bottom
            || Box2.Bottom > Box1.Top
            || Box2.Back > Box1.Front
            || Box2.Front < Box1.Back);
    }

    // Find if two lines intersect across all axis
    public static bool LineIntersection(AABB Box, MyVector3 StartPoint, MyVector3 EndPoint, out MyVector3 IntersectionPoint)
    {
     //Define our initial lowest and highest
     float Lowest = 0.0f;
     float Highest = 1.0f;

    //Default Value for intersction point is needed:
    IntersectionPoint = new MyVector3 (0,0,0);

        //Intersection check opn every axis - Using the intersectingAxis function

        //                                   Right
        if (!IntersectingAxis(new MyVector3(1, 0, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                   Left
        if (!IntersectingAxis(new MyVector3(-1, 0, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                    Up
        if (!IntersectingAxis(new MyVector3(0, 1, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                   Down
        if (!IntersectingAxis(new MyVector3(0, -1, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                   Foward
        if (!IntersectingAxis(new MyVector3(0, 0, 1), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                  Backward
        if (!IntersectingAxis(new MyVector3(0, 0, -1), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;

        //Calculate the intersection point through interpolation.
        IntersectionPoint = MyVector3.VecLerp(StartPoint, EndPoint, Lowest);

        return true;
    }

    // The origin of the intersection
    public static bool IntersectingAxis(MyVector3 Axis, AABB Box, MyVector3 StartPoint, MyVector3 Endpoint, ref float Lowest, ref float Highest)
    {
        //Calculate Minimum and Maximum based on the current axis
        float Minimum = 0.0f, Maximum = 0.0f;
        if (Axis == new MyVector3(1, 0, 0)) //Right
        {
            Minimum = (Box.Left - StartPoint.x) / (Endpoint.x - StartPoint.x);
            Maximum = (Box.Right - StartPoint.x) / (Endpoint.x - StartPoint.x);
        }
        else if (Axis == new MyVector3(-1, 0, 0)) //Left
        {
            Minimum = (Box.Left - StartPoint.x) / (Endpoint.x - StartPoint.x);
            Maximum = (Box.Right - StartPoint.x) / (Endpoint.x - StartPoint.x);
        }
        else if (Axis == new MyVector3(0, 1, 0)) //Up
        {
            Minimum = (Box.Bottom - StartPoint.y) / (Endpoint.y - StartPoint.y);
            Maximum = (Box.Top - StartPoint.y) / (Endpoint.y - StartPoint.y);
        }
        else if (Axis == new MyVector3(0, -1, 0)) //Down
        {
            Minimum = (Box.Bottom - StartPoint.y) / (Endpoint.y - StartPoint.y);
            Maximum = (Box.Top - StartPoint.y) / (Endpoint.y - StartPoint.y);
        }
        else if (Axis == new MyVector3(0, 0, 1)) //Forward
        {
            Minimum = (Box.Back - StartPoint.z) / (Endpoint.z - StartPoint.z);
            Maximum = (Box.Top - StartPoint.z) / (Endpoint.z - StartPoint.z);
        }
        else if (Axis == new MyVector3(0, 0, -1)) //Backward
        {
            Minimum = (Box.Back - StartPoint.z) / (Endpoint.z - StartPoint.z);
            Maximum = (Box.Top - StartPoint.z) / (Endpoint.z - StartPoint.z);
        }

        if (Maximum < Minimum)
        { 
            //Swapping values
            float temp = Maximum;
            Maximum = Minimum;
            Minimum = temp;
        }
        //Eliminate non-intersections early
        if (Maximum < Lowest)
            return false;

        if (Minimum > Highest) 
            return false;

        Lowest = Mathf.Max(Minimum, Lowest);
        Highest = Mathf.Min(Maximum, Highest);

        if (Lowest > Highest)
            return false;

        return true;
    }
            
}

