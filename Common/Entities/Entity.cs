using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    [System.SerializableAttribute]
    public abstract class Entity : IKeyable
    {

        public static string Indent = "    ";

        public int Id { get; set; }

        public abstract string ToMembersString();

        public override string ToString()
        {
            return string.Format($"{Indent} {Id} {ToMembersString()}");
        }

        public virtual string Key
        {
            get { return string.Format($"{this.GetType().Name} {Id}"); }
        }

    }
}
