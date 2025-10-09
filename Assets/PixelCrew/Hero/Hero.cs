using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_direction == Vector2.zero) return;

        Vector3 movement = _direction * (_speed * Time.deltaTime);
        transform.position += movement;
    }

    public void SaySomething()
    {
        Debug.Log("SaySomething");
    }
}