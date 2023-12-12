using UnityEngine;

public class DiscController : MonoBehaviour
{
    public float autoRotationSpeed = 5f;
    public float rotationSensitivity = 0.1f; // 用于调整鼠标旋转的灵敏度
    public GameObject handSprite; // 用于显示的手的sprite

    public float currentRotationSpeed; // 公共变量，存储当前角速度

    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Vector3 discCenter;
    private Camera mainCamera;
    
    public bool IsDragging
    {
        get { return isDragging; }
    }
    
    private float accumulatedRotation = 0f;

    public float AccumulatedRotation
    {
        get { return accumulatedRotation; }
    }
    

    void Start()
    {
        discCenter = transform.position; // 假设光碟中心位于其Transform的位置
        mainCamera = Camera.main;
        handSprite.SetActive(false); // 初始时隐藏手的sprite
        currentRotationSpeed = autoRotationSpeed; // 初始设置为自动旋转速度
    }

    void Update()
    {
        if (IsDragging)
        {
            RotateDiscWithMouse();
            MoveHandSprite();
        }
        else
        {
            AutoRotateDisc();
        }

        HandleMouseInput();
    }

    void AutoRotateDisc()
    {
        transform.Rotate(0, 0, autoRotationSpeed * Time.deltaTime);
        currentRotationSpeed = autoRotationSpeed; // 更新为自动旋转速度
    }

    void RotateDiscWithMouse()
    {
        Vector3 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentMousePosition.z = discCenter.z;

        float angle = Vector2.SignedAngle(lastMousePosition - discCenter, currentMousePosition - discCenter);
        float rotationAmount = angle * rotationSensitivity;

        transform.Rotate(0, 0, rotationAmount);

        currentRotationSpeed = rotationAmount / Time.deltaTime; // 更新当前角速度

        lastMousePosition = currentMousePosition;
        
        accumulatedRotation += Mathf.Abs(rotationAmount);
        if (accumulatedRotation >= 135f)
        {
            accumulatedRotation -= 135f;
        }
    }

    public void ResetAccumulatedRotation()
    {
        accumulatedRotation = 0f;
    }
    
    void MoveHandSprite()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = discCenter.z;
        handSprite.transform.position = mousePosition; 
        handSprite.transform.rotation = Quaternion.identity;
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverDisc())
        {
            StartDragging();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    bool IsMouseOverDisc()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = discCenter.z;
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    void StartDragging()
    {
        isDragging = true;
        lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lastMousePosition.z = discCenter.z;
        handSprite.SetActive(true);
        Cursor.visible = false;
    }

    void StopDragging()
    {
        isDragging = false;
        handSprite.SetActive(false);
        Cursor.visible = true;
    }
}
