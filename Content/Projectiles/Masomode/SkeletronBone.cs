// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SkeletronBone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class SkeletronBone : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_471";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(471);
      this.AIType = 471;
      this.Projectile.light = 1f;
      this.Projectile.scale = 1.5f;
      this.Projectile.timeLeft = 240;
      this.Projectile.tileCollide = false;
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.guardBoss, 68) && (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.skeleBoss, 35) || (double) Main.npc[EModeGlobalNPC.skeleBoss].ai[1] != 2.0))
        return;
      this.CooldownSlot = 1;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!SkeletronBone.SourceIsSkeletron(source))
        return;
      this.Projectile.ai[0] = 1f;
      this.Projectile.netUpdate = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.guardBoss, 68))
        target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture = ((double) this.Projectile.ai[0] != 1.0 || !SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/Masomode/SkeletronBone_Recolor", (AssetRequestMode) 2).Value : TextureAssets.Projectile[this.Type].Value;
      FargoSoulsUtil.ProjectileWithTrailDraw(this.Projectile, Color.op_Multiply(Color.White, this.Projectile.Opacity), texture, additiveTrail: true);
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor, texture);
      return false;
    }

    public static bool SourceIsSkeletron(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is NPC entity))
        return false;
      return entity.type == 35 || entity.type == 36 || entity.type == 68;
    }
  }
}
