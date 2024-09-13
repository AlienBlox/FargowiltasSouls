// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberJumpMark
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  public class TimberJumpMark : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = -1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<TimberChampion>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) ++this.Projectile.localAI[0] <= 4.0)
          return;
        this.Projectile.localAI[0] = 0.0f;
        for (int index = -1; index <= 1; index += 2)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(this.Projectile.ai[1] * this.Projectile.localAI[1] * (float) index, Vector2.UnitX));
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(vector2), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile projectile = Projectile.NewProjectileDirect(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, 696, this.Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            projectile.friendly = false;
            projectile.hostile = true;
          }
        }
        if ((double) ++this.Projectile.localAI[1] <= (WorldSavingSystem.MasochistModeReal ? 18.0 : 6.0))
          return;
        this.Projectile.Kill();
      }
    }
  }
}
