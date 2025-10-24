using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    private bool _isDragging = false;
    private Vector2 _lastTouchPos;

   private void Update()
    {
        if (!Application.isMobilePlatform)
            PCUpdate();
        else
            MobileUpdate();
    }

    private void PCUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x, 0, z) * _moveSpeed * Time.deltaTime;
    }
    private void MobileUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    _lastTouchPos = touch.position;
                    _isDragging = true;
                    break;
                case TouchPhase.Moved:
                    if (_isDragging)
                    {
                        Vector3 moveDir = -(touch.position - _lastTouchPos);
                        transform.position += moveDir * _moveSpeed * Time.deltaTime;

                        _lastTouchPos = touch.position;
                    }
                    break;
                    case TouchPhase.Ended:
                    _isDragging = false;
                    break;
            }
        }
    }
}
