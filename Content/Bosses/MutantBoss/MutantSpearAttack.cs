// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSpearAttack
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
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public abstract class MutantSpearAttack : ModProjectile
  {
    protected NPC npc;

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?();
    }

    protected void TryLifeSteal(Vector2 pos, int playerWhoAmI)
    {
      if (!WorldSavingSystem.MasochistModeReal || this.npc == null)
        return;
      int num1 = this.npc.lifeMax / 100 * 5;
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 2f, 9f), Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 6.2831854820251465)));
        float whoAmI = (float) ((Entity) this.npc).whoAmI;
        float num2 = ((Vector2) ref vector2).Length() / (float) Main.rand.Next(30, 90);
        int num3 = (int) ((double) (num1 / 20) * (double) Utils.NextFloat(Main.rand, 0.95f, 1.05f));
        if (playerWhoAmI == Main.myPlayer && Main.player[playerWhoAmI].ownedProjectileCounts[ModContent.ProjectileType<MutantHeal>()] < 10)
        {
          Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), pos, vector2, ModContent.ProjectileType<MutantHeal>(), num3, 0.0f, Main.myPlayer, whoAmI, num2, 0.0f);
          SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(pos), (SoundUpdateCallback) null);
        }
      }
    }
  }
}
