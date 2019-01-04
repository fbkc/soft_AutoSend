using System;
using System.Collections.Generic;
using System.Text;

namespace AutoSend
{
    public class Category1
    {
        public List<Category1> Children { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public Category1 Parent { get; set; }
    }
}
