using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Improx.TLC.Localization
{
    [CreateAssetMenu(fileName = "ArrayPluggable", menuName = "Localization/ArrayPluggable", order = 0)]
    public class ArrayPluggable : TextPluggableBase
    {
        [SerializeField]
        private List<TextPluggableBase> _contentList;

        public override void RegisterDependencies(Action Reparse)
        {
            foreach (var item in _contentList)
            {
                item.RegisterDependencies(Reparse);
            }
        }

        public override void UnregisterDependencies()
        {
            foreach (var item in _contentList)
            {
                item.UnregisterDependencies();
            }
        }

        public override string GetText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _contentList)
            {
                sb.Append(item.GetText());
            }
            return sb.ToString();
        }
    }
}