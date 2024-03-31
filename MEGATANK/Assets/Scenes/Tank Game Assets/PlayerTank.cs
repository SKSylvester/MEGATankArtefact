using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    public MyVector2 Rotation;
    public MyVector2 mouseRotation;
    public MyVector2 mousePosition;
    public MyVector2 lastmousePosition;
    public MyVector2 MouseDelta;
    public MyVector2 eulerRotation;
    public MyVector2 eulerAngle;
    public MyVector2 up = MyVector2.Up();




    // Start is called before the first frame update
    void Start()
    {
        lastmousePosition = new MyVector2(Input.mousePosition);
    }

    public MyVector2 CalculateEuler()
    {
        MyVector2 rv = new MyVector2(0, 0);

        Rotation = new MyVector2(transform.eulerAngles);

        rv = Rotation / 180f / Mathf.PI;

        return rv;
    }

    // Update is called once per frame
    void Update()
    {
        MouseDelta = new MyVector2(Input.mousePosition) - lastmousePosition;
        lastmousePosition = new MyVector2(Input.mousePosition);
        float MouseRotation = MathsLib.VectorToRadians(MouseDelta);

        Debug.Log(transform.eulerAngles);
        Debug.Log(MouseDelta);
        Debug.Log(eulerRotation);

        eulerRotation = CalculateEuler();

        eulerAngle = Rotation + MouseDelta;

        transform.eulerAngles = eulerAngle.ToUnityVector();


        MouseDelta = new MyVector2(Input.mousePosition) - lastmousePosition;
        lastmousePosition = new MyVector2(Input.mousePosition);

        mouseRotation = new MyVector2 (-MouseDelta.y, MouseDelta.x);

        transform.eulerAngles += mouseRotation.ToUnityVector();


        MyVector2 currentEulerAngles = eulerAngle;
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 0, 360f);
        currentEulerAngles.y = Mathf.Clamp(currentEulerAngles.y, -90f, 90f);
        transform.eulerAngles = currentEulerAngles.ToUnityVector();
    }
}
