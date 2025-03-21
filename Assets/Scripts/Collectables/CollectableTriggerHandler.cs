using UnityEngine;

public class CollectableTriggerHandler : MonoBehaviour
{

    private Collectable _collectable;

    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collectable.Collect(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
