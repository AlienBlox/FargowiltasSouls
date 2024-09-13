// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.ParadoxWolf.ParadoxWolfSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Common.Configs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.ParadoxWolf
{
  public class ParadoxWolfSoul : PatreonModItem
  {
    private int dashTime;
    private int dashCD;

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 5;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      if (player.mount.Active)
        return;
      if (this.dashCD > 0)
      {
        --this.dashCD;
        if (this.dashCD != 0)
          return;
        double num = Math.PI / 18.0;
        for (int index1 = 0; index1 < 36; ++index1)
        {
          Vector2 vector2 = Utils.RotatedBy(new Vector2(2f, 2f), num * (double) index1, new Vector2());
          int index2 = Dust.NewDust(((Entity) player).Center, 0, 0, 37, vector2.X, vector2.Y, 100, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
        }
      }
      else if (this.dashTime > 0)
      {
        --this.dashTime;
        ((Entity) player).position.Y = ((Entity) player).oldPosition.Y;
        player.immune = true;
        player.controlLeft = false;
        player.controlRight = false;
        player.controlJump = false;
        player.controlDown = false;
        player.controlUseItem = false;
        player.controlUseTile = false;
        player.controlHook = false;
        player.controlMount = false;
        player.itemAnimation = 0;
        player.itemTime = 0;
        if (this.dashTime != 0)
          return;
        Player player1 = player;
        ((Entity) player1).velocity = Vector2.op_Multiply(((Entity) player1).velocity, 0.5f);
        player.dashDelay = 0;
        this.dashCD = 180;
      }
      else
      {
        int num = 0;
        if (Fargowiltas.Fargowiltas.DashKey.Current)
        {
          if (player.controlRight)
            num = 1;
          else if (player.controlLeft)
            num = -1;
        }
        else if (!ModContent.GetInstance<FargoClientConfig>().DoubleTapDashDisabled)
        {
          if (player.controlRight && player.releaseRight)
          {
            if (player.doubleTapCardinalTimer[2] > 0 && player.doubleTapCardinalTimer[2] != 15)
              num = 1;
          }
          else if (player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[3] > 0 && player.doubleTapCardinalTimer[3] != 15)
            num = -1;
        }
        if (num == 0)
          return;
        ((Entity) player).velocity.X = 25f * (float) num;
        player.dashDelay = -1;
        this.dashTime = 20;
        Projectile.NewProjectile(player.GetSource_Accessory(this.Item, (string) null), ((Entity) player).Center, new Vector2(((Entity) player).velocity.X, 0.0f), ModContent.ProjectileType<WolfDashProj>(), (int) (50.0 * (double) ((StatModifier) ref player.GetDamage(DamageClass.Melee)).Additive), 0.0f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
        SoundEngine.PlaySound(ref SoundID.NPCDeath8, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
    }
  }
}
