using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveAlongPoints : MonoBehaviour
{
    public List<Transform> transforms;      // список точек, куда двигаться
    public float speed = 2f;             // скорость движения
    public float threshold = 0.1f;       // расстояние до точки, при котором переходить к следующей

    private int currentIndex = 0;

    void Update()
    {
        if (transforms == null || transforms.Count == 0) return;

        Vector3 target = transforms[currentIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < threshold)
        {
            currentIndex = (currentIndex + 1) % transforms.Count; // цикл по точкам
        }
    }
}