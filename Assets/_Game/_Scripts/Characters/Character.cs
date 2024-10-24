using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Character properties")]
    [SerializeField] private Rigidbody rigidbodyCharacter;
    [SerializeField] private Animator animationCharacter;
    [SerializeField] private GameObject weaponHolding;
    [SerializeField] private float moveSpeed;

    [Header("Weapons Properties")]
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform bulletSpawnPostion;

    private Quaternion lookDirection;
    private string currentAnimationName;

    protected virtual void OnInit()
    {
        //Instantiate(weaponPrefab, weaponHolding.transform);
        weapon = GetComponent<Weapon>();
    }

    //Movement character
    protected virtual void Move(Vector3 _postion)
    {
        //moveDirection = new Vector3(_horizontalDirection, 0 , _verticalDirection);
        rigidbodyCharacter.velocity = _postion * moveSpeed + new Vector3(0, rigidbodyCharacter.velocity.y, 0);

        if (_postion.x != 0 || _postion.z != 0)
        {
            lookDirection = Quaternion.LookRotation(_postion).normalized;
            transform.rotation = lookDirection;

            ChangeAniamtion("run", true);
        }
        else
        {
            ChangeAniamtion("idle", true);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeAniamtion("attack", true);
            weapon.Shoot();
        }
    }

    protected virtual void FindTarget() { }

    protected virtual void Die() { }

    protected virtual void UpSize() { }

    protected virtual void ChangeWeapon(/*WeaponType _weaponType*/) { }

    protected virtual void ChangeHat(/*HatType _hatType*/) { }

    protected virtual void ChangePant(/*PantType _pantType*/) { }

    //Animation character
    private void ChangeAniamtion(string _animationName, bool _stateAniamtion)
    {
        ResetAllAniamtionBools();
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

    private void ResetAllAniamtionBools()
    {
        animationCharacter.SetBool("IsIdle", false);
        animationCharacter.SetBool("IsDead", false);
        animationCharacter.SetBool("IsAttack", false);
        animationCharacter.SetBool("IsWin", false);
        animationCharacter.SetBool("IsDance", false);
        animationCharacter.SetBool("IsUlti", false);
    }
}
