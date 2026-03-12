using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private Snake snake;

    private void Start()
    {
        snake = FindFirstObjectByType<Snake>();   // FIXED reference
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        while (snake != null && snake.Occupies(x, y))
        {
            x++;

            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;

                if (y > bounds.max.y)
                    y = Mathf.RoundToInt(bounds.min.y);
            }
        }

        transform.position = new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // IMPORTANT: Must be "Food" and "Player"
        if (other.CompareTag("Player"))
        {
            RandomizePosition();
        }
    }
}
