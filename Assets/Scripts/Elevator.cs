using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Elevator : MonoBehaviour
    {
        public List<GameObject> Points;
        private int index = 0;
        private List<GameObject> objectsOn;

        public int Speed = 5;

        private void Start()
        {
            objectsOn = new List<GameObject>();
            transform.position = Points.First().GetComponent<Transform>().position;
        }

        private void FixedUpdate()
        {
            Vector3 value =  Vector3.MoveTowards(transform.position, Points[index].transform.position, Speed * Time.deltaTime);

            if (value == transform.position)
                return;

            transform.position = value;

            foreach (GameObject item in objectsOn)
            {
                item.transform.position = Vector3.MoveTowards(item.transform.position, Points[index].transform.position, Speed * Time.deltaTime);
            }
        }

        public void Trigger()
        {
            index++;
            if (index >= Points.Count)
            {
               index = 0;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            print("Enter");
            objectsOn.Add(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            print("Exit");
            objectsOn.Remove(collision.gameObject);
        }
    }
}
