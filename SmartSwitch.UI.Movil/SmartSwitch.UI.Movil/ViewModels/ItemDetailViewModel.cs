using System;

using SmartSwitch.UI.Movil.Models;

namespace SmartSwitch.UI.Movil.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
