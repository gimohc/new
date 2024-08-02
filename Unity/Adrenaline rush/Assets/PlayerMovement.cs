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
    [SerializeField] InputAction jump;

    [SerializeField] float movementFactor = 20f;
    [SerializeField] float sensitivityFactor = 1f;
    [SerializeField] float jumpingFactor = 1f;
    [SerializeField] float jumpHeight = 5f;

    void Start()
    {
        move.Enable();
        faceDirection.Enable();
        jump.Enable();

    }

    // Update is called once per frame

    void Update()
    {
        HandleDirection();
        HandleMovement();
        StartCoroutine(Jump());
    }
    // ws = y (front and back is the move.y) 
    // ad = x (left and right is the move.x)
    void HandleMovement()
    {
        Vector2 movementInput = move.ReadValue<Vector2>() * Time.deltaTime * movementFactor;
        float rotationY = transform.eulerAngles.y * Mathf.Deg2Rad;

        transform.Translate(Mathf.Cos(rotationY) * movementInput.x, 0, -Mathf.Sin(rotationY) * movementInput.x, Space.World);
        transform.Translate(Mathf.Sin(rotationY) * movementInput.y, 0, Mathf.Cos(rotationY) * movementInput.y, Space.World);
        //transform.position += new Vector3(Mathf.Cos(rotationY) * movementInput.y, 0, Mathf.Sin(rotationY) * movementInput.y); // front backwards movement 

    }
    void HandleDirection()
    {
        Vector2 directionInput = faceDirection.ReadValue<Vector2>() * Time.deltaTime * sensitivityFactor;

        transform.Rotate(-directionInput.y, directionInput.x, 0, Space.Self);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);

    }
    private IEnumerator Jump()
    {
        float jumpInput = jump.ReadValue<float>() * Time.deltaTime * jumpingFactor;
        if (jumpInput > 0)
        {
            float jumpPercent = 0f;
            Vector3 startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 finishPosition = new Vector3(startPosition.x, startPosition.y + jumpHeight, startPosition.z);
            Debug.Log(startPosition);
            while (jumpPercent < 1f)
            {
                jumpPercent += jumpInput * Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, finishPosition, jumpPercent);
            }
            Debug.Log(startPosition);
            yield return new WaitForEndOfFrame();

            while (jumpPercent > 0f)
            {
                jumpPercent -= jumpInput * Time.deltaTime;
                transform.position = Vector3.Lerp(finishPosition, startPosition, jumpPercent);
            }

            yield return new WaitForSeconds(0.5f);

        }
    }

}
