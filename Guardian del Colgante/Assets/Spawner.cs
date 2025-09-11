using UnityEngine;

public class Spawner : MonoBehaviour
{
    Transform puntoA;
    Transform puntoB;
    
    void Start()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.name == "PuntoA") puntoA = childTransform;
            else puntoB = childTransform;
        }
    }

    // Update is called once per frame
    public Vector3 getRandomPosition() {
        Vector3 direction = puntoB.position - puntoA.position;
        return puntoA.position + (direction * Random.value);
    }

}
