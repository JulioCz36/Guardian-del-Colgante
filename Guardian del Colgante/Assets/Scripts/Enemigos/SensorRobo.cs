using UnityEngine;

public class SensorRobo : MonoBehaviour
{
    private Ladron ladron;

    void Start()
    {
        ladron = GetComponentInParent<Ladron>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HurtboxMaterialObjetivo"))
        {
            ladron.EmpezarRobo();
        }
    }
}
