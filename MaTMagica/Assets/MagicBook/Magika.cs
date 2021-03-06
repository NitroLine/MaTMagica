using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MAGIC
{
    public class Magika
    {
        public string Name;
        public GameObject Obj;
        public int Power;

        public Magika(GameObject obj,string name, int power = 1)
        {
            Obj = obj;
            Power = power;
            Name = name;
        }
    }

    public enum Rune
    {
        Ice,
        Stone,
        Wind,
        Leaf,
        Shield,
    }

    public class KeyCombination
    {
        public readonly List<KeyCode> Combinations;

        public KeyCombination(params KeyCode[] combination)
        {
            Combinations = combination.ToList();
        }
        public KeyCombination(List<KeyCode> combination)
        {
            Combinations = combination;
        }

        public override int GetHashCode()
        {
            return Combinations
                .OrderBy(x => x)
                .Aggregate(0, (current, i) => (current + (int) i) * 307);
        }

        public override bool Equals(object obj)
        {
            var other = obj as KeyCombination;
            if (other == null || Combinations.Count != other.Combinations.Count)
                return false;
            other.Combinations.Sort();
            return !Combinations.OrderBy(x=>x).Where((t, i) => t != other.Combinations[i]).Any();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var x in Combinations)
            {
                sb.Append(x);
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }

}
