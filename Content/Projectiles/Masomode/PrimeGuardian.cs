// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.PrimeGuardian
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class PrimeGuardian : MutantGuardian
  {
    public override string Texture => "Terraria/Images/NPC_127";

    public override void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[(int) sbyte.MaxValue];
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 600;
      this.CooldownSlot = -1;
    }

    public override bool CanHitPlayer(Player target) => true;

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 0.0f, 6.28318548f);
        this.Projectile.hide = false;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
        }
      }
      this.Projectile.frame = 5;
      ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.3f;
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<NanoInjectionBuff>(), 480, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 480, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 480, true, false);
    }

    public override void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
      if (Main.dedServ)
        return;
      Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 3f), ModContent.Find<ModGore>(((ModType) this).Mod.Name, Utils.NextBool(Main.rand) ? "Gore_149" : "Gore_150").Type, this.Projectile.scale);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = TextureAssets.Npc[(int) sbyte.MaxValue].Value;
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }
  }
}
