// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.EaterLauncherJr
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class EaterLauncherJr : SoulsItem
  {
    public const int MaxCharge = 1000;
    public int Charge;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 36;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = this.Item.useAnimation = 46;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 6f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item95);
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 1;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<EaterRocketJr>();
      this.Item.shootSpeed = 18f;
    }

    public virtual void SaveData(TagCompound tag)
    {
      tag.Add("BlastbiterCharge", (object) this.Charge);
    }

    public virtual void LoadData(TagCompound tag)
    {
      if (!tag.ContainsKey("BlastbiterCharge"))
        return;
      this.Charge = tag.GetAsInt("BlastbiterCharge");
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(2f, -4f));

    public virtual bool CanRightClick() => Main.LocalPlayer.HasItem(68) && this.Charge < 1000;

    private void LoadChunk(Player player)
    {
      if (!player.ConsumeItem(68, false, false))
        return;
      SoundStyle soundStyle = SoundID.Item149;
      ((SoundStyle) ref soundStyle).Pitch = 0.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      this.Charge += 100;
      if (this.Charge <= 1000)
        return;
      this.Charge = 1000;
    }

    public virtual void RightClick(Player player) => this.LoadChunk(player);

    public virtual bool ConsumeItem(Player player) => false;

    public virtual bool CanUseItem(Player player) => this.Charge > 0 || player.HasItem(68);

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      if (this.Charge <= 0)
        this.LoadChunk(player);
      --this.Charge;
      return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public virtual void PostDrawInInventory(
      SpriteBatch spriteBatch,
      Vector2 position,
      Rectangle frame,
      Color drawColor,
      Color itemColor,
      Vector2 origin,
      float scale)
    {
      Utils.DrawBorderString(spriteBatch, this.Charge.ToString(), Vector2.op_Subtraction(position, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 15f), scale)), Color.SandyBrown, 0.75f, 0.0f, 0.0f, -1);
    }

    public virtual void HoldItem(Player player)
    {
      if (player.itemTime <= 0)
        return;
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2_1 = new Vector2();
        double num1 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_1.X += (float) (Math.Sin(num1) * 300.0);
        vector2_1.Y += (float) (Math.Cos(num1) * 300.0);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_1), new Vector2(4f, 4f)), 0, 0, 70, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.velocity = ((Entity) player).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_1), 5f));
        }
        dust1.noGravity = true;
        dust1.scale = 1f;
        Vector2 vector2_2 = new Vector2();
        double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_2.X += (float) Math.Sin(num2) * (float) player.FargoSouls().RockeaterDistance;
        vector2_2.Y += (float) Math.Cos(num2) * (float) player.FargoSouls().RockeaterDistance;
        Dust dust3 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, vector2_2), new Vector2(4f, 4f)), 0, 0, 70, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust3.velocity = ((Entity) player).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust4 = dust3;
          dust4.velocity = Vector2.op_Addition(dust4.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_2), -5f));
        }
        dust3.noGravity = true;
        dust3.scale = 1f;
      }
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = ModContent.ProjectileType<EaterRocketJr>();
    }
  }
}
