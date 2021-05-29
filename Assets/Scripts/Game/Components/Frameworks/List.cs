using UnityEngine;
using UniRx;
using Framework.Core;
using Framework.Resources;

namespace Game.Components.Frameworks
{
    using Context   = Framework.Views.Context;
    using CELL_LIST = System.Collections.Generic.Dictionary<int, LinkData>;
    using LIST_INFO = Presenters.Frameworks.LIST_INFO;

    public class List : Framework.Component
    {
        [Framework.Views.Inspector]
        public GameObject     CellPrefab;

        [Framework.Views.Inspector]
        public string         CellPath;

        private CELL_LIST     list; 

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
                .Select(x => x.RefOf<LIST_INFO>())
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

            var cellPrefabRef = new AssetRef(CellPath, CellPrefab);
            var presenter = context.presenter as Presenters.Frameworks.List;
            var data = presenter.Build(cellPrefabRef, CellPath);
            data.node.transform.SetParent(context.gameObject.transform);
            list.Add(index, data);
            return data;
        }
    }
}
