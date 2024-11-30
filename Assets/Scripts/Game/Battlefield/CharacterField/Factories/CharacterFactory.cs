using Game.Battlefield.Pawnfields;
using UnityEngine;

namespace Game.Gameplay.Pawnfields.Factories
{
    public class CharacterFactory
    {
        public Character Create(CharacterConfig factoryConfig)
        {
            var obj = Object.Instantiate(factoryConfig.ProfessionConfig.Prefab);
            obj.gameObject.SetActive(false);
            obj.GetComponent<SpriteRenderer>().sprite = factoryConfig.ProfessionConfig.View;
            return new Character(obj.transform);
        }
    }
}