using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{

    private Enemy enem;

    private Minecart mc;

    public enum EffectType { Stun, Slow, Weaken, ChainWeaken, DoT, Explosion, VoidSucc, AcidPool}

    private float stunTimer;

	[SerializeField]private Material fire;
	[SerializeField]private Material frost;
    [SerializeField] private Material shock;
    private Material[] originals;
	
	private Transform[] mcParts = new Transform[4];

    private List<EffectType> effects;

    // Start is called before the first frame update
    void Awake()
    {
        effects = new List<EffectType>();
        enem = GetComponent<Enemy>();
        if (!enem)
        {
            mc = GetComponent<Minecart>();
			originals = new Material[1];
			originals[0] = new Material(Resources.Load<Material>("Wheel/minecart_wheel"));
			mcParts = gameObject.GetComponentsInChildren<Transform>();
			/*int j = 0;
			for(int i=0; i< mcParts.Length; i++)
			{
				if(mcParts[i].name.Contains("minecartwheel"))
				{
					originals[j] = new Material(mcParts[i].GetChild(0).gameObject.GetComponent<MeshRenderer>().material);
					j++;
				}
			}*/
			frost = Resources.Load<Material>("GolemMats/GolemFrost");
        }
        else
        {
            string name = "Golem";
            if (enem.gameObject.name.Contains("Blaster"))
            {
                name = "Blaster";
            }

            originals = new Material[enem.BodyParts.Length];
            var i = 0;
            if (!name.Contains("Balloon"))
            {
                foreach (GameObject bodyPart in enem.BodyParts)
                {
                    originals[i] = new Material(bodyPart.GetComponent<SkinnedMeshRenderer>().material);
                    i++;
                }
            }
        }
    }

    public void AddEffect(EffectType newEffect, float effectFloat)
    {
        Debug.Log(newEffect);
        if (effects.Contains(newEffect)) //already one going
        {
            switch (newEffect)
            {
                case EffectType.DoT:
                    StopCoroutine("DoT");
                    break;
                case EffectType.Slow:
                    StopCoroutine("Slow");
                    break;
                case EffectType.Stun:
                    StopCoroutine("Stun");
                    if (mc)
                    {
                        mc.speed = 25;
                    }
                    break;
                case EffectType.Weaken:
                    StopCoroutine("Weaken");
                    enem.strikeDamage = enem.baseStrikeDamage;
                    break;
                case EffectType.ChainWeaken:
                    StopCoroutine("Weaken");
                    StopCoroutine("ChainWeaken");
                    enem.strikeDamage = enem.baseStrikeDamage;
                    break;
            }
            effects.Remove(newEffect); //remove effect from list
        }
        effects.Add(newEffect); //add effect to list
        switch (newEffect) //start new effect
        {
            case EffectType.DoT:
                StartCoroutine(DoT(effectFloat));
                break;
            case EffectType.Slow:
                StartCoroutine(Slow(effectFloat));
                break;
            case EffectType.Stun:
                StartCoroutine(Stun(effectFloat));
                break;
            case EffectType.Weaken:
                StartCoroutine(Weaken(effectFloat));
                break;
            case EffectType.ChainWeaken:
                StartCoroutine(ChainWeaken(effectFloat));
                break;
            case EffectType.Explosion:
                StartCoroutine(Explosion(effectFloat, effectFloat*3f));
                break;
        }
    }

    public IEnumerator VoidSucc(float range)
    {

        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            if (e != enem)
            {
                if (Vector3.Distance(e.transform.position, enem.transform.position) < range)
                {
                    float Force = Vector3.Distance(enem.transform.position, e.transform.position);
                    //e.GetComponent<Rigidbody>().AddForce();
                }
            }
        }

        yield return null;
    }

    public IEnumerator Weaken(float duration)
    {
        enem.strikeDamage = Mathf.RoundToInt(enem.baseStrikeDamage / 2);
        foreach (GameObject bodyPart in enem.BodyParts)
        {
            if (bodyPart.tag == "Shock" && !name.Contains("Balloon"))
            {
                bodyPart.GetComponent<SkinnedMeshRenderer>().material = shock;
            }
        }
        yield return new WaitForSeconds(duration);
        enem.strikeDamage = enem.baseStrikeDamage;
        var i = 0;
        foreach (GameObject bodyPart in enem.BodyParts)
        {
            if (bodyPart.tag == "Shock" && !name.Contains("Balloon"))
            {
                bodyPart.GetComponent<SkinnedMeshRenderer>().material = originals[i];
            }
            i++;
        }
    }

    public IEnumerator ChainWeaken(float duration)
    {
        Debug.Log("Chain Weaken");
        foreach (Enemy e in FindObjectsOfType<Enemy>())
        {
            if (e != enem && e && e.GetComponent<StatusEffect>())
            {
                if (Vector3.Distance(e.transform.position, enem.transform.position) < 5)
                {
                    e.GetComponent<StatusEffect>().AddEffect(EffectType.Weaken, duration);
                    GameObject g = Instantiate(Resources.Load("VFX/Prefabs/Lightning"), null) as GameObject;
                    g.transform.LookAt(e.transform);
                    Debug.Log("Weakened" + e);
                }
            }
        }
        AddEffect(EffectType.Weaken, duration);
        yield return null;
    }

    public IEnumerator Slow(float duration)
    {
        if (enem)
        {
            StartCoroutine("SlowAtk", duration);
        }
        StartCoroutine("SlowMovement", duration);
        yield return null;
    }

    public IEnumerator Stun(float duration)
    {
        if (enem)
        {
            StartCoroutine("StunAtk", duration);
        }
        StartCoroutine("StunMovement", duration);
        yield return null;
    }

    public IEnumerator SlowAtk(float duration) //coroutine to slow the enemy's attack speed
    {
		//Debug.Log("freeze");
		foreach(GameObject bodyPart in enem.BodyParts)
		{
			if (bodyPart.name.Contains("Leg") || bodyPart.name.Contains("Knee"))
				bodyPart.GetComponent<SkinnedMeshRenderer>().material = frost;
		}
        float timer = Time.time;
        enem.strikeTime += enem.OrigStrikeTime * .5f;
        yield return new WaitForSeconds(duration);
		//Debug.Log("Unfreeze");
		var i=0;
			foreach(GameObject bodyPart in enem.BodyParts)
			{
				if (bodyPart.name.Contains("Leg") || bodyPart.name.Contains("Knee"))
				{
					bodyPart.GetComponent<SkinnedMeshRenderer>().material = originals[i];
				}
				i++;
			}
        if (timer + duration >= Time.time)
        {
            enem.strikeTime -= enem.OrigStrikeTime * .5f;
        }
    }

    public IEnumerator StunMovement(float stunDuration) //coroutine to stop the enemy from moving
    {
        if (enem)
        {
            //Debug.Log(stunDuration);
			//Debug.Log("freeze");
			foreach(GameObject bodyPart in enem.BodyParts)
            {
                if (bodyPart.tag == "Freeze" && !name.Contains("Balloon"))
                {
                    bodyPart.GetComponent<SkinnedMeshRenderer>().material = frost;
                }
            }
            float stunTime = Time.time;
            enem.isStunnedMove = true;
            enem.myAgent.isStopped = true;
            enem.AnimationContorller.enabled = false;
            enem.myAgent.SetDestination(transform.position);
            yield return new WaitForSeconds(stunDuration);
            if ((Time.time >= stunTime + stunDuration)&& !(enem.isDead))
            {
				//Debug.Log("unfreeze");
			var i=0;
			foreach(GameObject bodyPart in enem.BodyParts)
			{
                    if (bodyPart.tag == "Freeze" && !name.Contains("Balloon"))
                    {
                        bodyPart.GetComponent<SkinnedMeshRenderer>().material = originals[i];
                    }
				i++;
            }
                enem.AnimationContorller.enabled = true;
                enem.myAgent.isStopped = false;
                enem.isStunnedMove = false;
            }
        }else if (mc)
        {
            //Debug.Log(stunDuration);
            float stunTime = Time.time;
            mc.speed = 0;
			foreach(Transform part in mcParts)
			{
				if(part.name.Contains("minecartwheel"))
				{
					part.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = frost;
				}
			}
            yield return new WaitForSeconds(stunDuration);
			foreach(Transform part in mcParts)
			{
				if(part.name.Contains("minecartwheel"))
				{
					part.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = originals[0];
				}
			}
            mc.speed = 25;
        }
    }

    public IEnumerator SlowMovement(float duration) //coroutine to slow the enemy's movement
    {
        //Debug.Log("SlowMe");
        if (enem)
        {
            float timer = Time.time;
            enem.myAgent.speed = enem.OrigMoveSpeed*.5f;
            yield return new WaitForSeconds(duration);
            if (timer + duration >= Time.time)
            {
                enem.myAgent.speed += enem.OrigMoveSpeed;
            }
        }else if (mc)
        {
			//transform.GetChild(1).transform.localPosition = new Vector3(transform.GetChild(1).transform.localPosition.x,
			//transform.GetChild(1).transform.localPosition.y+1f, transform.GetChild(1).transform.localPosition.z);
			foreach(Transform part in mcParts)
			{
				if(part.name.Contains("minecartwheel"))
				{
					part.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = frost;
				}
			}
            float timer = Time.time;
            mc.speed -= 10;
            if (mc.speed < 0)
            {
                mc.speed = 0;
            }
			
            yield return new WaitForSeconds(duration);
			Debug.Log("unslow");
			//transform.GetChild(1).transform.localPosition = new Vector3(transform.GetChild(1).transform.localPosition.x,
			//transform.GetChild(1).transform.localPosition.y-1f, transform.GetChild(1).transform.localPosition.z);
			foreach(Transform part in mcParts)
			{
                if (part)
                {
                    if (part.name.Contains("minecartwheel"))
                    {
                        part.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = originals[0];
                    }
                }
			}
            mc.speed += 10;
            if(mc.speed > 25)
            {
                mc.speed = 25;
            }
        }
    }

    public IEnumerator StunAtk(float stunDuration) //coroutine to stop the enemy from attacking
    {
        //Debug.Log(stunDuration);
        float stunTime = Time.time;
        enem.isStunnedAtk = true;
        yield return new WaitForSeconds(stunDuration);
        if (Time.time >= stunTime + stunDuration)
        {
            enem.isStunnedAtk = false;
        }
    }

    public IEnumerator Explosion(float damage, float distance) //coroutine to stop the enemy from attacking
    {
        foreach(Enemy e in FindObjectsOfType<Enemy>())
        {
            //if (e != enem)
            {
                if (Vector3.Distance(e.transform.position, enem.transform.position) < distance)
                {
                    var p = Player.Instance.GetComponentInChildren<Gun>();
                    e.hit((int)damage, p.myPartColors[0]);
                }
            }
        }
        yield return null;
    }

    public IEnumerator DoT(float duration)
    {
		foreach(GameObject bodyPart in enem.BodyParts)
        {
            if (bodyPart.tag == "Corrosive" && !name.Contains("Balloon"))
            {
                bodyPart.GetComponent<SkinnedMeshRenderer>().material = fire;
            }
            Debug.Log("Set Part Colors");
		}
        float startDot = Time.time;
        while (Time.time - startDot <= duration)
        {
            //Debug.Log(damage + " " + (int)(startDot - Time.time));
            var p = Player.Instance.GetComponentInChildren<Gun>();
            enem.hit(1, p.myPartColors[1]);
            yield return new WaitForSeconds(.9f);
        }
		if(!enem.isDead)
		{
			int i=0;
			foreach(GameObject bodyPart in enem.BodyParts)
			{
                if (bodyPart.tag == "Corrosive" && !name.Contains("Balloon"))
                {
                    bodyPart.GetComponent<SkinnedMeshRenderer>().material = originals[i];
                }
				i++;
			}
		}
    }

    public void AcidPool()
    {
        if (gameObject.name.Contains("Blaster")){
            Instantiate(Resources.Load("VFX/acid"), transform.position - Vector3.down * .2f, Quaternion.identity, null);
        }
        else {
            Instantiate(Resources.Load("VFX/acid"), transform.position + Vector3.down * 1.1f, Quaternion.identity, null);
        }
        Debug.Log("Spawned Acid");
    }
}
