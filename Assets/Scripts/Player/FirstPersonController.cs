using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Controller
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] float playerSpeed;

        private Vector3 playerVelocity;

        void Update()
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);
        }
    }
}
