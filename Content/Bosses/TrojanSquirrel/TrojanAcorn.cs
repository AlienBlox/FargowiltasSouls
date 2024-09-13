// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanAcorn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Timber;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.TrojanSquirrel
{
  public class TrojanAcorn : TimberAcorn
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Champions/Timber/TimberAcorn";

    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.tileCollide = true;
      this.CooldownSlot = -1;
    }

    public override void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 10; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 7, 0.0f, 0.0f, 0, new Color(), 1f);
    }
  }
}
