using UnityEngine;
using UniRx;
using MVP.Framework.Core;
using MVP.Framework.Resources;

namespace Components.Framework
{
    using Context   = MVP.Framework.Views.Context;
    using CELL_LIST = System.Collections.Generic.Dictionary<int, LinkData>;
    using LIST_INFO = Presenters.Framework.LIST_INFO;

    public class List : MVP.Framework.Component
    {
        [MVP.Framework.Views.Inspector]
        public GameObject     cellPrefab;
        [MVP.Framework.Views.Inspector]
        public string         cellPath;
        public CELL_LIST      list; 

        public override void Create(Context context)
        {
            base.Create(context);
            list = new CELL_LIST();

            Subscribe();
        }

        protected override void Dispose()
        {
            foreach (var cell in list)
            {
                cell.Value.manager.Close(cell.Value);
            }
            list.Clear();

            base.Dispose();
        }

        private void Subscribe()
        {
            context.state.GetObservable("data")
                .Select(x => x.RefValue<LIST_INFO>())
                .Subscribe(x =>
            {
                UpdateList(x);
            });
        }

        private void UpdateList(LIST_INFO listInfo)
        {
            foreach (var cell in list)
            {
                cell.Value.node.SetActive(false);
            }

            int i = 0;
            foreach (var data in listInfo.list)
            {
                var cell = Get(i);
                cell.node.SetActive(true);
                listInfo.action?.Invoke(cell.presenter, data);
                ++i;
            }
        }

        private LinkData Get(int index)
        {
            if (list.ContainsKey(index))
            {
                return list[index];
            }

            var cellPrefabRef = new AssetRef(cellPath, cellPrefab);
            var presenter = context.presenter as Presenters.Framework.List;
            var data = presenter.Build(cellPrefabRef, cellPath);
            data.node.transform.SetParent(context.gameObject.transform);
            list.Add(index, data);
            return data;
        }
    }
}