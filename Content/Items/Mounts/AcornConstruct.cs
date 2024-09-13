// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Mounts.AcornConstruct
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Mounts
{
  public class AcornConstruct : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 30;
      this.Item.useTime = 20;
      this.Item.useAnimation = 20;
      this.Item.useStyle = 1;
      this.Item.value = 30000;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item79);
      this.Item.noMelee = true;
      this.Item.mountType = ModContent.MountType<TrojanSquirrelMount>();
    }
  }
}
