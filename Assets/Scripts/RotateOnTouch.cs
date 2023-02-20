using UnityEngine;

public class RotateOnTouch : MonoSingleton<RotateOnTouch>
{
    // Döndürülecek obje
    public GameObject objectToRotate;

    // Dokunmanýn baþlangýç konumu
    private Vector2 initialTouchPosition;

    // Dokunmanýn son konumu
    private Vector2 lastTouchPosition;

    // Minimum hareket mesafesi (dokunma iþleminin baþlamasý için)
    public float minimumMovement = 10.0f;

    // Döndürme hýzý
    public float rotationSpeed = 5.0f;

    // Dokunma iþlemi sýrasýnda objenin dönüp dönmemesi
    public bool canRotate = false;

    // Sýnýrlar
    public float leftLimit = -10.0f;
    public float rightLimit = 10.0f;

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
