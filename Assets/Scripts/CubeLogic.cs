using UnityEngine;
using TMPro;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private bool bullet;
    [SerializeField] private float _jumpForce;
    [SerializeField] private GameObject cube;
    [SerializeField] private int _id;
    [SerializeField] private bool _inGame;
    [SerializeField] private TextMeshProUGUI _textInfo;
    public void Start()
    {
        _id = Random.Range(0,1000000);
        Init();
        Invoke("SetGameState", 2);
        if (bullet)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 25);
    }

    public int GetCount() => _count;
    public int GetId() => _id;
    public void SetCount(int value) 
    {
        _count = value;
        if (_count >= 2048)
        {
            Debug.Log("Win");
            _textInfo.text = "Win";
        }
        Init();
    }
    public void AddCount(int value) 
    {
        _count += value;
    }

    public void Init() 
    {
        if (_count <= 2048)
        {
            GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/" + _count.ToString());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeLogic>())
        {
            if (int.Equals(collision.gameObject.GetComponent<CubeLogic>().GetCount(), GetCount()) && GetId() > collision.gameObject.GetComponent<CubeLogic>().GetId())
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                GameObject CubePref = Instantiate(cube, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                CubePref.GetComponent<CubeLogic>().Jump();
                CubePref.GetComponent<CubeLogic>().SetCount(collision.gameObject.GetComponent<CubeLogic>().GetCount() + GetCount());
                CubePref.GetComponent<CubeLogic>().Init();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            if (_inGame)
            {
                _textInfo.text = "Fail";
            }
        }
    }

    private void Jump()
    {
        float x = Random.Range(-1f,1f);
        float z = Random.Range(-1f, 1f);
        GetComponent<Rigidbody>().AddForce(new Vector3(x * _jumpForce, _jumpForce, z * _jumpForce), ForceMode.Impulse);
    }


    public void SetGameState() 
    {
        _inGame = true;
    }
}
