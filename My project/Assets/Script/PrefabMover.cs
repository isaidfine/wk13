using UnityEngine;

public class PrefabMover : MonoBehaviour
{
    private float moveSpeed;
    private float destroyXPosition = -1f; // 到达这个x坐标时销毁

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (transform.position.x <= destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}