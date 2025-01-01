using System;
using _Game.Scripts.Infrastructure;
using UnityEngine;

namespace _Game.Scripts.Character
{
    public class AdventurerController : EntityController
    {
        private void Start()
        {
            MoveToPosition(new Vector2(40, 0));
        }
    }
}
