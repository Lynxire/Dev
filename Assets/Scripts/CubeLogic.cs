using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private bool bullet;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GameObject cube;
    public bool acitve;
    public void Start()
    {
        Init();
        if (bullet)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 25);
    }

    public int GetCount() => _count;
    public void SetCount(int value) 
    {
        _count = value;
        Init();
    }
    public void AddCount(int value) 
    {
        _count += value;
    }

    public void Init() 
    {
        if (_count < 2048)
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + _count.ToString());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeLogic>())
        {
            if (int.Equals(collision.gameObject.GetComponent<CubeLogic>().GetCount(), GetCount()))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                if (acitve)
                {
                    GameObject CubePref = Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                    CubePref.GetComponent<CubeLogic>().Jump();
                    CubePref.GetComponent<CubeLogic>().SetCount(collision.gameObject.GetComponent<CubeLogic>().GetCount() + GetCount());
                    CubePref.GetComponent<CubeLogic>().Init(); 
                }

            }
        }
    }

    private void Jump()
    { 
        GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpForce, 0), ForceMode.Impulse);
    }
}
