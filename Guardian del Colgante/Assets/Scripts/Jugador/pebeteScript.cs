using UnityEngine;

public class PebeteScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamara;
    private Rigidbody2D miRigidbody;

    public float fuerza = 5f;
    public float dano = 3.0f;

    private void OnEnable()
    {
        mainCamara = Camera.main;
        miRigidbody = GetComponent<Rigidbody2D>();

        mousePos = mainCamara.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direccion = mousePos - transform.position;

        miRigidbody.linearVelocity = new Vector2(direccion.x, direccion.y).normalized * fuerza;

        float rot = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {

            collision.SendMessageUpwards("recibirDano", dano);

            Destroy(gameObject);
        }
    }

    public void ConfigurarDamage(float nuevoDamage)
    {
        dano = nuevoDamage;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
