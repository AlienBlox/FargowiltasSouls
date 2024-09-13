// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.IceQueensCrown
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
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class IceQueensCrown : SoulsItem
  {
    public const int CIRNO_GRAZE_THRESHOLD = 9999;
    public const int CIRNO_GRAZE_MAX = 10539;

    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 7;
      this.Item.value = Item.sellPrice(0, 6, 0, 0);
      this.Item.defense = 5;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.endurance += 0.05f;
      player.buffImmune[ModContent.BuffType<HypothermiaBuff>()] = true;
      IceQueensCrown.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.IceQueensCrown = true;
      if (player.AddEffect<IceQueenGraze>(item))
      {
        fargoSoulsPlayer.Graze = true;
        fargoSoulsPlayer.CirnoGraze = true;
      }
      player.AddEffect<MasoGrazeRing>(item);
      if (!fargoSoulsPlayer.Graze || ((Entity) player).whoAmI != Main.myPlayer || !player.HasEffect<MasoGrazeRing>() || player.ownedProjectileCounts[ModContent.ProjectileType<GrazeRing>()] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<GrazeRing>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public static void OnGraze(FargoSoulsPlayer fargoPlayer, int damage)
    {
      fargoPlayer.CirnoGrazeCounter += damage;
      if (fargoPlayer.CirnoGrazeCounter > 10539)
        fargoPlayer.CirnoGrazeCounter = 10539;
      if (fargoPlayer.CirnoGrazeCounter == 10539 && ((Entity) fargoPlayer.Player).whoAmI == Main.myPlayer && fargoPlayer.Player.ownedProjectileCounts[ModContent.ProjectileType<CirnoBomb>()] < 1)
        Projectile.NewProjectile(((Entity) fargoPlayer.Player).GetSource_Misc(""), ((Entity) fargoPlayer.Player).Center, Vector2.Zero, ModContent.ProjectileType<CirnoBomb>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (!Main.dedServ)
      {
        SoundStyle soundStyle;
        // ISSUE: explicit constructor call
        ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Graze", (SoundType) 0);
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
      }
      Vector2 vector2_1 = Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI);
      bool flag = fargoPlayer.CirnoGrazeCounter > 9999;
      for (int index1 = 0; index1 < 64; ++index1)
      {
        Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(vector2_1, 3f), (double) (index1 - 31) * 6.2831854820251465 / 64.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
        Vector2 vector2_3 = Vector2.op_Subtraction(vector2_2, ((Entity) Main.LocalPlayer).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_2, vector2_3), 0, 0, flag ? 135 : 228, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].scale = flag ? 1f : 0.75f;
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_3;
      }
    }
  }
}
