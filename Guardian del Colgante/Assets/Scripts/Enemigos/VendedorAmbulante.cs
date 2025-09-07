using UnityEngine;

public class VendedorAmbulante : HinchaColon
{

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        materialObjetivo = GameObject.FindGameObjectWithTag("MaterialObjetivo");
        player = GameObject.FindGameObjectWithTag("Player");
        ActualizarDireccion();
    }
    void Update()
    {
        // Máquina de estados.
        if (estado == "ataque")
        {
            if (timerCascote >= cadenciaDeTiroDeCascotes)
            {
                GameObject casc = Instantiate(cascote);
                Cascote cascScript = casc.GetComponent<Cascote>();
                Vector2 offset = new Vector2(Random.Range(-offsetPosible, offsetPosible), Random.Range(-offsetPosible, offsetPosible));
                Vector2 posicionFinal = new Vector2(this.player.transform.position.x, this.player.transform.position.y) + offset;

                cascScript.Init(this.transform.position, posicionFinal, -gravedad, tiempoTiro, this.danoAEstructura, this.danoAJugador);
                timerCascote = 0.0f;

                if (Vector2.Distance(player.transform.position, this.transform.position) > distanciaHastaEmpezarAAtacar)
                {
                    //ActualizarDireccion();
                    estado = "busqueda";
                }
            }

            timerCascote += Time.deltaTime;

            rb.linearVelocity *= 0.99f;

        }

        if (estado == "busqueda")
        {
            //busquedaTimer += Time.deltaTime;
            ActualizarDireccion();
            rb.linearVelocity = direccion * speed;
            if (Vector2.Distance(player.transform.position, this.transform.position) <= distanciaHastaEmpezarAAtacar)
            {
                estado = "ataque";
            }
        }


    }
    protected new void ActualizarDireccion()
    {
        direccion = (player.transform.position - transform.position).normalized;
    }
}
