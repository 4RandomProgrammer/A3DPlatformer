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
    public PlayerController playerRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {

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
        playerRigidbody.changeGravity();

        // Espera pelo tempo de duração
        if (invertedGravityDuration > 0)
        {  
            yield return new WaitForSeconds(invertedGravityDuration);

            // Retorna a gravidade ao normal
            playerRigidbody.changeGravity();
            Physics.gravity = new Vector3(0, gravityForce, 0);
            gravityInverted = false;
        }
    }
}
