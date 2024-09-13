// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.FragilePixieLamp
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Lifelight;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class FragilePixieLamp : SoulsItem
  {
    public Vector2 OriginalLocation = Vector2.Zero;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 5;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 54;
      ((Entity) this.Item).height = 44;
      this.Item.useAnimation = 120;
      this.Item.useTime = 120;
      this.Item.useStyle = 4;
      this.Item.rare = 3;
      this.Item.consumable = true;
      this.Item.maxStack = 20;
      this.Item.noUseGraphic = false;
      this.Item.value = Item.sellPrice(0, 3, 0, 0);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyGoldBar", 4).AddIngredient(501, 2).AddIngredient(520, 2).AddIngredient(502, 3).AddTile(134).Register();
    }

    public virtual bool CanUseItem(Player Player)
    {
      return Player.ZoneHallow && Main.dayTime && !NPC.AnyNPCs(ModContent.NPCType<LifeChallenger>());
    }

    public virtual void UseItemFrame(Player player)
    {
      float num = (float) ((120 - player.itemAnimation) / 20);
      Player player1 = player;
      player1.itemLocation = Vector2.op_Addition(player1.itemLocation, new Vector2(Utils.NextFloat(Main.rand, -num, num), Utils.NextFloat(Main.rand, -num, num)));
    }

    public virtual void HoldItem(Player player)
    {
      if (player.itemAnimation == player.itemAnimationMax)
        this.OriginalLocation = player.itemLocation;
      Vector2 vector2 = Vector2.op_Addition(player.itemLocation, new Vector2((float) (((Entity) this.Item).width / 2 * ((Entity) player).direction), (float) (-((Entity) this.Item).height / 2)));
      if (player.itemAnimation > 1)
      {
        SoundEngine.PlaySound(ref SoundID.Pixie, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        Dust.NewDust(vector2, 0, 0, 55, (float) Main.rand.Next(-2, 2), (float) Main.rand.Next(-2, 2), 100, new Color(), 0.75f);
      }
      if (player.itemAnimation != 1)
        return;
      if (player.ZoneHallow && Main.dayTime)
      {
        SoundEngine.PlaySound(ref SoundID.Shatter, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        for (int index = 0; index < 50; ++index)
          Dust.NewDust(Vector2.op_Subtraction(vector2, Vector2.op_Division(((Entity) this.Item).Size, 2f)), ((Entity) this.Item).width, ((Entity) this.Item).height, 297, ((Entity) player).velocity.X, ((Entity) player).velocity.Y, 100, new Color(), 1f);
      }
      else
        SoundEngine.PlaySound(ref SoundID.NPCDeath3, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
    }

    public virtual bool? UseItem(Player Player)
    {
      FargoSoulsUtil.SpawnBossNetcoded(Player, ModContent.NPCType<LifeChallenger>());
      return new bool?(true);
    }
  }
}
