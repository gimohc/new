using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction move;
    [SerializeField] InputAction faceDirection;
    [SerializeField] float movementFactor = 20f;
    [SerializeField] float sensitivityFactor = 1f;

    void Start()
    {
        move.Enable();
        faceDirection.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleDirection();
        float z = transform.rotation.z;
        z = 0;
    }
    void HandleMovement()
    {
        Vector2 movementInput = move.ReadValue<Vector2>() * Time.deltaTime * movementFactor;


        transform.Translate(movementInput.x, 0, movementInput.y);
        //transform.localPosition += new Vector3(movementInput.x, 0, movementInput.y);

    }
    void HandleDirection()
    {
        Vector2 directionInput = faceDirection.ReadValue<Vector2>() * Time.deltaTime * sensitivityFactor;

        transform.localRotation *= Quaternion.Euler(-directionInput.y, directionInput.x, 0);//, Space.Self);
        //Vector3 rotation = new Vector3(-directionInput.y, directionInput.x, 0);
        //Debug.Log(rotation);
        //transform.rotation *= Quaternion.Euler(rotation);



    }
}
