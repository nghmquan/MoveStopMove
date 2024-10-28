using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float time;
    private Character character;

    private void Start()
    {
        StartCoroutine(SetTimeNotCollided(time));
    }

    public void SetOwner(Character _character)
    {
        this.character = _character;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Character"))
        {
            Character hitCharacter = _collision.gameObject.GetComponent<Character>();
            hitCharacter.Die();
            if(hitCharacter != null && hitCharacter != character)
            {
                character.CheckConditionUpSize();
                Destroy(gameObject);
            }
        }

        else if (_collision.gameObject)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator SetTimeNotCollided(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
