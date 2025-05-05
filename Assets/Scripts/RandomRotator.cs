using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 90f; // градусов в секунду
    private Vector3 rotationSpeed;

    void Start()
    {
        // Устанавливаем случайную скорость вращения по каждой оси в пределах [-maxSpeed, maxSpeed]
        rotationSpeed = new Vector3(
            Random.Range(-maxSpeed, maxSpeed),
            Random.Range(-maxSpeed, maxSpeed),
            Random.Range(-maxSpeed, maxSpeed)
        );
    }

    void Update()
    {
        // Вращаем объект с учетом времени
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}