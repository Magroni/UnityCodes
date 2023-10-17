using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController controller;
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private float verticalVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Movimentação do personagem
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = (cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput).normalized;
        moveDirection.y = 0;

        if (controller.isGrounded)
        {
            verticalVelocity = 0;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
            }
        }

        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Rotação do personagem com base na rotação da câmera
        float horizontalLook = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, horizontalLook, 0);

        // Rotação da câmera vertical
        float verticalLook = Input.GetAxis("Mouse Y") * rotationSpeed;
        Vector3 currentCameraRotation = cameraTransform.eulerAngles;
        currentCameraRotation.x -= verticalLook;
        cameraTransform.eulerAngles = currentCameraRotation;
        
        // Controle da câmera com controle de Xbox
        float rightStickX = Input.GetAxis("RightStickX");
        float rightStickY = Input.GetAxis("RightStickY");

        Vector3 cameraEulerAngles = cameraTransform.eulerAngles;
        cameraEulerAngles.x += rightStickY * rotationSpeed;
        cameraEulerAngles.y += rightStickX * rotationSpeed;
        cameraTransform.eulerAngles = cameraEulerAngles;
    }
}
