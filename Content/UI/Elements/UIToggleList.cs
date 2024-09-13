// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.UI.Elements.UIToggleList
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.UI.Elements
{
  public class UIToggleList : UIList
  {
    public static readonly FieldInfo field_innerList = typeof (UIList).GetField("_innerList", (BindingFlags) 36);
    public static readonly MethodInfo method_uiElementAppend = typeof (UIElement).GetMethod("Append", (BindingFlags) 20);
    public static readonly MethodInfo method_uiElementRecalcuate = typeof (UIElement).GetMethod("Recalculate", (BindingFlags) 20);
    public static readonly MethodInfo method_uiElementRemoveChild = typeof (UIElement).GetMethod("RemoveChild", (BindingFlags) 20);

    public virtual void Add(UIElement item)
    {
      this._items.Add(item);
      ((MethodBase) UIToggleList.method_uiElementAppend).Invoke(UIToggleList.field_innerList.GetValue((object) this), new object[1]
      {
        (object) item
      });
      ((MethodBase) UIToggleList.method_uiElementRecalcuate).Invoke(UIToggleList.field_innerList.GetValue((object) this), (object[]) null);
    }

    public virtual bool Remove(UIElement item)
    {
      ((MethodBase) UIToggleList.method_uiElementRemoveChild).Invoke(UIToggleList.field_innerList.GetValue((object) this), new object[1]
      {
        (object) item
      });
      return this._items.Remove(item);
    }
  }
}
