using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    
    private int _score;
    private int _currentHealth;
    
    public event UnityAction OnDamageTake;

    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _score += coin.CoinValue;
        } else if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.Damage < 0)
                return;

            _currentHealth -= enemy.Damage;
            OnDamageTake?.Invoke();
        }
        else if (other.TryGetComponent(out Heart heart))
        {
            if (_currentHealth != _health)
            {
                _currentHealth += heart.HealValue;
            }
        }
    }
}
