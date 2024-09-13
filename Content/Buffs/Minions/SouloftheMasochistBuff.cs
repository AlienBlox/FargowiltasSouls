// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Buffs.Minions.SouloftheMasochistBuff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Buffs.Minions
{
  public class SouloftheMasochistBuff : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoTimeDisplay[this.Type] = true;
      Main.buffNoSave[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      Item obj = (Item) null;
      if (player.AddEffect<SkeleMinionEffect>(obj))
      {
        fargoSoulsPlayer.SkeletronArms = true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<SkeletronArmL>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<SkeletronArmL>(), 64, 8f, ((Entity) player).whoAmI);
        if (player.ownedProjectileCounts[ModContent.ProjectileType<SkeletronArmR>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<SkeletronArmR>(), 64, 8f, ((Entity) player).whoAmI);
      }
      if (player.AddEffect<PungentMinion>(obj))
      {
        fargoSoulsPlayer.PungentEyeballMinion = true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<PungentEyeballMinion>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<PungentEyeballMinion>(), 150, 0.0f, ((Entity) player).whoAmI);
      }
      if (player.AddEffect<RainbowSlimeMinion>(obj))
      {
        fargoSoulsPlayer.RainbowSlime = true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<RainbowSlime>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<RainbowSlime>(), 105, 3f, ((Entity) player).whoAmI);
      }
      if (player.AddEffect<ProbeMinionEffect>(obj))
      {
        fargoSoulsPlayer.Probes = true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<Probe1>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<Probe1>(), 105, 9f, ((Entity) player).whoAmI);
        if (player.ownedProjectileCounts[ModContent.ProjectileType<Probe2>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<Probe2>(), 105, 9f, ((Entity) player).whoAmI, ai1: -1f);
      }
      if (player.AddEffect<PlantMinionEffect>(obj))
      {
        fargoSoulsPlayer.PlanterasChild = true;
        if (((Entity) player).whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<PlanterasChild>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<PlanterasChild>(), 120, 3f, ((Entity) player).whoAmI);
      }
      if (player.AddEffect<UfoMinionEffect>(obj))
      {
        fargoSoulsPlayer.MiniSaucer = true;
        if (player.ownedProjectileCounts[ModContent.ProjectileType<MiniSaucer>()] < 1)
          FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<MiniSaucer>(), 100, 3f, ((Entity) player).whoAmI);
      }
      if (!player.AddEffect<MasoTrueEyeMinion>(obj))
        return;
      fargoSoulsPlayer.TrueEyes = true;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeL>()] < 1)
        FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeL>(), 180, 3f, ((Entity) player).whoAmI, -1f);
      if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeR>()] < 1)
        FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeR>(), 180, 3f, ((Entity) player).whoAmI, -1f);
      if (player.ownedProjectileCounts[ModContent.ProjectileType<TrueEyeS>()] >= 1)
        return;
      FargoSoulsUtil.NewSummonProjectile(player.GetSource_Buff(buffIndex), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<TrueEyeS>(), 180, 3f, ((Entity) player).whoAmI, -1f);
    }
  }
}
