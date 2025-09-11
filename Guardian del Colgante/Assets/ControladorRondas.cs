using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PrefabEntry
{
    public GameObject prefab;   // acá arrastrás el prefab
    public int rondaEnLaQueAparece;
}

public class ControladorRondas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject spawnerIzq;
    GameObject spawnerDer;

    GameObject enemigosGameobject;

    int ronda = 1;

    int enemigosSpawneados = 0;
    int nroEnemigosMax = 0;

    public float duracionEntreSpawnDeEnemigos = 2.0f;
    private float duracionEntreSpawnDeEnemigosTimer = 0.0f;
    public float duracionEntreRondas = 5.0f;
    private float duractionEntreRondasTimer = 0.0f;
    public PrefabEntry[] enemigosPosibles;  // lista de prefabs con su nombre

    string estado = "ronda";

    bool colectivoDeLadoIzquierdo = false;
    bool colectivoDeLadoDerecho = false;

    void Start()
    {
        enemigosGameobject = GameObject.FindWithTag("Enemigos");
        actualizarEnemigosMax();
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.name == "SpawnerIzq") spawnerIzq = childTransform.gameObject;
            else spawnerDer = childTransform.gameObject;
        }
    }

    int getNumeroEnemigos(int rondaActual) {
        if (rondaActual == 1)
        {
            return 7;
        }
        else {
            return getNumeroEnemigos(rondaActual - 1) + rondaActual + 1;
        }
    }
    void actualizarEnemigosMax() {
        nroEnemigosMax = getNumeroEnemigos(this.ronda);
    }
    
    void Update()
    {
        if (enemigosSpawneados > nroEnemigosMax && enemigosGameobject.transform.childCount == 0) {
            cambiarDeRonda();
        }
        if (this.estado == "ronda") {
            duracionEntreSpawnDeEnemigosTimer += Time.deltaTime;
            if (duracionEntreSpawnDeEnemigosTimer > duracionEntreSpawnDeEnemigos && enemigosGameobject.transform.childCount <= nroEnemigosMax) {
                generarEnemigo();
                duracionEntreSpawnDeEnemigosTimer = 0;
            }
        }
    }
    void generarEnemigo() {
        bool enemigoValido = false;
        bool ladoIzq = false;
        PrefabEntry randomEnemy = null;

        while (!enemigoValido) {
            ladoIzq = Random.value > 0.5f;
            int randomIndex = Random.Range(0, enemigosPosibles.Length);
            randomEnemy = enemigosPosibles[randomIndex];
            print(randomEnemy.prefab);
            if (this.ronda < randomEnemy.rondaEnLaQueAparece)
            {
                continue;
            }
            else
            {
                if (randomEnemy.prefab.name == "Colectivo")
                {
                    if (ladoIzq)
                    {
                        if (colectivoDeLadoIzquierdo)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (colectivoDeLadoDerecho)
                        {
                            continue;
                        }
                    }
                }
            }
            enemigoValido = true;
        }
        Vector3 posicion;
        if (ladoIzq)
        {
            posicion = spawnerIzq.GetComponent<Spawner>().getRandomPosition();
        }
        else {
            posicion = spawnerDer.GetComponent<Spawner>().getRandomPosition();
        }
        Instantiate(randomEnemy.prefab, posicion, Quaternion.identity, enemigosGameobject.transform);
    }
    void cambiarDeRonda() {
        
        this.ronda++;
        print("CAMBIO DE RONDA A " + this.ronda);
        actualizarEnemigosMax();
        enemigosSpawneados = 0;
        duractionEntreRondasTimer = 0.0f;
    }
}
