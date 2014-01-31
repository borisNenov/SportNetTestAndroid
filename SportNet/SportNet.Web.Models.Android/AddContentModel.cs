using System;
using System.Collections.Generic;
using System.Text;


namespace SportNet.Web.Models
{
	public class AddContentModel 
    {
        public CategoryDTO ParentCategory { get; set; }
        public List<AddContentItem> Categories { get; set; }
        public List<CategoryDTO> UserCategories { get; set; }
    }
    public class AddContentItem : CategoryDTO    
    {
        public AddContentItem() { }
        public AddContentItem(CategoryDTO dto)
        {
            this.fkParent = dto.fkParent;
            this.Id = dto.Id;
            this.MenuIndex = dto.MenuIndex;
            this.Name = dto.Name;
            this.ShowInMenu = dto.ShowInMenu;
            this.Thumb = dto.Thumb;            
        }
        public bool Checked { get; set; }
    }
}
