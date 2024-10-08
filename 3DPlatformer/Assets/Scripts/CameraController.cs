using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // Refer�ncia ao jogador/transform do personagem
    public Vector3 offset;    // Offset entre a c�mera e o jogador
    public float sensitivity = 5f;  // Sensibilidade do mouse para rota��o
    public float minYAngle = -30f;  // Limite m�nimo de rota��o vertical
    public float maxYAngle = 60f;   // Limite m�ximo de rota��o vertical

    private float currentX = 0f;  // �ngulo atual no eixo X (horizontal)
    private float currentY = 0f;  // �ngulo atual no eixo Y (vertical)

    void Start()
    {
        // Calcula o offset inicial com base na posi��o inicial da c�mera e do jogador
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Obt�m o movimento do mouse para ajustar a rota��o
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Limita o �ngulo vertical para evitar que a c�mera gire muito para cima ou para baixo
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // Calcula a rota��o com base nos �ngulos X e Y
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Aplica a rota��o e ajusta a posi��o da c�mera em rela��o ao jogador
        transform.position = player.position + rotation * offset;

        // Faz com que a c�mera sempre olhe para o jogador
        transform.LookAt(player);
    }
}
