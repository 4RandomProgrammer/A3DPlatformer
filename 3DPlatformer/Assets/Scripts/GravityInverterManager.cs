using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInverterManager : MonoBehaviour
{

    // Força de gravidade padrão do jogador
    public float gravityForce = -9.81f;

    // Tempo que a gravidade ficará invertida
    public float invertedGravityDuration = 5.0f;

    private bool gravityInverted = false;
    private Rigidbody playerRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null && !gravityInverted)
            {
                // Inverte a gravidade do jogador
                StartCoroutine(InvertGravity());
            }
        }
    }

    private IEnumerator InvertGravity()
    {
        gravityInverted = true;
        Physics.gravity = new Vector3(0, -gravityForce, 0); // Inverte a direção da gravidade

        yield return new WaitForSeconds(invertedGravityDuration); // Espera pelo tempo de duração

        // Retorna a gravidade ao normal
        Physics.gravity = new Vector3(0, gravityForce, 0);
        gravityInverted = false;
    }
}
