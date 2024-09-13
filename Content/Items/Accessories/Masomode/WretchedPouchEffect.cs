// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.WretchedPouchEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class WretchedPouchEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<BionomicHeader>();

    public override int ToggleItemType => ModContent.ItemType<WretchedPouch>();

    public override bool ExtraAttackEffect => true;

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      Player Player = player;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!Player.controlUseItem && !Player.controlUseTile && fargoSoulsPlayer.WeaponUseTimer <= 6 || Player.HeldItem.IsAir || Player.HeldItem.damage <= 0 || Player.HeldItem.pick > 0 || Player.HeldItem.axe > 0 || Player.HeldItem.hammer > 0 || !Player.HasBuff(ModContent.BuffType<WretchedHexBuff>()))
        return;
      int index1 = Dust.NewDust(((Entity) Player).position, ((Entity) Player).width, ((Entity) Player).height, 27, ((Entity) Player).velocity.X * 0.4f, ((Entity) Player).velocity.Y * 0.4f, 0, new Color(), 3f);
      Main.dust[index1].noGravity = true;
      Dust dust = Main.dust[index1];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      ref StatModifier local = ref Player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 1.2f);
      Player.endurance -= 0.2f;
      Player player1 = Player;
      ((Entity) player1).velocity = Vector2.op_Multiply(((Entity) player1).velocity, 0.875f);
      if (--fargoSoulsPlayer.WretchedPouchCD > 0)
        return;
      fargoSoulsPlayer.WretchedPouchCD = 25;
      if (((Entity) Player).whoAmI != Main.myPlayer)
        return;
      Vector2 vector2 = Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f);
      NPC npc = ((IEnumerable<NPC>) Main.npc).FirstOrDefault<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && (double) ((Entity) n).Distance(((Entity) Player).Center) < 360.0 && n.CanBeChasedBy((object) null, false) && Collision.CanHit(((Entity) Player).position, ((Entity) Player).width, ((Entity) Player).height, ((Entity) n).position, ((Entity) n).width, ((Entity) n).height)));
      if (npc != null)
        vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Player, ((Entity) npc).Center);
      Vector2 baseVel = Vector2.op_Multiply(vector2, 8f);
      SoundEngine.PlaySound(ref SoundID.Item103, new Vector2?(((Entity) Player).Center), (SoundUpdateCallback) null);
      int dam = 40;
      if (fargoSoulsPlayer.MasochistSoul)
        dam *= 3;
      dam = (int) ((double) dam * (double) Player.ActualClassDamage(DamageClass.Magic));
      int num = npc == null ? 3 : 6;
      float variance = 6.28318548f / (float) num;
      if (npc != null)
      {
        for (int index2 = 0; index2 < num / 2; ++index2)
          ShootTentacle(baseVel, variance, 60, 90);
      }
      for (int index3 = 0; index3 < num; ++index3)
        ShootTentacle(Utils.RotatedBy(baseVel, (double) variance * (double) index3, new Vector2()), variance, 30, 50);

      void ShootTentacle(Vector2 baseVel, float variance, int aiMin, int aiMax)
      {
        Vector2 vector2 = Utils.RotatedBy(baseVel, (double) variance * (Main.rand.NextDouble() - 0.5), new Vector2());
        float num1 = (float) Main.rand.Next(aiMin, aiMax) * (1f / 1000f);
        if (Utils.NextBool(Main.rand))
          num1 *= -1f;
        float num2 = (float) Main.rand.Next(aiMin, aiMax) * (1f / 1000f);
        if (Utils.NextBool(Main.rand))
          num2 *= -1f;
        Projectile.NewProjectile(this.GetSource_EffectItem(Player), ((Entity) Player).Center, vector2, ModContent.ProjectileType<ShadowflameTentacle>(), dam, 4f, ((Entity) Player).whoAmI, num1, num2, 0.0f);
      }
    }
  }
}
