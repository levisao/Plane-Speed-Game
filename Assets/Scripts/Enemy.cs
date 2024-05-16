using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int enemyHits = 0;
    [Tooltip("Total HP of the enemy")]
    [Range(0,10)][SerializeField] int enemyLife = 2;

    [Tooltip("The amount of points this enemy gets the player when destroyed")]
    [Range(0, 10)][SerializeField] int pointAmount = 1; 

    [Tooltip("Particle System assign to the explosion of enemies")]
    [SerializeField] GameObject deathParticlesVfx; //game object pq será criado um objeto no mundo
    [SerializeField] GameObject hitParticleVfx;

    [Tooltip("Parent Empty object Transform to store the Particles that are going to be created at runtime")]
    GameObject parentObject;

    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>(); // Runs through the entire project and the very first obj of this Type that it finds, it's then the object we're refering to // Don't use on update, it resource heavy
        parentObject = GameObject.FindWithTag("SpawnAtRunTime"); //pegando objeto com essa tag
        
    }

    private void Awake()
    {
        AddRigidBody();
        
    }
    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); //adicionando um componente por código
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {

        HitVfx();
        TakeDamage(other);
    }

    private void HitVfx()
    {
        GameObject hitVfx = Instantiate(hitParticleVfx, transform.position, Quaternion.identity);
        hitVfx.transform.parent = parentObject.transform;
    }

    private void TakeDamage(GameObject other)
    {
        enemyHits++;
        
        if (enemyHits >= enemyLife) // preferi fazer com um contador ao invez de diminuir a vida do objeto, pois assim tenho sempre acesso ao hp total do inimigo
        {
            KillEnemy();
        }

        Debug.Log($"{name} HIT BY {other.gameObject.name}"); // outro jeito de escrever string no debug.log
    }

    private void KillEnemy()
    {
        
            GameObject vfx = Instantiate(deathParticlesVfx, transform.position, Quaternion.identity); // criando, instancando, novo game object do tipo GameObject na variavel vfx
            vfx.transform.parent = parentObject.transform; //.parent means we1re assigning a gameobject parent to this new object, which is the empty object we created
                                                    // criamos uma instancia do tipo Transform porque o Transform tem ese método de atribuir um parent

            Destroy(gameObject);
            UpdateScore();
        
    }

    private void UpdateScore()
    {
        scoreBoard.IncreaseScore(pointAmount);
    }
}
