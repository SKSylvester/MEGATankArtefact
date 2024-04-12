using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    public Transform turret;
    public LayerMask targetLayer;
    public GameObject projectile;
    float angleRadians = 0f;
    float angleDegrees = 0f;
    float projectileSpeed = 10f;


    /**
     * The following code snippet was adapted using ChatGPT-3.5 (OpenAI, 2021).
     */

    void Update()
    {

        // Shoot projectile if fire button is pressed
        if (Input.GetButtonDown("Fire1")) 
        {
            ShootProjectile();
        }

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

        // Perform line trace to check for target
        RaycastHit2D hit = Physics2D.Raycast(turretPos.ToUnityVector(), direction.ToUnityVector(), Mathf.Infinity, targetLayer);

        // If the line trace hits a target, do something
        if (hit.collider != null)
        {
            Debug.DrawLine(turretPos.ToUnityVector(), hit.point, Color.red); // Draw a debug line to visualize the line trace
            
        }
        else
        {
            Debug.DrawLine(turretPos.ToUnityVector(), (turretPos + direction).ToUnityVector(), Color.green); // Draw a debug line to visualize the line trace
            // Handle case when the line trace doesn't hit any target
        }
    }

    public void ShootProjectile()
    {
        MyVector2 Bulletdirection = GetMousePosition() - new MyVector2(turret.position.x, turret.position.y);
        angleRadians = MathsLib.VectorToRadians(Bulletdirection);
        MyVector2 velocity = MyVector2.RadiansToVector(angleRadians) * projectileSpeed;

        GameObject SpawnProjectile = Instantiate(projectile, turret.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

        if (projectileRigidbody != null)
        {
            // Set the velocity of the projectile
            projectileRigidbody.velocity = velocity.ToUnityVector() * projectileSpeed;

        }

    }

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



