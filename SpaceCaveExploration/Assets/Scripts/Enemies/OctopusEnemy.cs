using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemy : MonoBehaviour {
    public float DistanceToDetectPlayer = 10.0f;
    public float MoveSpeed = 2.0f;

    private PlayerController m_PlayerReference;
    private bool m_bIsChasingPlayer;
    private Vector2 m_DirectionMoving;

    private void Awake() {
        m_PlayerReference = FindObjectOfType<PlayerController>();
        m_DirectionMoving = Random.insideUnitCircle;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanceToDetectPlayer);
    }

    private void Update() {
        if(m_bIsChasingPlayer) {
            if(m_PlayerReference.IsMoving()) {
                m_DirectionMoving = (m_PlayerReference.transform.position - transform.position).normalized;
            } else {
                m_bIsChasingPlayer = false;
                m_DirectionMoving = Random.insideUnitCircle;
            }
        } else {
            if(m_PlayerReference.IsMoving() && Vector3.Distance(transform.position, m_PlayerReference.transform.position) <= DistanceToDetectPlayer) {
                m_bIsChasingPlayer = true;
            }
        }

        transform.Translate(m_DirectionMoving * Time.deltaTime * MoveSpeed);
    }
}
