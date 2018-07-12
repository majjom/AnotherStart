using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour {

    [SerializeField] public float MaxHorizontalSpeed = 200; //X
    [SerializeField] public float MaxVerticalSpeed = 200; //Y
    

    private Rigidbody2D playerRigidbody2D;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    public void Fly(float horizontalRequestIntensity, float verticalRequestIntensity)
    {
        playerRigidbody2D.velocity = new Vector2(horizontalRequestIntensity * MaxHorizontalSpeed, verticalRequestIntensity * MaxVerticalSpeed);
    }
}
