using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class vfx_controller : MonoBehaviour
{
	[SerializeField]private GameObject explosion;
	[Range(0, 10)]public float explosion_size;
    public int damage = 5;
    public bulletType bulType;
	public bool wasHit;
	private Vector3 v;
    void Awake()
    {
        this.gameObject.tag = "vfx";
    }

    public void hit()
	{
        if (!wasHit)
        {
            v = transform.position;
            wasHit = true;
            if (explosion_size > 0)
            {
                Collider[] c = Physics.OverlapSphere(v, explosion_size, Physics.AllLayers);
                Instantiate(explosion, v, Quaternion.identity);
                for (int i = 0; i < c.Length; i++)
                {
                    var enem = c[i].gameObject.GetComponentInParent<Enemy>();
                    if (c[i].tag == "vfx")
                    {
                        c[i].gameObject.GetComponent<vfx_controller>().hit();
                    }
                    if (enem)
                    {
                        var p = Player.Instance.GetComponentInChildren<Gun>();
                        enem.hit(damage, p.myPartColors[0]);
                    }
                }
            }
            Destroy(gameObject);
        }
    }
}
