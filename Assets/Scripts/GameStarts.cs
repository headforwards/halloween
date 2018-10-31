using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarts : MonoBehaviour {

    Animator Animator;
	// Use this for initialization
	void Start () {
        Animator = GetComponent<Animator>();

        Animator.Play("StartGame");
	}
}
