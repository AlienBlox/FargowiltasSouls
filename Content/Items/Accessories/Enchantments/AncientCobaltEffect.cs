// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AncientCobaltEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AncientCobaltEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<EarthHeader>();

    public override int ToggleItemType => ModContent.ItemType<AncientCobaltEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.CobaltImmuneTimer > 0)
      {
        player.immune = true;
        --fargoSoulsPlayer.CobaltImmuneTimer;
      }
      if (fargoSoulsPlayer.CobaltCooldownTimer > 0)
        --fargoSoulsPlayer.CobaltCooldownTimer;
      if (player.jump <= 0 && (double) ((Entity) player).velocity.Y == 0.0)
      {
        fargoSoulsPlayer.CanCobaltJump = true;
        fargoSoulsPlayer.JustCobaltJumped = false;
      }
      else
        fargoSoulsPlayer.CanCobaltJump = false;
      if (player.controlJump && player.releaseJump && fargoSoulsPlayer.CanCobaltJump && !fargoSoulsPlayer.JustCobaltJumped && fargoSoulsPlayer.CobaltCooldownTimer <= 0)
      {
        int num1 = ModContent.ProjectileType<CobaltExplosion>();
        int num2 = 100;
        if (player.HasEffect<CobaltEffect>())
          num2 = 250;
        Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<AncientCobaltEffect>(), (string) null), ((Entity) player).Center, Vector2.Zero, num1, num2, 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        fargoSoulsPlayer.JustCobaltJumped = true;
        if (fargoSoulsPlayer.CobaltImmuneTimer <= 0)
          fargoSoulsPlayer.CobaltImmuneTimer = 15;
        if (fargoSoulsPlayer.CobaltCooldownTimer <= 10)
          fargoSoulsPlayer.CobaltCooldownTimer = 10;
      }
      if (!fargoSoulsPlayer.CanCobaltJump && (!fargoSoulsPlayer.JustCobaltJumped || ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.CloudInABottle)).Active || ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.BlizzardInABottle)).Active || ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Active || ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.TsunamiInABottle)).Active || ((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.SandstormInABottle)).Active || fargoSoulsPlayer.JungleJumping))
        return;
      if (player.HasEffect<CobaltEffect>())
        player.jumpSpeedBoost += 10f;
      else
        player.jumpSpeedBoost += 5f;
    }
  }
}
