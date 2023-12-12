using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public GameObject[] sprites; // 存放头尾相连的sprites
    public float speed = 5f; // 移动速度

    private float spriteWidth; // Sprite的宽度

    void Start()
    {
        // 假设所有sprites的宽度都是相同的
        spriteWidth = sprites[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        foreach (GameObject sprite in sprites)
        {
            // 向左移动每个sprite
            sprite.transform.Translate(Vector2.left * speed * Time.deltaTime);
            
            // 检查sprite是否完全离开屏幕
            if (sprite.transform.position.x < -spriteWidth)
            {
                // 将sprite移动到队列的末端
                Vector3 newPos = sprite.transform.position;
                newPos.x += spriteWidth * sprites.Length;
                sprite.transform.position = newPos;
            }
        }
    }
}