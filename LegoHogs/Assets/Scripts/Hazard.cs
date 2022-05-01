using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private LevelManager myLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        myLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        FindObjectOfType<LevelManager>().OnEnterHazard(collision.gameObject);

        myLevelManager.OnReSpawn();

    }
}
