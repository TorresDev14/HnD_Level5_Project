using UnityEngine;

public class RecoilScript : MonoBehaviour
{
    private Vector3 currentRotaion;
    public static Vector3 targetRotation;

    [SerializeField] private float recoilX, recoilY, recoilZ;

    [SerializeField] private float aimRecoilX, aimRecoilY, aimRecoilZ;

    [SerializeField] private float snappiness, returnSpeed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotaion = Vector3.Slerp(currentRotaion, targetRotation, snappiness * Time.fixedDeltaTime);
       // transform.localRotation = Quaternion.Euler(currentRotaion); ;
    }

    public void RecoilFire()
    {
        if (GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponAnimation>().isAiming == true)
        {
            targetRotation += new Vector3(aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
        }
        else targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

}
