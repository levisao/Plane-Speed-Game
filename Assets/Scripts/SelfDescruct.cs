using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDescruct : MonoBehaviour
{
    [Range(0, 5)][SerializeField] float destroyParticlesObjectTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyParticlesObjectTime);
    }
}
