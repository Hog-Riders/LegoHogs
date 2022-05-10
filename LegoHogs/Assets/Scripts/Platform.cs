using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public enum platformColors { BLUE, YELLOW };
    [SerializeField] public platformColors platformColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<LevelManager>().OnEnterPlatform(gameObject);
    }
}
