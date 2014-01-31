using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? fkParent { get; set; }

        public bool ShowInMenu { get; set; }

        public bool HasChildren { get; set; }

        public short MenuIndex { get; set; }

        public string Thumb { get; set; }
    }
}
