using UnityEngine;

public class RotateOnTouch : MonoSingleton<RotateOnTouch>
{
    // D�nd�r�lecek obje
    public GameObject objectToRotate;

    // Dokunman�n ba�lang�� konumu
    private Vector2 initialTouchPosition;

    // Dokunman�n son konumu
    private Vector2 lastTouchPosition;

    // Minimum hareket mesafesi (dokunma i�leminin ba�lamas� i�in)
    public float minimumMovement = 10.0f;

    // D�nd�rme h�z�
    public float rotationSpeed = 5.0f;

    // Dokunma i�lemi s�ras�nda objenin d�n�p d�nmemesi
    public bool canRotate = false;

    // S�n�rlar
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
