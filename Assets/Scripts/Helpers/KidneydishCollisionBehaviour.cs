/*********************************************************
 * Property of USC ICT                                   *
 * Distributed under MIT Licence                         *
 *********************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenMg.SmartObjects
{
    public class KidneydishCollisionBehaviour : MonoBehaviour
    {
        public AudioSource collideKidneydishAudio;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Kidneydish")
            {
                collideKidneydishAudio.Play();
            }
        }
    }
}
