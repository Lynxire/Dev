using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameObject _bullet;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Instantiate(_bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
        }
    }

}
