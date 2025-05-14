using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    [SerializeField] private float m_mouseSensitivity = 5f;
    [SerializeField] private float m_maxSpeed = 5f;

    private Vector3 m_currentVelocity = Vector3.zero;
    private float rotY;
    private float rotX;


    private void Start()
    {
        _inputReader.EnablePlayerActions();
    }

    void Update()
    {
        if (_inputReader.rotateIsBeingPressed) {
            Move(_inputReader.move, _inputReader.mouseDelta, 0, 0);
            SetCursorStatus(CursorStatusTypes.Hidden);
        }
        else
        {
            SetCursorStatus(CursorStatusTypes.Normal);
        }
    }
    
    public void Move(Vector2 movementVector, Vector2 mousePosition2D, float moveUpDown, float inputSpeedSlow)
    {
        // Debug.Log(mousePosition2D);
        Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
        if (!screenRect.Contains(Input.mousePosition))
            return;
        var camepraPos = new Vector2(transform.eulerAngles.x, transform.eulerAngles.y);
        // Debug.Log(camepraPos);

        rotY = camepraPos.y;
        rotX = camepraPos.x;
        // Debug.Log($"pos x: {mousePosition2D.x}; pos y: {mousePosition2D.y}");

        rotY += mousePosition2D.x * m_mouseSensitivity;
        rotX -= mousePosition2D.y * m_mouseSensitivity;
        // Debug.Log($"rot x: {rotX}; rot y: {rotY}");

        transform.eulerAngles = new Vector3(rotX, rotY, 0);

        // Debug.Log($"x: {movementVector.x}; y: {movementVector.y}");

        var newPos1 = transform.position + transform.forward * (movementVector.y * 100) + transform.right * movementVector.x * 100 + transform.up * (moveUpDown * 100);

        float speedModifier = 1;
        // if (inputSpeedSlow != 0)
        // {
        //     speedModifier = (float)(inputSpeedSlow > 0 ? m_shiftspeedModifier : m_ctrlspeedModifier);
        // }

        var newPos = Vector3.SmoothDamp(transform.localPosition, newPos1, ref m_currentVelocity, 0.5f, m_maxSpeed * speedModifier);
        transform.localPosition = newPos;
        // m_test.localPosition = newPos;

    }
    
    
    public void SetCursorStatus(CursorStatusTypes type) {
        switch (type) {
            case CursorStatusTypes.Normal: {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                    
                break;
            }
            case CursorStatusTypes.Hidden: {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            }
        }
    }
    
    
    public enum CursorStatusTypes {
        Normal,
        Hidden
    }
}