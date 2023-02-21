using UnityEngine;
using UnityEngine.UI;

public class RotateOnTouch : MonoSingleton<RotateOnTouch>
{
    public GameObject objectToRotate;

    private Vector2 initialTouchPosition;
    private Vector2 lastTouchPosition;

    public float minimumMovement = 10.0f;
    public float rotationSpeed = 5.0f;
    public bool canRotate = false;
    public float leftLimit = -10.0f;
    public float rightLimit = 10.0f;

    public Image target;

    void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {

            int touchCount = Input.touchCount;

            if (touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        initialTouchPosition = touch.position;
                        lastTouchPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        float movementDistance = Vector2.Distance(lastTouchPosition, touch.position);

                        if (movementDistance < minimumMovement)
                            return;

                        target.rectTransform.position = new Vector2(touch.position.x, (Camera.main.pixelHeight / 2) - 50);
                        float direction = Mathf.Sign(touch.position.x - lastTouchPosition.x);

                        float rotationAngle = direction * rotationSpeed * Time.deltaTime;

                        objectToRotate.transform.GetChild(MainManager.Instance.gunCount).transform.GetChild(0).Rotate(Vector3.up, rotationAngle, Space.World);
                        Vector3 currentRotation = objectToRotate.transform.GetChild(MainManager.Instance.gunCount).transform.GetChild(0).transform.eulerAngles;
                        currentRotation.z = 0.0f;
                        currentRotation.x = 0.0f;
                        currentRotation.y = ((touch.position.x / Camera.main.pixelWidth) * 80) - 40;
                        /*if (currentRotation.y > 300) currentRotation.y -= 360;
                        currentRotation.y = Mathf.Clamp(currentRotation.y, leftLimit, rightLimit);*/
                        objectToRotate.transform.GetChild(MainManager.Instance.gunCount).transform.GetChild(0).transform.eulerAngles = currentRotation;

                        lastTouchPosition = touch.position;

                        canRotate = true;
                        break;

                    case TouchPhase.Ended:
                        canRotate = false;
                        break;
                }
            }
        }

    }
}
