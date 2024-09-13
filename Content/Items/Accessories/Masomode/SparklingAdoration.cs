// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.SparklingAdoration
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class SparklingAdoration : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 11, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 4;
      this.Item.value = Item.sellPrice(0, 3, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.buffImmune[119] = true;
      player.buffImmune[ModContent.BuffType<LovestruckBuff>()] = true;
      if (player.AddEffect<MasoGraze>(this.Item))
      {
        fargoSoulsPlayer.Graze = true;
        fargoSoulsPlayer.DeviGraze = true;
      }
      fargoSoulsPlayer.DevianttHeartItem = this.Item;
      player.AddEffect<DevianttHearts>(this.Item);
      player.AddEffect<MasoGrazeRing>(this.Item);
      if (!fargoSoulsPlayer.Graze || ((Entity) player).whoAmI != Main.myPlayer || !player.HasEffect<MasoGrazeRing>() || player.ownedProjectileCounts[ModContent.ProjectileType<GrazeRing>()] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(this.Item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<GrazeRing>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public static void OnGraze(FargoSoulsPlayer fargoPlayer, int damage)
    {
      double num1 = 0.25;
      if (fargoPlayer.MutantEyeItem != null)
        num1 += 0.25;
      double num2 = 1.0 / 80.0 * 0.75;
      if (fargoPlayer.AbomWandItem != null)
        num2 *= 2.0;
      fargoPlayer.DeviGrazeBonus += num2;
      if (fargoPlayer.DeviGrazeBonus > num1)
      {
        fargoPlayer.DeviGrazeBonus = num1;
        if (fargoPlayer.StyxSet)
          fargoPlayer.StyxMeter += FargoSoulsUtil.HighestDamageTypeScaling(Main.LocalPlayer, damage) * 4;
      }
      fargoPlayer.DeviGrazeCounter = -1;
      if (!Main.dedServ)
      {
        SoundStyle soundStyle;
        // ISSUE: explicit constructor call
        ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Graze", (SoundType) 0);
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
      }
      Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI);
      for (int index1 = 0; index1 < 64; ++index1)
      {
        Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(vector2_1, 3f), (double) (index1 - 31) * 6.2831854820251465 / 64.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, ((Entity) Main.LocalPlayer).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_2, vector2_3), 0, 0, fargoPlayer.DeviGrazeBonus >= num1 ? 86 : 228, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].scale = fargoPlayer.DeviGrazeBonus >= num1 ? 1f : 0.75f;
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_3;
      }
    }
  }
}
