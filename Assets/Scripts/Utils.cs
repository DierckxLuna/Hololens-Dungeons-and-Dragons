using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Utils
    {
        public static T GetRandom<T>(this IEnumerable<T> list) => list.ElementAt(Random.Range(0, list.Count()));
    }
}