// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.PearlwoodEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class PearlwoodEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TimberHeader>();

    public override int ToggleItemType => ModContent.ItemType<PearlwoodEnchant>();

    public override void PostUpdateEquips(Player player)
    {
      PearlwoodEffect.PearlwoodStar(player, this.EffectItem(player));
    }

    public static void PearlwoodStar(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.PearlwoodStar)
        return;
      player.AddBuff(ModContent.BuffType<PearlwoodStarBuff>(), 2, true, false);
      if (Vector2.op_Inequality(fargoSoulsPlayer.PearlwoodTrail[fargoSoulsPlayer.PearlwoodIndex], Vector2.Zero))
      {
        fargoSoulsPlayer.PStarelinePos = fargoSoulsPlayer.PearlwoodTrail[fargoSoulsPlayer.PearlwoodIndex];
        if (!fargoSoulsPlayer.PStarelineActive)
        {
          if (fargoSoulsPlayer.PearlwoodGrace == 120)
          {
            Projectile.NewProjectile(player.GetSource_EffectItem<PearlwoodEffect>(), fargoSoulsPlayer.PStarelinePos, Vector2.Zero, 931, 1000, 0.0f, -1, 0.0f, 0.0f, 0.0f);
            fargoSoulsPlayer.PearlwoodGrace = 0;
          }
          else if ((double) ((Vector2) ref ((Entity) player).velocity).Length() > 0.0)
            ++fargoSoulsPlayer.PearlwoodGrace;
        }
      }
      fargoSoulsPlayer.PearlwoodTrail[fargoSoulsPlayer.PearlwoodIndex] = ((Entity) player).Center;
      ++fargoSoulsPlayer.PearlwoodIndex;
      if (fargoSoulsPlayer.PearlwoodIndex < fargoSoulsPlayer.PearlwoodTrail.Length)
        return;
      fargoSoulsPlayer.PearlwoodIndex = 0;
    }

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      if (!player.FargoSouls().PStarelineActive || !hitInfo.Crit)
        return;
      SoundEngine.PlaySound(ref SoundID.Item25, new Vector2?(((Entity) target).position), (SoundUpdateCallback) null);
      for (int index = 0; index < 30; ++index)
        Dust.NewDust(((Entity) target).position, ((Entity) target).width, ((Entity) target).height, 292, 0.0f, 0.0f, 0, new Color(), 1f);
    }

    public override void ModifyHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      PearlwoodEffect.PearlwoodCritReroll(player, ref modifiers, proj.DamageType);
    }

    public override void ModifyHitNPCWithItem(
      Player player,
      Item item,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      PearlwoodEffect.PearlwoodCritReroll(player, ref modifiers, item.DamageType);
    }

    public static void PearlwoodCritReroll(
      Player player,
      ref NPC.HitModifiers modifiers,
      DamageClass damageClass)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.PStarelineActive)
        return;
      int num = fargoSoulsPlayer.ForceEffect<PearlwoodEnchant>() ? 2 : 1;
      for (int index = 0; index < num; ++index)
      {
        if ((double) Main.rand.Next(0, 100) <= (double) player.ActualClassCrit(damageClass))
          ((NPC.HitModifiers) ref modifiers).SetCrit();
      }
    }
  }
}
