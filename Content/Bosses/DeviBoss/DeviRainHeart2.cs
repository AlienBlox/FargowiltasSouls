// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviRainHeart2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviRainHeart2 : DeviRainHeart
  {
    public override string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/FakeHeart";

    public override void OnKill(int timeLeft)
    {
      base.OnKill(timeLeft);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc == null || !FargoSoulsUtil.HostCheck)
        return;
      if (WorldSavingSystem.MasochistModeReal)
      {
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<DeviDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        if ((double) ((Entity) Main.player[npc.target]).Center.Y <= (double) ((Entity) this.Projectile).Center.Y)
          return;
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.UnitY, ModContent.ProjectileType<DeviDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 5; ++index1)
        {
          float num = (float) (4.0 + (double) index1 * 8.0);
          int index2 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), num), ModContent.ProjectileType<DeviHeart>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          if (index2 != Main.maxProjectiles)
            Main.projectile[index2].timeLeft = 20;
          if ((double) ((Entity) Main.player[npc.target]).Center.Y > (double) ((Entity) this.Projectile).Center.Y)
          {
            int index3 = Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.UnitY, num), ModContent.ProjectileType<DeviHeart>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
            if (index3 != Main.maxProjectiles)
              Main.projectile[index3].timeLeft = 20;
          }
        }
      }
    }
  }
}
