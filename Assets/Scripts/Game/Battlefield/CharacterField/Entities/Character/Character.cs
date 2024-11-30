using Game.Battlefield.Pawnfields.Teams;
using System;
using UnityEngine;

namespace Game.Battlefield.Pawnfields
{
    public class Character
    {
        public enum OwnerType { Player, PC }

        public Transform View { get; }
        public Team Team { get; set; }
        public OwnerType Owner { get; set; }
        public Action NextTurn;

        private Vector3[] path;
        private int index;

        public Character(Transform view)
        {
            View = view;
        }

        public void Start()
        {
            index = 0;
            path = null;
        }

        public void Update()
        {
            if (path != null)
            {
                Run();
            }
        }

        public void MoveTo(Vector3[] path)
        {
            this.path = path;
        }

        private void Run()
        {
            if (index < path.Length)
            {
                View.position = Vector3.MoveTowards(View.position, path[index], 3 * Time.deltaTime);

                if (View.position == path[index])
                {
                    index++;
                }
            }
            else NextTurn.Invoke();
        }
    }
}
