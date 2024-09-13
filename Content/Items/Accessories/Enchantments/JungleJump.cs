// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.JungleJump
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class JungleJump : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<JungleEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.grapCount > 0)
      {
        fargoSoulsPlayer.CanJungleJump = true;
        fargoSoulsPlayer.JungleJumping = false;
      }
      else if (player.controlJump && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.BlizzardInABottle)).Available && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.SandstormInABottle)).Available && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.CloudInABottle)).Available && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Available && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.TsunamiInABottle)).Available && !((ExtraJumpState) ref player.GetJumpState<ExtraJump>(ExtraJump.UnicornMount)).Available && player.jump == 0 && player.releaseJump && (double) ((Entity) player).velocity.Y != 0.0 && !player.mount.Active && fargoSoulsPlayer.CanJungleJump)
      {
        player.jump = (int) ((double) Player.jumpHeight * 3.0);
        fargoSoulsPlayer.JungleJumping = true;
        fargoSoulsPlayer.JungleCD = 0;
        fargoSoulsPlayer.CanJungleJump = false;
        if (Main.netMode == 1)
          NetMessage.SendData(13, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      if (fargoSoulsPlayer.JungleJumping)
      {
        if (player.rocketBoots > 0)
        {
          fargoSoulsPlayer.savedRocketTime = player.rocketTimeMax;
          player.rocketTime = 0;
        }
        player.runAcceleration *= 3f;
        if (fargoSoulsPlayer.JungleCD == 0)
        {
          int num = 1;
          if (fargoSoulsPlayer.ChlorophyteEnchantActive)
            ++num;
          if (fargoSoulsPlayer.ForceEffect<JungleEnchant>())
            ++num;
          fargoSoulsPlayer.JungleCD = 18 - num * num;
          int dmg = 12 * num * num - 5;
          SoundStyle soundStyle = SoundID.Item62;
          ((SoundStyle) ref soundStyle).Volume = 0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          if (((Entity) player).whoAmI == Main.myPlayer)
          {
            foreach (Projectile projectile in FargoSoulsUtil.XWay(10, this.GetSource_EffectItem(player), ((Entity) player).Bottom, 228, 4f, FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.0f))
            {
              if (projectile != null)
              {
                projectile.usesIDStaticNPCImmunity = true;
                projectile.idStaticNPCHitCooldown = 10;
                projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
                ++projectile.extraUpdates;
              }
            }
          }
        }
        if (player.jump == 0 || Vector2.op_Equality(((Entity) player).velocity, Vector2.Zero))
        {
          fargoSoulsPlayer.JungleJumping = false;
          player.rocketTime = fargoSoulsPlayer.savedRocketTime;
        }
      }
      else if (player.jump <= 0 && (double) ((Entity) player).velocity.Y == 0.0)
        fargoSoulsPlayer.CanJungleJump = true;
      if (fargoSoulsPlayer.JungleCD == 0)
        return;
      --fargoSoulsPlayer.JungleCD;
    }
  }
}
