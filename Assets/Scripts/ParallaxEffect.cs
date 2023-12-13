using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float efectoParallax;

    private Transform camara;
    private Vector3 ultimaPosCam;


    private void Start()
    {
        camara = Camera.main.transform;
        ultimaPosCam = camara.position;
    }

    private void LateUpdate()
    {
        Vector3 movimientoBG = camara.position - ultimaPosCam;
        transform.position += new Vector3(movimientoBG.x * efectoParallax, movimientoBG.y, 0);
        ultimaPosCam = camara.position;
    }
}
