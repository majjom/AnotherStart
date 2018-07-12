using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour {


    public float Damage = 10;
    public LayerMask whatToHit;
    public int ShotsPerSecond = 10;

    public Transform BulletTrailPrefab;
    public Transform firePoint;
    

    float timeForNextShot = 0;
    

    // Use this for initialization
    void Start () {
		
	}

    public void ShootMouse(float screenX, float screenY)
    {
        Vector2 mousePositiononScreen = new Vector3(screenX, screenY, 0);
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(mousePositiononScreen).x, Camera.main.ScreenToWorldPoint(mousePositiononScreen).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        ShootDirection(mousePosition - firePointPosition);
    }

    public void ShootDirection(Vector2 direction)
    {
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, direction, 100, whatToHit);
        if (Time.time >= timeForNextShot)
        {
            Effect();
            timeForNextShot = Time.time + (1 / (float)ShotsPerSecond);
        }
        //Debug.DrawLine(firePointPosition, direction * 100, Color.cyan);
        if (hit.collider != null)
        {
            //Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
    }

    void Effect()
    {
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
