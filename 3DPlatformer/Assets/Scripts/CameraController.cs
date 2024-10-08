using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // Referência ao jogador/transform do personagem
    public Vector3 offset;    // Offset entre a câmera e o jogador
    public float sensitivity = 5f;  // Sensibilidade do mouse para rotação
    public float minYAngle = -30f;  // Limite mínimo de rotação vertical
    public float maxYAngle = 60f;   // Limite máximo de rotação vertical

    private float currentX = 0f;  // Ângulo atual no eixo X (horizontal)
    private float currentY = 0f;  // Ângulo atual no eixo Y (vertical)

    void Start()
    {
        // Calcula o offset inicial com base na posição inicial da câmera e do jogador
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Obtém o movimento do mouse para ajustar a rotação
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Limita o ângulo vertical para evitar que a câmera gire muito para cima ou para baixo
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // Calcula a rotação com base nos ângulos X e Y
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Aplica a rotação e ajusta a posição da câmera em relação ao jogador
        transform.position = player.position + rotation * offset;

        // Faz com que a câmera sempre olhe para o jogador
        transform.LookAt(player);
    }
}
