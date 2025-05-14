using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    [SerializeField] private float m_mouseSensitivity = 5f;
    [SerializeField] private float m_maxSpeed = 5f;
    [SerializeField] private float m_shiftspeedModifier = 3f;

    [SerializeField] private GameObject UIRoot;
    [SerializeField] private GameObject Split;
    [SerializeField] private GameObject Main;
    
    private Vector3 m_currentVelocity = Vector3.zero;
    private float rotY;
    private float rotX;

    

    private void Start()
    {
        _inputReader.EnablePlayerActions();
        _inputReader.One += InputReaderOnOne;
        _inputReader.Two += InputReaderOnTwo;
    }


    private void InputReaderOnOne()
    {
        Split.SetActive(!Split.activeSelf);
        Main.SetActive(!Main.activeSelf);
    }
    
    private void InputReaderOnTwo()
    {
        UIRoot.SetActive(!UIRoot.activeSelf);
    }

    void Update()
    {
        if (_inputReader.rotateIsBeingPressed) {
            Move(_inputReader.move, _inputReader.mouseDelta, _inputReader.upDown);
            SetCursorStatus(CursorStatusTypes.Hidden);
        }
        else
        {
            SetCursorStatus(CursorStatusTypes.Normal);
        }
    }
    
    public void Move(Vector2 movementVector, Vector2 mousePosition2D, float moveUpDown)
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
        if (_inputReader.speedUpIsBeingPressed)
        {
            speedModifier = (float)(m_shiftspeedModifier);
        }

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