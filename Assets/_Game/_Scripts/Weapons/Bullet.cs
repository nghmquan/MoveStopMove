using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float time;
    private GameObject owner;
    private bool hasCollided = false;

    private void Start()
    {
        StartCoroutine(SetTimeNotCollided(time));
    }

    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject != owner && _collision.gameObject.CompareTag("Character"))
        {
            hasCollided = true;
            Destroy(gameObject);
        }else if(_collision.gameObject != owner)
        {
            hasCollided = true;
            Destroy(gameObject);
        }
    }
    
    //Bộ đếm thời gian để destroy bullet nếu không va chạm bất cứ collider nào
    private IEnumerator SetTimeNotCollided(float _time)
    {
        yield return new WaitForSeconds(_time);

        if (!hasCollided)
        {
            Destroy(gameObject);
        }
    }
}
