using UnityEngine;

public class Perro : Ladron
{

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        //player = GameObject.FindGameObjectWithTag("Player");

        //estado = "busqueda";
        //direccion = (materialObjetivo.transform.position - transform.position).normalized;
        //rb.linearVelocity = direccion * speed;
    }

    void Update()
    {
        // Máquina de estados.
        //if (estado == "frenado")
        //{
        // frenadoTimer += Time.deltaTime;

        //rb.linearVelocity *= 0.99f;
        // if (frenadoTimer >= tiempoFreno)
        // {
        // ActualizarDireccion();
        // frenadoTimer = 0.0f;
        // estado = "busqueda";
    }
}

// if (estado == "busqueda")
//{
//busquedaTimer += Time.deltaTime;
//rb.linearVelocity = direccion * speed;
//if (busquedaTimer >= tiempoBusqueda)
//{
//    busquedaTimer = 0.0f;
//    estado = "frenado";
//}
//}

// if (estado == "escape")
//{
// rb.linearVelocity = Vector2.right * velocidadDeEscape;
//
// }
//}
