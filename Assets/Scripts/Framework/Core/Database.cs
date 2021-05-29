using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Framework.Core
{
    class DatabaseInfo<T>
    {
        public string key { get; private set; }
        public T[] records { get; private set; }
    }

    public class Database
    {
        Dictionary<string, object> tables;
        public string[] TableNames { get; private set; }

        public Database()
        {
            this.tables = new Dictionary<string, object>();
        }

        public async void LoadTables()
        {
            await LoadTablesAsync();
        }

        public async Task LoadTablesAsync()
        {
            var jsonData = UnityEngine.Resources.Load("Database/config") as TextAsset;
            TableNames = await Json.FromString<string[]>(jsonData.text);
        }

        public async void Load<T>(string name)
        {
            await LoadAsync<T>(name);
        }

        public async Task<T> LoadAsync<T>(string name)
        {
            if (tables.ContainsKey(name)) return (T)tables[name];

            var jsonData = UnityEngine.Resources.Load("Database/"+name) as TextAsset;
            var table = await Json.FromString<T>(jsonData.text);
            tables.Add(name, table);
            return table;
        }

        public T Get<T, TKey>(string name, TKey value, Func<TKey, TKey, bool> compare, string field = null)
        {
            if (!tables.ContainsKey(name)) return default(T);
            var table = tables[name] as DatabaseInfo<T>;
            foreach (var record in table.records)
            {
                TKey fieldValue = (TKey)record.GetType().GetProperty(field ?? table.key).GetValue(record);
                if (compare(fieldValue, value))
                {
                    return record;
                }
            }

            return default(T);
        }

        public T[] GetAll<T>(string name)
        {
            if (!tables.ContainsKey(name)) return null;
            return (tables[name] as DatabaseInfo<T>)?.records;
        }

        public T[] GetWithPredicate<T>(string name, Func<T, bool> predicate)
        {
            if (!tables.ContainsKey(name)) return null;
            var table = tables[name] as DatabaseInfo<T>;
            if (table == null) return default(T[]);

            var records = from record in table.records
                          where predicate(record)
                          select record;
            return records.ToArray();
        }

        public int GetRecordCount<T>(string name)
        {
            if (!tables.ContainsKey(name)) return 0;
            var table = tables[name] as DatabaseInfo<T>;
            if (table == null) return 0;

            return table.records.Length;
        }
    }
}
