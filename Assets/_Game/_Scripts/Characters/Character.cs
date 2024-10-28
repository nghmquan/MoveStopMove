using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character properties")]
    [SerializeField] private Rigidbody rigidbodyCharacter;
    [SerializeField] private Collider colliderCharacter;
    [SerializeField] private Animator animationCharacter;
    [SerializeField] private GameObject weaponHolding;
    [SerializeField] private GameObject areaCombat;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeAfterDeadAnimation;
    [SerializeField] private float timeToAttack;
    [SerializeField] private float rotationSpeed;
    public bool isDead  = false;

    [Header("Weapons Properties")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform bulletSpawnPostion;

    [SerializeField] private int enemyCount = 0;

    private Quaternion lookDirection;
    private string currentAnimationName;

    protected virtual void OnInit()
    {
        weapon = GetComponent<Weapon>();
        weapon.SetOwner(this);
    }

    //Character movement
    protected virtual void Move(Vector3 _postion)
    {
        rigidbodyCharacter.velocity = _postion * moveSpeed + new Vector3(0, rigidbodyCharacter.velocity.y, 0);

        if (_postion.x != 0 || _postion.z != 0)
        {
            lookDirection = Quaternion.LookRotation(_postion).normalized;
            transform.rotation = lookDirection;

            ChangeAnimation("run", true);
        }
        else
        {
            if (isDead == true)
            {
                return;
            }

            ChangeAnimation("idle", true);
        }
    }

    //Character attack
    public void Attack()
    {
        ChangeAnimation("attack", true);
        weapon.Shoot();
    }

    private void LockTarget(Collider _target)
    {
        Vector3 directionToTarget = _target.transform.position - transform.position;
        directionToTarget.y = 0;

        Quaternion targetRotaion = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotaion, rotationSpeed);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Character") && _other.GetComponent<Character>())
        {
            LockTarget(_other);
            StartCoroutine(WaitForAttack());
        }
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(timeToAttack);
        Attack();
    }

    public void CheckConditionUpSize()
    {
        enemyCount++;
        if(enemyCount == 3)
        {   
            UpSize();
        }
    }

    //Character death
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            ChangeAnimation("dead", true);
            areaCombat.SetActive(false);
            rigidbodyCharacter.useGravity = false;
            rigidbodyCharacter.constraints = RigidbodyConstraints.FreezeAll ;
            colliderCharacter.enabled = false;
            StartCoroutine(SetTimeAterDieAnimation());
            return;
        }
    }

    protected virtual IEnumerator SetTimeAterDieAnimation()
    {
        yield return new WaitForSeconds(timeAfterDeadAnimation);
        Destroy(this.gameObject);
    }

    //Change size character
    public void UpSize()
    {
        transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
    }

    //Change cloths character
    protected virtual void ChangeWeapon(/*WeaponType _weaponType*/) { }

    protected virtual void ChangeHat(/*HatType _hatType*/) { }

    protected virtual void ChangePant(/*PantType _pantType*/) { }

    //Animation character
    protected virtual void ChangeAnimation(string _animationName, bool _stateAniamtion)
    {
        ResetAllAnimationBools();
        switch (_animationName)
        {
            case "idle":
                animationCharacter.SetBool("IsIdle", _stateAniamtion);
                break;
            case "run":
                animationCharacter.SetBool("IsIdle", false);
                animationCharacter.SetBool("IsDead", false);
                animationCharacter.SetBool("IsAttack", false);
                animationCharacter.SetBool("IsWin", false);
                break;
            case "attack":
                animationCharacter.SetBool("IsAttack", _stateAniamtion);
                animationCharacter.SetBool("IsDead", false);
                animationCharacter.SetBool("IsWin", false);
                animationCharacter.SetBool("IsUlti", false);
                break;
            case "ulti":
                animationCharacter.SetBool("IsAttack", _stateAniamtion);
                animationCharacter.SetBool("IsDead", false);
                animationCharacter.SetBool("IsWin", false);
                animationCharacter.SetBool("IsUlti", _stateAniamtion);
                break;
            case "dance":
                animationCharacter.SetBool("IsDance", _stateAniamtion);
                break;
            case "win":
                animationCharacter.SetBool("IsWin", _stateAniamtion);
                animationCharacter.SetBool("IsDead", false);
                break;
            case "dead":
                animationCharacter.SetBool("IsDead", _stateAniamtion);
                break;
            default:
                Debug.Log(animationCharacter);
                break;
        }

        currentAnimationName = _animationName;
    }

    private void ResetAllAnimationBools()
    {
        animationCharacter.SetBool("IsIdle", false);
        animationCharacter.SetBool("IsDead", false);
        animationCharacter.SetBool("IsAttack", false);
        animationCharacter.SetBool("IsWin", false);
        animationCharacter.SetBool("IsDance", false);
        animationCharacter.SetBool("IsUlti", false);
    }
}