// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Shadow.ShadowGuardian
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Shadow
{
  public class ShadowGuardian : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/NPC_68";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.npcFrameCount[68];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 70;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.timeLeft = 600;
      this.Projectile.hide = true;
      this.Projectile.light = 0.5f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 0.0f, 6.28318548f);
        this.Projectile.hide = false;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].noGravity = true;
        }
      }
      ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X < 0.0 ? -1 : 1;
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * 0.3f;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
      if (Main.dedServ)
        return;
      Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).velocity, 3f), ModContent.Find<ModGore>(((ModType) this).Mod.Name, Utils.NextBool(Main.rand) ? "Gore_54" : "Gore_55").Type, this.Projectile.scale);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(22, 300, true, false);
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
        target.AddBuff(80, 300, true, false);
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
      }
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.guardBoss, 68))
        return;
      target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = TextureAssets.Npc[68].Value;
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }
  }
}
