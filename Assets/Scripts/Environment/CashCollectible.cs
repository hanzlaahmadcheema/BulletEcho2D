using UnityEngine;

public class CashCollectible : MonoBehaviour
{
    [Header("Objective Value")]
    [Tooltip("Amount added to GameManager.collectedCash when the Player enters this object's trigger collider.")]
    [Min(1)]
    public int cashAmount = 1;

    private bool collected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected || other.GetComponent<PlayerHealth>() == null)
        {
            return;
        }

        collected = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectCash(cashAmount);
        }

        Destroy(gameObject);
    }
}
