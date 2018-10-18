using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BreakObject : MonoBehaviour {

    [SerializeField]
    private GameObject m_ExpPrefab; //CompleteTankExplosionを流用
    private GameObject m_ExpPrefabCopy;
    private ParticleSystem m_ExpParticle;
    [SerializeField]
    private GameObject m_HitPrefab; //CompleteShellExplosion
    private GameObject m_HitPrefabCopy;
    private ParticleSystem m_HitParticle;
    private bool isBroken;

    [SerializeField]
    private int breakCount = 2; //壊れるまでの必要Attack数
    private int hp;

    [SerializeField]
    private float breakTime = 0.8f;
    [SerializeField]
    private float size_Y = 10f;
    [SerializeField]
    private float break_RotMax = 30f;
    private float time;

    [SerializeField]
    private Collider[] colliders;

    private Vector3 startPosition;
    private Vector3 startRotate;
    private Vector3 breakVelocity;
    private Vector3 breakRotate;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
        startRotate = this.transform.rotation.eulerAngles;
        hp = breakCount;
        time = 0f;


        #region ParticleInit
        //AutoSet
        if(m_ExpPrefab == null) m_ExpPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Completed-Assets/Prefabs/CompleteTankExplosion.prefab", typeof(GameObject)) as GameObject;

        m_ExpPrefabCopy = Instantiate(m_ExpPrefab) as GameObject;
        m_ExpPrefabCopy.transform.position = startPosition;
        m_ExpParticle = m_ExpPrefabCopy.GetComponent<ParticleSystem>();


        if (m_HitPrefab == null) m_HitPrefab = AssetDatabase.LoadAssetAtPath("Assets/_Completed-Assets/Prefabs/CompleteShellExplosion.prefab", typeof(GameObject)) as GameObject;

        m_HitPrefabCopy = Instantiate(m_HitPrefab) as GameObject;
        m_HitPrefabCopy.transform.position = startPosition - new Vector3(0, size_Y, 0);
        m_HitParticle = m_HitPrefabCopy.GetComponent<ParticleSystem>();
        #endregion

        colliders = gameObject.GetComponents<Collider>();

        SetBreakValue();
	}

    void ReStart()
    {
        this.transform.position = startPosition;
        this.transform.rotation = Quaternion.Euler(startRotate);
        hp = breakCount;
        time = 0f;

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }


    // Update is called once per frame
    void Update () {
        if (isBroken && time < breakTime) BreakingMove();
	}

    void BreakingMove()
    {
        time += Time.deltaTime;
        this.transform.position = startPosition + Vector3.Lerp(Vector3.zero, breakVelocity, time / breakTime);
        this.transform.rotation = Quaternion.Euler(startRotate + Vector3.Lerp(Vector3.zero, breakRotate, time / breakTime));
    }

    void SetBreakValue()
    {
        breakRotate = new Vector3(break_RotMax * Random.Range(0f, 1f), break_RotMax * Random.Range(0f, 1f), break_RotMax * Random.Range(0f, 1f));

        breakVelocity.y = -size_Y * Random.Range(80f, 100f) / 100f;
    }

    public void Damage()
    {
        if (isBroken) return;

        if (--hp == 0)
        {

            isBroken = true;

            m_ExpParticle.Play(true);
            m_ExpParticle.GetComponent<AudioSource>().Play();

            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
        }
        else
        {
            m_HitParticle.Play(true);
        }
    }
}
