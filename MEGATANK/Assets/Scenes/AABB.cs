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
    MyVector2 MinExtent;
    MyVector2 MaxExtent;

    //Axis-Aligned Bounding Box Constructor
    public AABB(MyVector2 Min, MyVector2 Max)
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


    //Collision detection/Intersection
    public static bool Intersects(AABB Box1, AABB Box2)
    {
        return !(Box1.Left > Box1.Right
            || Box2.Right < Box1.Left
            || Box2.Top < Box1.Bottom
            || Box2.Bottom > Box1.Top);
    }

    // Find if two lines intersect across all axis
    public static bool LineIntersection(AABB Box, MyVector2 StartPoint, MyVector2 EndPoint, out MyVector2 IntersectionPoint)
    {
     //Define our initial lowest and highest
     float Lowest = 0.0f;
     float Highest = 1.0f;

    //Default Value for intersction point is needed:
    IntersectionPoint = new MyVector2(0,0);

        //Intersection check opn every axis - Using the intersectingAxis function

        //                                   Right
        if (!IntersectingAxis(new MyVector2(1, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                   Left
        if (!IntersectingAxis(new MyVector2(-1, 0), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                    Up
        if (!IntersectingAxis(new MyVector2(0, 1), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        //                                   Down
        if (!IntersectingAxis(new MyVector2(0, -1), Box, StartPoint, EndPoint, ref Lowest, ref Highest))
            return false;
        

        //Calculate the intersection point through interpolation.
        IntersectionPoint = MyVector2.VecLerp(StartPoint, EndPoint, Lowest);

        return true;
    }

    // The origin of the intersection
    public static bool IntersectingAxis(MyVector2 Axis, AABB Box, MyVector2 StartPoint, MyVector2 Endpoint, ref float Lowest, ref float Highest)
    {
        //Calculate Minimum and Maximum based on the current axis
        float Minimum = 0.0f, Maximum = 0.0f;
        if (Axis == new MyVector2(1, 0)) //Right
        {
            Minimum = (Box.Left - StartPoint.x) / (Endpoint.x - StartPoint.x);
            Maximum = (Box.Right - StartPoint.x) / (Endpoint.x - StartPoint.x);
        }
        else if (Axis == new MyVector2(-1, 0)) //Left
        {
            Minimum = (Box.Left - StartPoint.x) / (Endpoint.x - StartPoint.x);
            Maximum = (Box.Right - StartPoint.x) / (Endpoint.x - StartPoint.x);
        }
        else if (Axis == new MyVector2(0, 1)) //Up
        {
            Minimum = (Box.Bottom - StartPoint.y) / (Endpoint.y - StartPoint.y);
            Maximum = (Box.Top - StartPoint.y) / (Endpoint.y - StartPoint.y);
        }
        else if (Axis == new MyVector2(0, -1)) //Down
        {
            Minimum = (Box.Bottom - StartPoint.y) / (Endpoint.y - StartPoint.y);
            Maximum = (Box.Top - StartPoint.y) / (Endpoint.y - StartPoint.y);
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

