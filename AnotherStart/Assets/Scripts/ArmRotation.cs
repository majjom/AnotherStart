using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ArmRotation : MonoBehaviour
{
    public int rotationOffset = 90;

    public void RotateArmMouse(float screenX, float screenY)
    {
        // subtracting the position of the player from the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(new Vector3(screenX, screenY,0)) - transform.position;
        difference.Normalize();     // normalizing the vector. Meaning that all the sum of the vector will be equal to 1
        RotateArmDirection(new Vector2(difference.x, difference.y));
    }

    public void RotateArmDirection(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;   // find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
