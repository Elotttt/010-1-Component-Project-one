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
        //Debug.Log(dist);
        
        GetComponent<Renderer>().material.SetFloat("_Distance", dist);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided!");

        if (other.tag == "Player")
        {
            StartCoroutine(DissolveOut(1));
        }
    }

    IEnumerator DissolveOut(float duration)
    {
        float start = 0f;
        float end = 1f;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            GetComponent<Renderer>().material.SetFloat("_Dissolve", Mathf.Lerp(start, end, time / duration));
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
