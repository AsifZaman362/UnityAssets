using UnityEngine;
using UnityEngine.Events;

namespace Items
{

    public class Item
    {

        public string name = "";
        public int amount = 0;
        public Sprite m_icon;
        public bool isUsable;
        public UnityEvent useEvent;

        public Item(string pname, int pamount, Sprite picon)
        {
            name = pname;
            amount = pamount;
            m_icon = picon;
        }

    }

}
