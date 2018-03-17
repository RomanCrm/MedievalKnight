using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
	Animator animator;
	NavMeshAgent nav;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 500) && Input.GetMouseButtonDown(1))
		{
			nav.SetDestination (hit.point);
			animator.SetBool ("IsWalk", true);
			nav.isStopped = false;
		}
		if (Vector3.Distance(transform.position, nav.destination) < 0.1f)
		{
			nav.isStopped = true;
			animator.SetBool ("IsWalk", false);
		}
	}
}
