using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {

    [SerializeField]
    private GameObject m_ExpPrehab;
    private ParticleSystem m_ExpParticle;
    [SerializeField]
    private GameObject m_DirtPrehab;
    private ParticleSystem m_DirtParticle;
    private bool isBroken;
    [SerializeField]
    private float breakTime = 0.8f;
    [SerializeField]
    private float size_Y = 10f;
    [SerializeField]
    private float break_RotMax = 30f;
    private float time;

    private Vector3 startPosition;
    private Vector3 startRotate;
    private Vector3 breakVelocity;
    private Vector3 breakRotate;

	// Use this for initialization
	void Start () {
        startPosition = this.transform.position;
        startRotate = this.transform.rotation.eulerAngles;

        SetParticleConfig(m_ExpPrehab, m_ExpParticle);
        SetParticleConfig(m_DirtPrehab, m_DirtParticle);

        SetBreakValue();
	}
	
    void SetParticleConfig(GameObject prehab, ParticleSystem target)
    {
        target = Instantiate(prehab).GetComponent<ParticleSystem>();
        target.transform.position = this.startPosition;
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

    public void Broken()
    {
        if (isBroken) return;

        isBroken = true;
        m_ExpParticle.Play();
        m_ExpParticle.GetComponent<AudioSource>().Play();
    }
}
