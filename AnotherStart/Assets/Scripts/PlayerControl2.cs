using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour {

    public ArmRotation ArmRotation;
    public WeaponShoot WeaponShoot;
    public bool MouseEnabled = false;

    // Update is called once per frame
    void Update () {
        if (MouseEnabled)
        {
            ArmRotation.RotateArmMouse(Input.mousePosition.x, Input.mousePosition.y);

            if (Input.GetButton("Fire1"))
                WeaponShoot.ShootMouse(Input.mousePosition.x, Input.mousePosition.y);
        }
        else
        {
            var vertical = SimpleInput.GetAxis("Vertical2");
            var horizontal = SimpleInput.GetAxis("Horizontal2");

            if ((vertical == 0) && (horizontal == 0)) return;

            var direction = new Vector2(horizontal, vertical);
            ArmRotation.RotateArmDirection(direction);
            WeaponShoot.ShootDirection(direction);

            //var verticalButton = SimpleInput.GetButtonDown("Vertical2");
            //var horizontalButton = SimpleInput.GetButtonDown("Horizontal2");

            //if (verticalButton || horizontalButton)
            //{
                
            //}            
        }
    }
}
