using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{

    public Transform turret;
    public GameObject projectile;
    float angleRadians = 0f;
    float angleDegrees = 0f;
    public float launchVelocity = 10f;
    public float launchVelocityOverTime = 0f;


    void Update()
    {

            ShootProjectile();

        // Get mouse position
        MyVector2 mousePos = GetMousePosition();
        
        // Get turret position
        MyVector2 turretPos = new MyVector2(turret.position.x, turret.position.y);

        // Calculate direction from turret to mouse position
        MyVector2 direction = mousePos - turretPos;

        // Calculate angle 
         angleRadians = MathsLib.VectorToRadians(direction);

        // Convert radians to degrees for Quaternion.Euler
        angleDegrees = MathsLib.FloatRadiansToDegrees(angleRadians);

        // Rotate turret to face the mouse position
        turret.rotation = Quaternion.Euler(new Vector3(0, 0, angleDegrees));

        //MyMatrix4x4 Roll Use from workshop code

    }

    public void ShootProjectile()
    {
        //Get the direction of the bullet
        MyVector2 Bulletdirection = GetMousePosition() - new MyVector2(turret.position.x, turret.position.y);

        //Turn the direction into an angle
        angleRadians = MathsLib.VectorToRadians(Bulletdirection);
       
        
        if (Input.GetMouseButton(0))
        {
            Debug.Log("The left mouse button is being held down.");

             launchVelocityOverTime +=  5f * Time.deltaTime * 2;

        }
        if (Input.GetMouseButtonUp(0))
        {
            //Spawn and launch the projectile
            GameObject SpawnProjectile = Instantiate(projectile, turret.position, turret.rotation);
            Rigidbody2D projectileRigidbody = SpawnProjectile.GetComponent<Rigidbody2D>();

            Debug.Log("Bulletdirection" + Bulletdirection);

            Debug.Log("angleRadians" + angleRadians);


            // Check if the Rigidbody2D component is found
            if (projectileRigidbody != null)
            {

                // Calculate the velocity vector
                MyVector2 velocity = Bulletdirection.Normalize() * (launchVelocity + launchVelocityOverTime);

                // Apply force to the projectile
                projectileRigidbody.velocity = velocity.ToUnityVector2();

                launchVelocityOverTime = 0;

                Debug.Log("projectileRigidbody.velocity" + projectileRigidbody.velocity);
            }
            else
            {
                Debug.LogError("Projectile does not have a Rigidbody2D component.");
            }
        }

    }

    /**
* The following code snippet was adapted using ChatGPT-3.5 (OpenAI, 2021).
*/
    private MyVector2 GetMousePosition()
    {
        // Get mouse position
        Vector3 mousePos = Input.mousePosition;

        //convert the mouse position from screen coordinates to world coordinates along the y-axis.
        mousePos.z = Camera.main.transform.position.y - turret.position.y;

        //converts the  mouse position from screen coordinates to world coordinates
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Creates the new mouse posistion from a 3D vector to a 2D Vector
        return new MyVector2(worldMousePos.x, worldMousePos.y);

    }
    /**
* End of adaptation
*/
}



