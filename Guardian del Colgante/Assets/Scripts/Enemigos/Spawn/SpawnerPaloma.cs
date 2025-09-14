using UnityEngine;

public class SpawnerPaloma : MonoBehaviour
{
    public GameObject palomaPrefab;
    public float intervalo = 5f;
    public int maxPalomas = 3;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= intervalo && transform.childCount < maxPalomas)
        {
            Instantiate(palomaPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
