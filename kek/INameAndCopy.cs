using System;
using System.Collections.Generic;
using System.Text;

namespace kek
{
    public interface INameAndCopy
    {
        string Name { get; set; }
        public object DeepCopy(object originalObject);
    }
}
