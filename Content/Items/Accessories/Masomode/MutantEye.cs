// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.MutantEye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class MutantEye : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 18, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(1, 0, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.buffImmune[ModContent.BuffType<MutantFangBuff>()] = true;
      fargoSoulsPlayer.MutantEyeItem = this.Item;
      if (!hideVisual)
        fargoSoulsPlayer.MutantEyeVisual = true;
      if (fargoSoulsPlayer.MutantEyeCD > 0)
      {
        --fargoSoulsPlayer.MutantEyeCD;
        if (fargoSoulsPlayer.MutantEyeCD == 0)
        {
          SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          for (int index1 = 0; index1 < 50; ++index1)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 8f), (double) (index1 - 24) * 6.2831854820251465 / 50.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity = vector2_2;
          }
          for (int index3 = 0; index3 < 30; ++index3)
          {
            int index4 = Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
            Main.dust[index4].noGravity = true;
            Main.dust[index4].noLight = true;
            Dust dust = Main.dust[index4];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
          }
        }
      }
      if (((Entity) player).whoAmI == Main.myPlayer && fargoSoulsPlayer.MutantEyeVisual && fargoSoulsPlayer.MutantEyeCD <= 0 && player.ownedProjectileCounts[ModContent.ProjectileType<PhantasmalRing2>()] <= 0)
        Projectile.NewProjectile(player.GetSource_Accessory(this.Item, (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<PhantasmalRing2>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      if (fargoSoulsPlayer.AbomWandCD <= 0)
        return;
      --fargoSoulsPlayer.AbomWandCD;
    }
  }
}
