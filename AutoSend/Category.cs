using System;
using System.Collections.Generic;

using System.Text;

namespace AutoSend
{
    public class Category
    {
        public int Id { get; set; }
        public string ChsName { get; set; }
        public string EngName { get; set; }
        public string Initial { get; set; }
        public string IsEnd { get; set; }
        public string PinYin { get; set; }
        public string PinYinJian { get; set; }
        public int LevelId { get; set; }
        public int OrderNum { get; set; }
        public int ParentId { get; set; }
        public int ReferenceId { get; set; }
    }
}
