using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves in the 4 directions based on player input")]
    [SerializeField] float forceThrow = 10f;
    [Tooltip("How far the ship can go Left and Right")]
    [SerializeField] float horizontalRange = 10f;
    [Tooltip("How far the ship can go Up and Down")]
    [SerializeField] float verticalRange = 3.5f;

    [Header("Rotation of the Airship Settings")]
    [Tooltip("How much the ship rotates when moving Up and Down")]
    [SerializeField] float xRotationAmount = 0.6f;  //Pitch
    [Tooltip("How much the ship rotates when moving Left and Right to face de center of the screen")]
    [SerializeField] float yRotationAmount = 0.03f;  //Yall
    [Tooltip("How much the ship rotates when moving Left and Right")]
    [SerializeField] float zRotationAmount = 0.7f;  //Row

    [Header("Lasers Array")]
    [Tooltip("Add all Player lasers here")]
    [SerializeField] GameObject[] lasers;


    float horizontalThrow;
    float verticalThrow;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotation();
        Fire();

    }

    private void Movement()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        float horizontalOffset = horizontalThrow * Time.deltaTime * forceThrow;
        float newHorizontalPos = transform.localPosition.x + horizontalOffset;
        float clampedHorizontalPos = Mathf.Clamp(newHorizontalPos, -horizontalRange, horizontalRange); // limitando o valor de newHorizontalPos para os valores de horizontalRange
        

        float verticalOffset = verticalThrow * Time.deltaTime * forceThrow;
        float newVerticalPos = transform.localPosition.y + verticalOffset;
        float clampedVerticalPos = Mathf.Clamp(newVerticalPos, -verticalRange, verticalRange);

        transform.localPosition = new Vector3(clampedHorizontalPos, clampedVerticalPos, transform.localPosition.z);
    }

    private void Rotation()
    {
        float pitch = xRotationAmount * -verticalThrow; //the airship is going to go to this position an go back to normal, as it is being updated through the input value
        float yaw = yRotationAmount * -transform.localPosition.x; //the airship is going to stay ate this position
        float roll = zRotationAmount * -horizontalThrow; //same as pitch

        transform.localRotation = quaternion.Euler(pitch, yaw, roll);

    }

    /*
     * private void Fire2() //modo mais simples, só que menos sofisticado, teria q adicionar todos os lasers possiveis manualmente
    {
           if (Input.GetMouseButton(0))
            {
            laserLeft.SetActive(true);
            //laserLeft.GetComponent<Renderer>().enabled = true;
            }
            else
            {
            laserLeft.SetActive(false);
            //laserLeft.GetComponent<Renderer>().enabled = false;
            }
        
               
            if (Input.GetMouseButton(1))
            {
            laserRight.SetActive(true);
        }
            else
            {
            laserRight.SetActive(false);
        }
   
    }
    */

    private void Fire() //modo mais simples, só que menos sofisticado, teria q adicionar todos os lasers possiveis manualmente
    {
        if (Input.GetMouseButton(0))
        {
            ActivateLasers(true);
        }
        else
        {
            {
                ActivateLasers(false);
            }
        }
    }

    private void ActivateLasers(bool isActive) // fazer dessa forma tbm para ficar com menos linhas possivel fazendo tudo de uma vez
    {
        foreach (GameObject laser in lasers) // correndo pela array de lasers adicionados e ativando-os
        {
            //laser.SetActive(true);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission; //por algum motivo tem q ser desse jeito
            emissionModule.enabled = isActive;  // desativando e ativando somente a emission para as particles poderem viajar ate o destino sem sumirem antes de chegar lá,
                                                //  que é o que acontece se desativa o objeto inteiro
        }
    }

   
}
