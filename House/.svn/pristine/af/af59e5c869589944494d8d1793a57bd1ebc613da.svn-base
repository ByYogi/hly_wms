using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace Cargo
{
    /// <summary>
    /// JSON序列化和反序列化
    /// </summary>
    public class JSON
    {
        public static string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        public static string GtmcDateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
        public static string EncodeDateTime(object o)
        {
            if (o == null || o.ToString() == "null") return null;

            if (o != null && (o.GetType() == typeof(String) || o.GetType() == typeof(string)))
            {
                return o.ToString();
            }
            IsoDateTimeConverter dt = new IsoDateTimeConverter();
            dt.DateTimeFormat = GtmcDateTimeFormat;
            return JsonConvert.SerializeObject(o, dt);
        }
        public static string Encode(object o)
        {
            if (o == null || o.ToString() == "null") return null;

            if (o != null && (o.GetType() == typeof(String) || o.GetType() == typeof(string)))
            {
                return o.ToString();
            }
            IsoDateTimeConverter dt = new IsoDateTimeConverter();
            dt.DateTimeFormat = DateTimeFormat;
            return JsonConvert.SerializeObject(o, dt);
        }

        public static object Decode(string json)
        {
            if (String.IsNullOrEmpty(json)) return "";
            object o = JsonConvert.DeserializeObject(json);
            if (o.GetType() == typeof(String) || o.GetType() == typeof(string))
            {
                o = JsonConvert.DeserializeObject(o.ToString());
            }
            object v = toObject(o);
            return v;
        }
        public static object Decode(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        private static object toObject(object o)
        {
            if (o == null) return null;

            if (o.GetType() == typeof(string))
            {
                //判断是否符合2010-09-02T10:00:00的格式
                string s = o.ToString();
                if (s.Length == 19 && s[10] == 'T' && s[4] == '-' && s[13] == ':')
                {
                    o = System.Convert.ToDateTime(o);
                }
            }
            else if (o is JObject)
            {
                JObject jo = o as JObject;

                Hashtable h = new Hashtable();

                foreach (KeyValuePair<string, JToken> entry in jo)
                {
                    h[entry.Key] = toObject(entry.Value);
                }

                o = h;
            }
            else if (o is IList)
            {

                ArrayList list = new ArrayList();
                list.AddRange((o as IList));
                int i = 0, l = list.Count;
                for (; i < l; i++)
                {
                    list[i] = toObject(list[i]);
                }
                o = list;

            }
            else if (typeof(JValue) == o.GetType())
            {
                JValue v = (JValue)o;
                o = toObject(v.Value);
            }
            else
            {
            }
            return o;
        }


        public static ArrayList ToTree(ArrayList table, string childrenField, string idField, string parentIdField)
        {
            ArrayList tree = new ArrayList();
            //建立快速索引
            Hashtable hash = new Hashtable();
            for (int i = 0, l = table.Count; i < l; i++)
            {
                Hashtable t = (Hashtable)table[i];
                hash[t[idField]] = t;
            }
            //数组转树形        
            for (int i = 0, l = table.Count; i < l; i++)
            {
                Hashtable t = (Hashtable)table[i];
                object parentID = t[parentIdField];
                if (parentID == null || parentID.ToString() == "-1")   //如果没有父节点, 是第一层
                {
                    tree.Add(t);

                    continue;
                }
                Hashtable parent = (Hashtable)hash[parentID];
                if (parent == null)     //如果没有父节点, 是第一层
                {
                    tree.Add(t);
                    continue;
                }
                ArrayList children = (ArrayList)parent[childrenField];
                if (children == null)
                {
                    children = new ArrayList();
                    parent[childrenField] = children;
                }
                children.Add(t);
            }

            //创建树形后, 遍历树形, 生成OuterLineNumber体现树形结构
            SyncTreeNodes(tree, 1, "", childrenField);

            return tree;
        }
        private static void SyncTreeNodes(ArrayList nodes, int outlineLevel, String outlineNumber, string childrenField)
        {

            for (int i = 0, l = nodes.Count; i < l; i++)
            {
                Hashtable node = nodes[i] as Hashtable;

                node["OutlineLevel"] = outlineLevel;
                node["OutlineNumber"] = outlineNumber + (i + 1);

                ArrayList childNodes = (ArrayList)node[childrenField];

                if (childNodes != null && childNodes.Count > 0)
                {
                    SyncTreeNodes(childNodes, outlineLevel + 1, node["OutlineNumber"].ToString() + ".", childrenField);
                }
            }
        }

        public static ArrayList ToList(ArrayList tree, string parentId, string childrenField, string idField, string parentIdField)
        {
            ArrayList list = new ArrayList();
            for (int i = 0, len = tree.Count; i < len; i++)
            {
                Hashtable task = (Hashtable)tree[i];

                task[parentIdField] = parentId;

                list.Add(task);

                ArrayList children = (ArrayList)task[childrenField];

                if (children != null && children.Count > 0)
                {
                    ArrayList list2 = ToList(children, task[idField].ToString(), childrenField, idField, parentIdField);
                    list.AddRange(list2);


                }
                task.Remove(childrenField);
            }
            return list;
        }
    }
}