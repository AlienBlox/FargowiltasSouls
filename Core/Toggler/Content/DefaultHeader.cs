// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.Content.DefaultHeader
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.UI.Elements;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Toggler.Content
{
  public class DefaultHeader : Header
  {
    public override int Item => ModContent.ItemType<TogglerIconItem>();

    public override float Priority => -1E+10f;

    public override string SortCategory => "Enchantments";
  }
}
