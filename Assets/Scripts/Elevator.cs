using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Elevator : MonoBehaviour
    {
        public int Speed = 5;
        public List<GameObject> Points;

        private Rigidbody body;
        private int index = 0;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
            body.position = Points.First().GetComponent<Transform>().position;
        }

        private void FixedUpdate()
        {
            Vector3 value =  Vector3.MoveTowards(transform.position, Points[index].transform.position, Speed * Time.deltaTime);

            if (value == transform.position)
                return;

            body.MovePosition(value);
        }

        public void Trigger()
        {
            index++;
            if (index >= Points.Count)
            {
               index = 0;
            }
        }
    }
}
