using UnityEngine;
using System.Linq;

public class ShowingBulletDecals : MonoBehaviour
{
    [SerializeField] private Decal[] _defaultDecals;
    [SerializeField] private Decal[] _bloodDecals;

    public void Show(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Item item))
            return;

        Decal decal;

        if (hit.collider.TryGetComponent(out Zombie zombie) || hit.collider.TryGetComponent(out ZombieLimb zombieLimb))
        {
            decal = _bloodDecals.FirstOrDefault(decal => decal.gameObject.activeSelf == false);

            if(decal != null)
                decal.Set(hit.collider.transform);
        }
        else
        {
            decal = _defaultDecals.FirstOrDefault(decal => decal.gameObject.activeSelf == false);
        }
           
        if (decal != null)
        {
            decal.transform.position = hit.point;
            decal.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            decal.gameObject.SetActive(true);
        }
    }
}
