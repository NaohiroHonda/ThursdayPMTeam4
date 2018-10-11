using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {

    [SerializeField]
    private GameObject m_BreakPrehab;
    private ParticleSystem m_BreakParticle;
    private bool isBroken;
    [SerializeField]
    private float breakTime;

	// Use this for initialization
	void Start () {
        m_BreakParticle = Instantiate(m_BreakPrehab).GetComponent<ParticleSystem>();

        SetBreakValue();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetBreakValue()
    {

    }

    public void Broken()
    {
        if (isBroken) return;

        isBroken = true;
        m_BreakParticle.Play();
    }
}
