using System;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Attractable") && !Input.GetKey(KeyCode.E))
        {
            other.transform.position = transform.position;
        }
    }
}
