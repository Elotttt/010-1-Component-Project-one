using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityShader : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Mathf.Clamp(Vector3.Distance(player.transform.position, transform.position), 1, 10);
        Debug.Log(dist);
        
        GetComponent<Renderer>().material.SetFloat("_Distance", dist);
    }
}
